using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TwentyGameChallenge.Core;

public class GameObject
{
    private Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();
    private List<IUpdatableComponent> _updateables = new List<IUpdatableComponent>();
    private List<IDrawableComponent> _drawables = new List<IDrawableComponent>();
    private List<GameObject> _children = new List<GameObject>();

    public GameObject()
    {
        AddComponent<Transform>();
    }
    
    public void AddChild(GameObject child)
    {
        if (child != null)
        {
            _children.Add(child);
        }
    }

    public T AddComponent<T>() where T : IComponent, new()
    {
        var type = typeof(T);

        if (_components.ContainsKey(type))
            return (T)_components[type];
        
        var newComponent = new T();
        
        AddComponent(newComponent);

        return newComponent;
    }

    public void AddComponent(IComponent component)
    {
        if (component == null)
            return;

        var type = component.GetType();

        if (!_components.TryAdd(type, component))
            return;
        
        if (component is IUpdatableComponent updatableComponent)
        {
            _updateables.Add(updatableComponent);
        }

        if (component is IDrawableComponent drawableComponent)
        {
            _drawables.Add(drawableComponent);
        }

        component.GameObject = this;
    }

    public T GetComponent<T>()
    {
        var type = typeof(T);

        if (_components.ContainsKey(type))
            return (T)_components[type];

        return default(T);
    }

    public void RemoveComponent<T>()
    {
        var type = typeof(T);

        if (!_components.ContainsKey(type))
            return;
        
        var component = _components[typeof(T)];
        var updatable = component as IUpdatableComponent;
        var drawable = component as IDrawableComponent;
        
        _components.Remove(type);
        
        if (_updateables.Contains(updatable))
            _updateables.Remove(component as IUpdatableComponent);
        
        if (_drawables.Contains(drawable))
            _drawables.Remove(component as IDrawableComponent);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var updateable in _updateables)
        {
            updateable.Update(gameTime);
        }

        foreach (var child in _children)
        {
            child.Update(gameTime);
        }
    }

    public void Draw(GameTime gameTime)
    {
        foreach (var drawable in _drawables)
        {
            drawable.Draw(gameTime);
        }

        foreach (var child in _children)
        {
            child.Draw(gameTime);
        }
    }
}