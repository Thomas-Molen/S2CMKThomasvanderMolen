using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class RockHeadSprite : BaseGameObject
    {
        private const int BBWidth = 129;
        private const int BBHeight = 129;
        private int minPosX;
        private int maxPosX;

        private int movementSpeed = 2;
        public int xVelocity;

        Texture2D idleTexture;

        public RockHeadSprite(Texture2D texture, int inputMinPosX, int inputMaxPosX)
        {
            baseTexture = texture;
            idleTexture = texture;
            minPosX = inputMinPosX;
            maxPosX = inputMaxPosX;

            xVelocity = movementSpeed;

            AddBoundingBox(new BoundingBox(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public void Movement()
        {
            if (baseTexture != idleTexture)
            {
                baseTexture = idleTexture;
            }

            if (Position.X >= maxPosX)
            {
                xVelocity = -movementSpeed;
            }
            else if (Position.X <= minPosX)
            {
                xVelocity = movementSpeed;
            }
            Position = new Vector2(Position.X + xVelocity, Position.Y);
        }

        public void ChangeTexture(Texture2D texture)
        {
            baseTexture = texture;
        }
    }
}
