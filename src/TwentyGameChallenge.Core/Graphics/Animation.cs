using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TwentyGameChallenge.Core.Graphics;

public class Animation
{
    private int _currentFrame = 0;
    private TimeSpan _elapsed = TimeSpan.Zero;
    
    public List<Sprite> Frames { get; set; }
    
    public TimeSpan Delay { get; set; }

    public Animation()
    {
        Frames = new List<Sprite>();
        Delay = TimeSpan.FromMilliseconds(100);
    }

    public Animation(List<Sprite> frames, TimeSpan delay)
    {
        Frames = frames;
        Delay = delay;
    }

    public void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;

        if (_elapsed >= Delay)
        {
            _elapsed -= Delay;
            _currentFrame++;

            if (_currentFrame >= Frames.Count)
            {
                _currentFrame = 0;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        Frames[_currentFrame].Draw(spriteBatch, position);
    }
}