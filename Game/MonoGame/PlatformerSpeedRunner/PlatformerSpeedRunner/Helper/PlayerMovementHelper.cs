using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PlatformerSpeedRunner.Camera;
using PlatformerSpeedRunner.Objects;

namespace PlatformerSpeedRunner.Helper
{
    public class PlayerMovementHelper
    {
        private const int maxXVelocity = 10;
        private const int maxYVelocity = 15;
        private const float Gravity = 0.4f;
        private const float playerSpeed = 3.0f;

        public float xVelocity { get; private set; } = 0.0f;
        public float yVelocity { get; private set; } = 0.0f;

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

        public void StopVerticalMovement(bool checkForHeadCollision = false)
        {
            if (checkForHeadCollision)
            {
                if (yVelocity < 0)
                {
                    yVelocity = 0.1f;
                }
            }
            else
            {
                yVelocity = 0;
            }
        }

        public void StopHorizontalMovement()
        {
            xVelocity = 0;
        }

        public void Grapple(int timeCharged, Player player, CameraHelper camera)
        {
            if (timeCharged < 10)
            {
                return;
            }
            else if (timeCharged >= 30)
            {
                CalculateGrappleForce(30, player, camera);
            }
            else
            {
                CalculateGrappleForce(timeCharged, player, camera);
            }
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

        private void CalculateGrappleForce(int timeCharged, Player player, CameraHelper camera)
        {
            MouseState mouseState = Mouse.GetState();
            float yGrappleDistance = mouseState.Y - player.Position.position.Y;

            if (yGrappleDistance < -700)
            {
                yVelocity += -700 / 135 * timeCharged / 10;
            }
            else
            {
                yVelocity += yGrappleDistance / 135 * timeCharged / 10;
            }
            xVelocity += (-(camera.transform.Translation.X + 23) + mouseState.X - (player.Position.position.X + (player.Texture.Width / 2))) / 150 * timeCharged / 10;
        }
    }
}
