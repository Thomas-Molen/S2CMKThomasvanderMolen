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

        public void SetTexture(Texture2D Texture)
        {
            texture = Texture;
        }
    }
}
