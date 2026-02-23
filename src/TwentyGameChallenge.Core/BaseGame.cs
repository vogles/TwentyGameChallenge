using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TwentyGameChallenge.Core;

public class BaseGame : Game
{
    public static GraphicsDeviceManager Graphics { get; private set; }
    
    public new static GraphicsDevice GraphicsDevice { get; private set; }
    
    public new static ContentManager Content { get; private set; }
    
    public static SpriteBatch SpriteBatch { get; private set; }
    
    public BaseGame(string title, int width, int height, bool fullscreen)
    {
        Graphics = new GraphicsDeviceManager(this);
        
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullscreen;
        
        Graphics.ApplyChanges();
        
        Window.Title = title;

        Content = base.Content;
        Content.RootDirectory = "Content";
        
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GraphicsDevice = base.GraphicsDevice;
        
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        
        base.Initialize();
    }
}