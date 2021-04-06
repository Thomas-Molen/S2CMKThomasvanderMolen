using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class BasicObject : RenderAbleObject
    {
        public BoundingBoxHelper BoundingBox = new BoundingBoxHelper();
        private int BBWidth;
        private int BBHeight;

        public BasicObject(Texture2D Texture, Vector2 Position, bool BoundingBox = false)
        {
            base.Position.SetPosition(Position);
            base.Texture.SetTexture(Texture);

            if (BoundingBox)
            {
                BBWidth = base.Texture.Width + 1;
                BBHeight = base.Texture.Height + 1;

                this.BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(Position.X, Position.Y), BBWidth, BBHeight));
            }
        }
        public BasicObject(Texture2D Texture, int BoundingBoxWidth, int BoundingBoxHeight, Vector2 Position)
        {
            base.Position.SetPosition(Position);
            base.Texture.SetTexture(Texture);

            BBWidth = BoundingBoxWidth + 1;
            BBHeight = BoundingBoxHeight + 1;

            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(Position.X, Position.Y), BBWidth, BBHeight));
        }
    }
}
