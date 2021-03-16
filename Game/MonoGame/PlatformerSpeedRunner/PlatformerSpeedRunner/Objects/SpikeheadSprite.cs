using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class SpikeHeadSprite : BaseGameObject
    {
        private const int BBWidth = 129;
        private const int BBHeight = 129;
        private int minPosY;
        private int maxPosY;

        private int movementSpeed = 3;
        public int yVelocity;

        Texture2D idleTexture;

        public SpikeHeadSprite(Texture2D texture, int inputMinPosX, int inputMaxPosX)
        {
            baseTexture = texture;
            idleTexture = texture;
            minPosY = inputMinPosX;
            maxPosY = inputMaxPosX;

            yVelocity = movementSpeed;

            AddBoundingBox(new BoundingBox(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public void Movement()
        {
            if (Position.Y >= maxPosY)
            {
                yVelocity = -movementSpeed;
            }
            else if (Position.Y <= minPosY)
            {
                yVelocity = movementSpeed;
            }
            Position = new Vector2(Position.X, Position.Y + yVelocity);
        }
    }
}
