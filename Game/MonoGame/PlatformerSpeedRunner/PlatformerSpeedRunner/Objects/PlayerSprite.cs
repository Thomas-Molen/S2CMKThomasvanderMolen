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
        public CameraMode cameraState = CameraMode.Horizontal;

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

        private readonly Animation IdleAnimation;
        private readonly Animation RunningRightAnimation;
        private readonly Animation RunningLeftAnimation;
        private readonly Animation FallingRightAnimation;
        private readonly Animation FallingLeftAnimation;
        private readonly Animation JumpingRightAnimation;
        private readonly Animation JumpingLeftAnimation;
        private readonly Animation DeathAnimation;
        private const string idlePrefix = "Player\\Idle\\PlayerIdle";
        private readonly string[] IdleAnimationArray = { idlePrefix + 1, idlePrefix + 2, idlePrefix+3, idlePrefix+4, idlePrefix + 5, idlePrefix + 6, idlePrefix + 7, idlePrefix + 8, idlePrefix + 9, idlePrefix + 10, idlePrefix + 11 };
        private const string runningRightPrefix = "Player\\Running\\PlayerRunningRight";
        private readonly string[] RunningRightAnimationArray = { runningRightPrefix + 1, runningRightPrefix + 2, runningRightPrefix + 3, runningRightPrefix + 4, runningRightPrefix + 5, runningRightPrefix + 6, runningRightPrefix + 7, runningRightPrefix + 8, runningRightPrefix + 9, runningRightPrefix + 10, runningRightPrefix + 11, runningRightPrefix + 12 };
        private const string runningLeftPrefix = "Player\\Running\\PlayerRunningLeft";
        private readonly string[] RunningLeftAnimationArray = { runningLeftPrefix + 1, runningLeftPrefix + 2, runningLeftPrefix + 3, runningLeftPrefix + 4, runningLeftPrefix + 5, runningLeftPrefix + 6, runningLeftPrefix + 7, runningLeftPrefix + 8, runningLeftPrefix + 9, runningLeftPrefix + 10, runningLeftPrefix + 11, runningLeftPrefix + 12 };
        private readonly string[] FallingRightAnimationArray = { "Player\\FallingRight" };
        private readonly string[] FallingLeftAnimationArray = { "Player\\FallingLeft" };
        private readonly string[] JumpingRightAnimationArray = { "Player\\JumpingRight" };
        private readonly string[] JumpingLeftAnimationArray = { "Player\\JumpingLeft" };
        private const string deathAnimationPrefix = "Player\\Death\\Death";
        private readonly string[] DeathAnimationArray = { deathAnimationPrefix + 1, deathAnimationPrefix + 2, deathAnimationPrefix + 3, deathAnimationPrefix + 4, deathAnimationPrefix + 5, deathAnimationPrefix + 6 };

        public PlayerSprite(Texture2D texture)
        {
            baseTexture = texture;
            AddBoundingBox(new BoundingBox(new Vector2(BBPosX, BBPosY), BBWidth, BBHeight));
            animationState = Animations.Idle;

            RunningRightAnimation = new Animation(RunningRightAnimationArray, 24);
            RunningLeftAnimation = new Animation(RunningLeftAnimationArray, 24);
            IdleAnimation = new Animation(IdleAnimationArray, 55);
            FallingRightAnimation = new Animation(FallingRightAnimationArray, 1);
            FallingLeftAnimation = new Animation(FallingLeftAnimationArray, 1);
            JumpingRightAnimation = new Animation(JumpingRightAnimationArray, 1);
            JumpingLeftAnimation = new Animation(JumpingLeftAnimationArray, 1);
            DeathAnimation = new Animation(DeathAnimationArray, 12);
        }

        public void PlayerPhysics()
        {
            CheckAnimationState();
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
            return animationState switch
            {
                Animations.Death => DeathAnimation,
                Animations.Idle => IdleAnimation,
                Animations.RunningRight => RunningRightAnimation,
                Animations.RunningLeft => RunningLeftAnimation,
                Animations.FallingRight => FallingRightAnimation,
                Animations.FallingLeft => FallingLeftAnimation,
                Animations.JumpingRight => JumpingRightAnimation,
                Animations.JumpingLeft => JumpingLeftAnimation,
                _ => IdleAnimation,
            };
        }

        public void PlayerAnimation(Texture2D texture)
        {
            baseTexture = texture;
        }

        private void CheckAnimationState()
        {
            if (xVelocity == 0 && yVelocity == 0 && animationState != Animations.Idle)
            {
                animationState = Animations.Idle;
            }
            else if (yVelocity < 0 && xVelocity >= 0 &&animationState != Animations.JumpingRight)
            {
                animationState = Animations.JumpingRight;
            }
            else if (yVelocity < 0 && xVelocity <= 0 && animationState != Animations.JumpingRight)
            {
                animationState = Animations.JumpingLeft;
            }
            else if (yVelocity > 0 && xVelocity >= 0 && animationState != Animations.FallingRight)
            {
                animationState = Animations.FallingRight;
            }
            else if (yVelocity > 0 && xVelocity <= 0 && animationState != Animations.FallingRight)
            {
                animationState = Animations.FallingLeft;
            }
            else if (xVelocity > 0 && yVelocity == 0 && animationState != Animations.RunningRight)
            {
                animationState = Animations.RunningRight;
            }
            else if (xVelocity < 0 && yVelocity == 0 && animationState != Animations.RunningLeft)
            {
                animationState = Animations.RunningLeft;
            }
        }
    }
}