using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects.Base;
using PlatformerSpeedRunner.Enum;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace PlatformerSpeedRunner.Objects
{
    class PlayerSprite : BaseGameObject
    {
        public float xVelocity = 0.0f;
        public float yVelocity = 0.0f;
        private const float playerSpeed = 3.0f;
        private const float playerGravity = 0.3f;

        private const int BBPosX = 1;
        private const int BBPosY = 1;
        private const int BBWidth = 50;
        private const int BBHeight = 60;

        public PlayerSprite(Texture2D texture)
        {
            baseTexture = texture;
            AddBoundingBox(new BoundingBox(new Vector2(BBPosX, BBPosY), BBWidth, BBHeight));
        }

        public void PlayerPhysics()
        {
            GravityEffect();        

            if (xVelocity > 15)
            {
                xVelocity = 15;
            }
            else if (xVelocity < -15)
            {
                xVelocity = -15;
            }

            Position = new Vector2(Position.X + xVelocity, Position.Y + yVelocity);           
        }

        public void MoveLeft()
        {
            if (xVelocity >= -playerSpeed)
            {
                xVelocity -= playerSpeed / 4;
            }
        }

        public void MoveRight()
        {
            if (xVelocity <= playerSpeed)
            {
                xVelocity += playerSpeed / 4;
            }
        }

        public void NoDirection()
        {
            if (xVelocity > 0)
            {
                xVelocity += -playerSpeed / 15;
            }
            if (xVelocity < 0)
            {
                xVelocity += playerSpeed / 15;
            }
            if (xVelocity > 0 && xVelocity < 0.5)
            {
                xVelocity = 0;
            }
        }

        private void GravityEffect()
        {
            if (yVelocity <= 20)
            {
                yVelocity += playerGravity;
            }
        }

        public void OnGround(float yPosition)
        {
            Position = new Vector2(Position.X, yPosition);
        }

        public void Grapple(int timeCharged)
        {
            MouseState mouseState = Mouse.GetState();

            //Position = new Vector2(mouseState.X, mouseState.Y);
            float xGrappleDistance = mouseState.X - Position.X;
            float yGrappleDistance = mouseState.Y - Position.Y;

            yVelocity += (yGrappleDistance / 100) * timeCharged/10;
            xVelocity += (xGrappleDistance / 100) * timeCharged/10;
        }
    }
}