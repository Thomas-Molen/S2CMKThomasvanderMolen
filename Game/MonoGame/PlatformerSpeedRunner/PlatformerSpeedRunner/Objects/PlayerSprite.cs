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
        private float xVelocity = 0.0f;
        private float yVelocity = 0.0f;
        private float xMomentum = 0.0f;
        private float yMomentum = 0.0f;
        private const float playerSpeed = 5.0f;
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

        public void PlayerPhysics(bool gravityState)
        {
            if (gravityState)
            {
                GravityEffect();
            }
            //Friction();
            //AirDrag();
            if (xVelocity + xMomentum <= 10)
            {
                Position = new Vector2(Position.X + xVelocity + xMomentum, Position.Y);
            }
            else
            {
                Position = new Vector2(Position.X + 10, Position.Y);
            }
            if (yVelocity + yMomentum <= 10)
            {
                Position = new Vector2(Position.X, Position.Y + yVelocity + yMomentum);
            }
            else
            {
                Position = new Vector2(Position.X, Position.Y + 10);
            }
            
        }

        public void MoveLeft()
        {
            if (xMomentum >= -playerSpeed)
            {
                xMomentum -= playerSpeed / 4;
            }
        }

        public void MoveRight()
        {
            if (xMomentum <= playerSpeed)
            {
                xMomentum += playerSpeed / 4;
            }
        }

        public void MoveNone()
        {
            xMomentum = 0;
        }

        private void GravityEffect()
        {
            if (yVelocity <= 12)
            {
                yVelocity += playerGravity;
            }
        }

        public void YVelocityNone()
        {
            yVelocity = 0;
            yMomentum = 0;
        }

        public void XVelocityNone()
        {
            xVelocity = 0;
            xMomentum = 0;
        }

        public void Grapple()
        {
            MouseState mouseState = Mouse.GetState();

            float xGrappleDistance = mouseState.X - Position.X;
            float yGrappleDistance = mouseState.Y - Position.Y;
            if (xGrappleDistance != 0)
            {
                if (xVelocity < 5)
                {
                    xVelocity += xGrappleDistance / 100;
                }
            }
            
            if (yGrappleDistance != 0)
            {
                if (yMomentum < 5)
                {
                    yMomentum += yGrappleDistance / 100;
                }
            }
        }

        private void AirDrag()
        {
            if (xVelocity > 0)
            {
                xVelocity += -0.5f;
            }
            else if (xVelocity < 0)
            {
                xVelocity += 0.5f;
            }

            if (yMomentum > 0)
            {
                yMomentum += -0.5f;
            }
            else if (yMomentum < 0)
            {
                yMomentum += 0.5f;
            }
        }

        private void Friction()
        {
            Decimal.Round(Convert.ToDecimal(xVelocity), MidpointRounding.ToZero);
        }
    }
}