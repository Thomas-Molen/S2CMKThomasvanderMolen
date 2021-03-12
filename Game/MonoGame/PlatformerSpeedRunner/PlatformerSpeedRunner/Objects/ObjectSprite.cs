using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class ObjectSprite : BaseGameObject
    {
        private int BBWidth;
        private int BBHeight;

        public ObjectSprite(Texture2D texture, int BoundingBoxWidth, int BoundingBoxHeight)
        {
            baseTexture = texture;

            BBWidth = BoundingBoxWidth + 1;
            BBHeight = BoundingBoxHeight + 1;

            AddBoundingBox(new BoundingBox(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public ObjectSprite(Texture2D texture, int BoundingBoxWidth, int BoundingBoxHeight, int BoundingBoxOffSetX, int BoundingBoxOffSetY)
        {
            baseTexture = texture;

            BBWidth = BoundingBoxWidth + 1;
            BBHeight = BoundingBoxHeight + 1;

            AddBoundingBox(new BoundingBox(new Vector2(BoundingBoxOffSetX, BoundingBoxOffSetY), BBWidth, BBHeight));
        }

        public ObjectSprite(Texture2D texture)
        {
            baseTexture = texture;
        }
    }
}
