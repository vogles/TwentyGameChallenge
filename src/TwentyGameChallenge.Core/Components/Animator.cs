using System;
using Microsoft.Xna.Framework;
using TwentyGameChallenge.Core.Graphics;

namespace TwentyGameChallenge.Core.Components;

public class Animator : IUpdatableComponent
{
    public bool Enabled { get; } = true;

    public int UpdateOrder { get; } = 0;
    
    public GameObject GameObject { get; set; }
    
    public Animation Animation { get; set; }
    
    public event EventHandler<EventArgs> EnabledChanged;
    public event EventHandler<EventArgs> UpdateOrderChanged;
    
    private int _currentFrame = -1;
    private TimeSpan _elapsed = TimeSpan.Zero;
    private bool _isAnimating = false;
    private SpriteRenderer _renderer = null;

    public void Play()
    {
        if (Animation == null)
            return;
        
        _currentFrame = 0;
        _elapsed = TimeSpan.Zero;
        _isAnimating = true;

        _renderer = GameObject.GetComponent<SpriteRenderer>();
    }
    
    public void Update(GameTime gameTime)
    {
        if (_isAnimating)
        {
            _elapsed += gameTime.ElapsedGameTime;

            if (_elapsed >= Animation.Delay)
            {
                _elapsed -= Animation.Delay;
                _currentFrame++;

                if (_currentFrame >= Animation.Frames.Count)
                    _currentFrame = 0;
                
                var currentSprite = Animation.Frames[_currentFrame];
                _renderer.Sprite = currentSprite;
            }
        }
    }
}