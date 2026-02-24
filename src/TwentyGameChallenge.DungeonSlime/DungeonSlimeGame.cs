using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TwentyGameChallenge.Core;
using TwentyGameChallenge.Core.Components;
using TwentyGameChallenge.Core.Graphics;

namespace TwentyGameChallenge.DungeonSlime;

public class DungeonSlimeGame : BaseGame
{
    // private Texture2D _logo;

    private GameObject _slime;
    private GameObject _bat;
    
    public DungeonSlimeGame() : base("Dungeon Slime", 1280, 720, false)
    {
    }
    
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
        // _logo = Content.Load<Texture2D>("logo");

        var atlas = SpriteAtlas.FromFile(Content, "images/atlas-definition.xml");

        _slime = new GameObject();
        _slime.GetComponent<Transform>()
            .Scale(4, 4, 1);
        _slime.AddComponent(new SpriteRenderer(SpriteBatch)
        {
            Sprite = atlas.GetSprite("slime-1")
        });
        var slimeAnimator = _slime.AddComponent<Animator>();
        slimeAnimator.Animation = atlas.GetAnimation("slime-idle");
        slimeAnimator.Play();
        
        _bat = new GameObject();
        _bat.GetComponent<Transform>()
            .Scale(4, 4, 1)
            .Translate(90, 0, 0);
        _bat.AddComponent(new SpriteRenderer(SpriteBatch)
        {
            Sprite = atlas.GetSprite("bat-1")
        });
        var batAnimator = _bat.AddComponent<Animator>();
        batAnimator.Animation = atlas.GetAnimation("bat-idle");
        batAnimator.Play();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _slime.Update(gameTime);
        _bat.Update(gameTime);

        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        SpriteBatch.Begin();
        
        // SpriteBatch.Draw(_logo, Vector2.Zero, Color.White);
        _slime.Draw(gameTime);
        
        _bat.Draw(gameTime);
        
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}