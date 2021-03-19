using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class CheckPoint : BaseGameObject
    {
        private const int BBWidth = 38;
        private const int BBHeight = 72;

        public bool activated = false;

        public CheckPoint(Texture2D texture)
        {
            baseTexture = texture;
            AddBoundingBox(new BoundingBox(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public void ChangeTexture(Texture2D texture)
        {
            baseTexture = texture;
        }
    }
}
