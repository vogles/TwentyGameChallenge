using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TwentyGameChallenge.Core.Graphics;

public class Sprite
{
    public Texture2D Texture { get; set; }

    public Rectangle SourceRectangle { get; set; } = Rectangle.Empty;
    
    public Vector2 Origin { get; set; } = Vector2.Zero;

    public float Width => SourceRectangle.Width;

    public float Height => SourceRectangle.Height;

    public Sprite() { }
    
    public Sprite(Texture2D texture, int x, int y, int width, int height) : this(texture, new Rectangle(x, y, width, height)) { }

    public Sprite(Texture2D texture, Rectangle sourceRectangle)
    {
        Texture = texture;
        SourceRectangle = sourceRectangle;
    }

    public void CenterOrigin()
    {
        Origin = new Vector2(Width, Height) * 0.5f;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        // Region.Draw(spriteBatch, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
}