using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class ObjectSprite : RenderAbleObject
    {
        public BoundingBoxHelper boundingBoxHelper = new BoundingBoxHelper();
        public RenderHelper renderHelper = new RenderHelper();
        private int BBWidth;
        private int BBHeight;

        public ObjectSprite(Texture2D Texture, Vector2 Position, bool BoundingBox = false)
        {
            positionHelper.SetPosition(Position);
            textureHelper.SetTexture(Texture);

            if (BoundingBox)
            {
                BBWidth = textureHelper.Width + 1;
                BBHeight = textureHelper.Height + 1;

                boundingBoxHelper.AddBoundingBox(new BoundingBoxObject(new Vector2(Position.X, Position.Y), BBWidth, BBHeight));
            }
        }
        public ObjectSprite(Texture2D Texture, int BoundingBoxWidth, int BoundingBoxHeight, Vector2 Position)
        {
            positionHelper.SetPosition(Position);
            textureHelper.SetTexture(Texture);

            BBWidth = BoundingBoxWidth + 1;
            BBHeight = BoundingBoxHeight + 1;

            boundingBoxHelper.AddBoundingBox(new BoundingBoxObject(new Vector2(Position.X, Position.Y), BBWidth, BBHeight));
        }
    }
}
