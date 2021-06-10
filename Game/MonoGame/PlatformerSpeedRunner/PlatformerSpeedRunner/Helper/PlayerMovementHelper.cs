using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class PlayerMovementHelper
    {
        private const int maxXVelocity = 10;
        private const int maxYVelocity = 15;
        private const float Gravity = 0.1f; //Change to 0.3f after testing
        private const float playerSpeed = 3.0f;

        public float xVelocity = 0.0f;
        public float yVelocity = 0.0f;

        public void PlayerPhysics(Player player)
        {
            TerminalVelocity();
            GravityEffect();
            player.Position.SetPosition(new Vector2(player.Position.position.X + xVelocity, player.Position.position.Y + yVelocity));
        }

        private void TerminalVelocity()
        {
            if (xVelocity > maxXVelocity)
            {
                xVelocity = maxXVelocity;
            }
            else if (xVelocity < -maxXVelocity)
            {
                xVelocity = -maxXVelocity;
            }
            if (yVelocity > maxYVelocity)
            {
                yVelocity = maxYVelocity;
            }
            else if (yVelocity < -maxYVelocity)
            {
                yVelocity = -maxYVelocity;
            }
        }

        private void GravityEffect()
        {
            if (yVelocity < maxYVelocity)
            {
                yVelocity += Gravity;
            }
        }

        public void MoveLeft()
        {
            if (xVelocity >= -playerSpeed)
            {
                xVelocity -= playerSpeed / 3;
            }
        }

        public void MoveRight()
        {
            if (xVelocity <= playerSpeed)
            {
                xVelocity += playerSpeed / 3;
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

        public void ResetVelocity()
        {
            xVelocity = 0;
            yVelocity = 0;
        }

        public void KeepPlayerInbound(Player player, int xGameBorderMin, int xGameBorderMax, int yGameBorderMin)
        {
            if (player.Position.position.X < xGameBorderMin)
            {
                player.Position.SetPosition(new Vector2(xGameBorderMin, player.Position.position.Y));
            }
            if (player.Position.position.X + player.Texture.Width > xGameBorderMax)
            {
                player.Position.SetPosition(new Vector2(xGameBorderMax - player.Texture.Width, player.Position.position.Y));
            }
            if (player.Position.position.Y < yGameBorderMin)
            {
                player.Position.SetPosition(new Vector2(player.Position.position.X, yGameBorderMin));
                yVelocity /= 2;
            }
        }
    }
}
