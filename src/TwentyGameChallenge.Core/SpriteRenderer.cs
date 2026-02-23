using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TwentyGameChallenge.Core.Graphics;

namespace TwentyGameChallenge.Core;

public class SpriteRenderer : IDrawableComponent
{
    private readonly SpriteBatch _spriteBatch;
    
    public GameObject GameObject { get; set; }
    
    public Sprite Sprite { get; set; }

    public SpriteRenderer(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
    }
    
    public void Draw(GameTime gameTime)
    {
        var transform = GameObject.GetComponent<Transform>();
        var position = transform.WorldPosition;
        var scale = transform.WorldScale;
        var rotation = transform.RotationInEulerAngles();
        var textureRegion = Sprite?.Region;

        if (textureRegion != null)
        {
            _spriteBatch.Draw(
                textureRegion.Texture, 
                position.ToVector2(), 
                textureRegion.SourceRectangle,
                Sprite.Color,
                rotation.Z,
                Sprite.Origin,
                scale.ToVector2(),
                Sprite.Effects,
                Sprite.LayerDepth);
        }
    }

    public int DrawOrder { get; private set; } = 0;
    public bool Visible { get; set; } = true;
    public event EventHandler<EventArgs> DrawOrderChanged;
    public event EventHandler<EventArgs> VisibleChanged;
}