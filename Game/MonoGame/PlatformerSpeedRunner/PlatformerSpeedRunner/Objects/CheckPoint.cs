using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class CheckPoint : RenderAbleObject
    {
        public BoundingBoxHelper BoundingBox = new BoundingBoxHelper();
        
        private const int BBWidth = 38;
        private const int BBHeight = 72;

        public bool activated = false;

        public CheckPoint(Texture2D Texture, Vector2 Position)
        {
            base.Texture.SetTexture(Texture);
            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(Position.X, Position.Y), BBWidth, BBHeight));

            base.Position.SetPosition(Position);
        }
    }
}
