using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class StoneGroundSprite : BaseGameObject
    {
        private const int BBPosX = 1;
        private const int BBPosY = 1;
        private const int BBWidth = 200;
        private const int BBHeight = 67;

        public StoneGroundSprite(Texture2D texture)
        {
            baseTexture = texture;
            AddBoundingBox(new BoundingBox(new Vector2(BBPosX, BBPosY), BBWidth, BBHeight));
        }
    }
}
