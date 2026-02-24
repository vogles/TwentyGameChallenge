using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TwentyGameChallenge.Core.Components;
using TwentyGameChallenge.Core.Graphics;

namespace TwentyGameChallenge.Core;

public class SpriteRenderer : IDrawableComponent
{
    private readonly SpriteBatch _spriteBatch;
    
    public GameObject GameObject { get; set; }
    
    public Sprite Sprite { get; set; }

    public Color Color { get; set; } = Color.White;
    
    public float LayerDepth { get; set; } = 0f;
    
    public SpriteRenderer(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
    }
    
    public void Draw(GameTime gameTime)
    {
        if (Sprite == null)
            return;
        
        var transform = GameObject.GetComponent<Transform>();
        var position = transform.WorldPosition;
        var scale = transform.WorldScale;
        var rotation = transform.RotationInEulerAngles();

        _spriteBatch.Draw(
            Sprite.Texture, 
            position.ToVector2(), 
            Sprite.SourceRectangle,
            Color,
            rotation.Z,
            Sprite.Origin,
            scale.ToVector2(),
            SpriteEffects.None,
            LayerDepth);
    }

    public int DrawOrder { get; private set; } = 0;
    public bool Visible { get; set; } = true;
    public event EventHandler<EventArgs> DrawOrderChanged;
    public event EventHandler<EventArgs> VisibleChanged;
}