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
        private const int BBPosX = 0;
        private const int BBPosY = 0;
        private const int BBWidth = 201;
        private const int BBHeight = 68;

        public StoneGroundSprite(Texture2D texture)
        {
            baseTexture = texture;
            AddBoundingBox(new BoundingBox(new Vector2(BBPosX, BBPosY), BBWidth, BBHeight));
        }
    }
}
