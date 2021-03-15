using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects.Base;
using PlatformerSpeedRunner.Enum;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Helper;

namespace PlatformerSpeedRunner.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        public float xVelocity = 0.0f;
        public float yVelocity = 0.0f;
        private const float playerSpeed = 3.0f;
        private const float playerGravity = 0.3f;

        private const int maxXVelocity = 10;
        private const int maxYVelocity = 15;

        private const int BBPosX = 1;
        private const int BBPosY = 0;
        private const int BBWidth = 45;
        private const int BBHeight = 55;

        private Animation IdleAnimation;
        private Animation RunningRightAnimation;
        private Animation RunningLeftAnimation;
        private Animation FallingRightAnimation;
        private Animation JumpingRightAnimation;
        private string[] IdleAnimationArray = { "PlayerIdle1", "PlayerIdle2", "PlayerIdle3", "PlayerIdle4", "PlayerIdle5", "PlayerIdle6", "PlayerIdle7", "PlayerIdle8", "PlayerIdle9", "PlayerIdle10", "PlayerIdle11" };
        private string[] RunningRightAnimationArray = { "PlayerRunningRight1", "PlayerRunningRight2", "PlayerRunningRight3", "PlayerRunningRight4", "PlayerRunningRight5", "PlayerRunningRight6", "PlayerRunningRight7", "PlayerRunningRight8", "PlayerRunningRight9", "PlayerRunningRight10", "PlayerRunningRight11", "PlayerRunningRight12"};
        private string[] RunningLeftAnimationArray = { "PlayerRunningLeft1", "PlayerRunningLeft2", "PlayerRunningLeft3", "PlayerRunningLeft4", "PlayerRunningLeft5", "PlayerRunningLeft6", "PlayerRunningLeft7", "PlayerRunningLeft8", "PlayerRunningLeft9", "PlayerRunningLeft10", "PlayerRunningLeft11", "PlayerRunningLeft12" };
        private string[] FallingRightAnimationArray = { "FallingRight" };
        private string[] JumpingRightAnimationArray = { "JumpingRight" };

        public PlayerSprite(Texture2D texture)
        {
            baseTexture = texture;
            AddBoundingBox(new BoundingBox(new Vector2(BBPosX, BBPosY), BBWidth, BBHeight));
            animationState = Animations.Idle;

            RunningRightAnimation = new Animation(RunningRightAnimationArray, 24);
            RunningLeftAnimation = new Animation(RunningLeftAnimationArray, 24);
            IdleAnimation = new Animation(IdleAnimationArray, 55);
            FallingRightAnimation = new Animation(FallingRightAnimationArray, 1);
            JumpingRightAnimation = new Animation(JumpingRightAnimationArray, 1);
        }

        public void PlayerPhysics()
        {
            //animationState = Enum.Animations.Idle;
            GravityEffect();

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

            Position = new Vector2(Position.X + xVelocity, Position.Y + yVelocity);           
        }
        
        public void MoveLeft()
        {
            animationState = Animations.RunningLeft;
            if (xVelocity >= -playerSpeed)
            {
                xVelocity -= playerSpeed / 3;
            }
        }

        public void MoveRight()
        {
            animationState = Animations.RunningRight;
            if (xVelocity <= playerSpeed)
            {
                xVelocity += playerSpeed / 3;
            }
        }

        public void NoDirection()
        {
            if (xVelocity == 0 && yVelocity == 0)
            {
                animationState = Animations.Idle;
            }
            else if (yVelocity > 0)
            {
                animationState = Animations.Falling;
            }
            else
            {
                animationState = Animations.Jumping;
            }
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
            if (yVelocity < maxYVelocity)
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

            float yGrappleDistance = mouseState.Y - Position.Y;
            float xGrappleDistance;
            if (Position.X + Width/2 < Program.width / 2 && mouseState.X < Program.width / 2)
            {
                xGrappleDistance = mouseState.X - Position.X;
            }
            else if (Position.X + Width / 2 < Program.width / 2 && mouseState.X > Program.width / 2)
            {
                xGrappleDistance = mouseState.X - Position.X;
            }
            else
            {
                xGrappleDistance = mouseState.X - Program.width / 2;
            }
            if (yGrappleDistance < -700)
            {
                yVelocity += (-700 / 135) * timeCharged / 10;
            }
            else
            {
                yVelocity += (yGrappleDistance / 135) * timeCharged / 10;
            }
            xVelocity += (xGrappleDistance / 150) * timeCharged / 10;
        }

        public Animation GetAnimationState()
        {
            switch (animationState)
            {
                case Animations.Idle:
                    return IdleAnimation;
                case Animations.RunningRight:
                    return RunningRightAnimation;
                case Animations.RunningLeft:
                    return RunningLeftAnimation;
                case Animations.Falling:
                    return FallingRightAnimation;
                case Animations.Jumping:
                    return JumpingRightAnimation;
                default:
                    return IdleAnimation;
            }
        }

        public void PlayerAnimation(Texture2D texture)
        {
            baseTexture = texture;
        }
    }
}