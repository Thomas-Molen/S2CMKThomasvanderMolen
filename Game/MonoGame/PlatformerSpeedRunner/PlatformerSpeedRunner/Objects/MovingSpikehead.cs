using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class MovingSpikeHead : RenderAbleObject
    {
        public BoundingBoxHelper BoundingBox = new BoundingBoxHelper();
        private const int BBWidth1 = 75;
        private const int BBHeight1 = 129;
        private const int BBWidth2 = 129;
        private const int BBHeight2 = 75;
        private int minPosY;
        private int maxPosY;

        private int movementSpeed = 3;
        public int yVelocity;

        public MovingSpikeHead(Texture2D Texture, Vector2 Position, int inputMinPosX, int inputMaxPosX)
        {
            base.Position.SetPosition(Position);
            base.Texture.SetTexture(Texture);
            minPosY = inputMinPosX;
            maxPosY = inputMaxPosX;

            yVelocity = movementSpeed;

            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(30, 0), BBWidth1, BBHeight1));
            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(0, 30), BBWidth2, BBHeight2));
        }

        public void Movement()
        {
            BoundingBox.UpdateBoundingBoxes(Position.position);
            if (Position.position.Y >= maxPosY)
            {
                yVelocity = -movementSpeed;
            }
            else if (Position.position.Y <= minPosY)
            {
                yVelocity = movementSpeed;
            }
            Position.SetPosition(new Vector2(Position.position.X, Position.position.Y + yVelocity));
        }
    }
}
