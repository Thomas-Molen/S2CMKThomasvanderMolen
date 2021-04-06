using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class SpikeHeadSprite : RenderAbleObject
    {
        public BoundingBoxHelper boundingBoxHelper = new BoundingBoxHelper();
        public RenderHelper renderHelper = new RenderHelper();
        private const int BBWidth1 = 75;
        private const int BBHeight1 = 129;
        private const int BBWidth2 = 129;
        private const int BBHeight2 = 75;
        private int minPosY;
        private int maxPosY;

        private int movementSpeed = 3;
        public int yVelocity;

        public SpikeHeadSprite(Texture2D Texture, Vector2 Position, int inputMinPosX, int inputMaxPosX)
        {
            positionHelper.SetPosition(Position);
            textureHelper.SetTexture(Texture);
            minPosY = inputMinPosX;
            maxPosY = inputMaxPosX;

            yVelocity = movementSpeed;

            boundingBoxHelper.AddBoundingBox(new BoundingBoxObject(new Vector2(30, 0), BBWidth1, BBHeight1));
            boundingBoxHelper.AddBoundingBox(new BoundingBoxObject(new Vector2(0, 30), BBWidth2, BBHeight2));
        }

        public void Movement()
        {
            boundingBoxHelper.UpdateBoundingBoxes(positionHelper.position);
            if (positionHelper.position.Y >= maxPosY)
            {
                yVelocity = -movementSpeed;
            }
            else if (positionHelper.position.Y <= minPosY)
            {
                yVelocity = movementSpeed;
            }
            positionHelper.SetPosition(new Vector2(positionHelper.position.X, positionHelper.position.Y + yVelocity));
        }
    }
}
