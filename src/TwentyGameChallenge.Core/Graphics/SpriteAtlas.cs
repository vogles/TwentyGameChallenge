using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TwentyGameChallenge.Core.Graphics;

public class SpriteAtlas
{
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();
 
    public Texture2D Texture { get; private set; }

    public SpriteAtlas() { }

    public SpriteAtlas(Texture2D texture)
    {
        Texture = texture;
    }

    public void AddSprite(string name, int x, int y, int width, int height)
    {
        _sprites.Add(name, new Sprite(Texture, x, y, width, height));
    }

    public Sprite GetSprite(string name)
    {
        return _sprites[name];
    }

    public void RemoveSprite(string name)
    {
        _sprites.Remove(name);
    }

    public void AddAnimation(string animationName, Animation animation)
    {
        _animations.Add(animationName, animation);
    }

    public Animation GetAnimation(string animationName)
    {
        return _animations[animationName];
    }

    public void RemoveAnimation(string animationName)
    {
        _animations.Remove(animationName);
    }

    public void Clear()
    {
        _sprites.Clear();
        _animations.Clear();
    }

    public static SpriteAtlas FromFile(ContentManager content, string assetName)
    {
        var atlas = new SpriteAtlas();
        var filePath = Path.Combine(content.RootDirectory, assetName);

        using (var stream = TitleContainer.OpenStream(filePath))
        {
            using (var reader = XmlReader.Create(stream))
            {
                var doc = XDocument.Load(reader);
                var root = doc.Root;

                var texturePath = root.Element("Texture").Value;
                atlas.Texture = content.Load<Texture2D>(texturePath);
                
                var regions = root.Element("Regions")?.Elements("Region");

                if (regions != null)
                {
                    foreach (var region in regions)
                    {
                        var name = region.Attribute("name")?.Value;
                        var x = int.Parse(region.Attribute("x")?.Value ?? "0");
                        var y = int.Parse(region.Attribute("y")?.Value ?? "0");
                        var width = int.Parse(region.Attribute("width")?.Value ?? "0");
                        var height = int.Parse(region.Attribute("height")?.Value ?? "0");

                        if (!String.IsNullOrEmpty(name))
                        {
                            atlas.AddSprite(name, x, y, width, height);
                        }
                    }
                }
                
                var animations = root.Element("Animations")?.Elements("Animation");

                if (animations != null)
                {
                    foreach (var animation in animations)
                    {
                        var animationName = animation.Attribute("name")?.Value;
                        var delayInMilliseconds = int.Parse(animation.Attribute("delay")?.Value ?? "0");
                        var delay = TimeSpan.FromMilliseconds(delayInMilliseconds);

                        var frameList = new List<Sprite>();
                        
                        var frames = animation.Elements("Frame");

                        foreach (var frame in frames)
                        {
                            var regionName = frame.Attribute("region")?.Value;
                            var sprite = atlas.GetSprite(regionName);
                            frameList.Add(sprite);
                        }

                        var newAnimation = new Animation(frameList, delay);
                        atlas.AddAnimation(animationName, newAnimation);
                    }
                }
            }
        }

        return atlas;
    }
}