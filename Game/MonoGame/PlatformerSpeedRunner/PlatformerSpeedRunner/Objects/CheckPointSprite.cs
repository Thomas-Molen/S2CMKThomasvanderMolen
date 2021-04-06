using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class CheckPointSprite : RenderAbleObject
    {
        public BoundingBoxHelper boundingBoxHelper = new BoundingBoxHelper();
        public RenderHelper renderHelper = new RenderHelper();
        
        private const int BBWidth = 38;
        private const int BBHeight = 72;

        public bool activated = false;

        public CheckPointSprite(Texture2D Texture, Vector2 Position)
        {
            textureHelper.SetTexture(Texture);
            boundingBoxHelper.AddBoundingBox(new BoundingBoxObject(new Vector2(Position.X, Position.Y), BBWidth, BBHeight));

            positionHelper.SetPosition(Position);
        }
    }
}
