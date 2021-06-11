using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class TextureHelper
    {
        public Texture2D texture { get; private set; }
        public int Width { get { return texture.Width; } }
        public int Height { get { return texture.Height; } }
        private ContentManager content;

        public TextureHelper(ContentManager contentManager)
        {
            content = contentManager;
        }

        public void SetTexture(Texture2D Texture)
        {
            texture = Texture;
        }
        public void SetTexture(string textureString)
        {
            texture = GetTexture2D(textureString);
        }

        public Texture2D GetTexture2D(string textureName)
        {
            try
            {
                var texture = content.Load<Texture2D>(textureName);
                return texture;
            }
            catch (ContentLoadException)
            {
                var texture = content.Load<Texture2D>("ErrorSprite");
                return texture;
            }
        }
    }
}
