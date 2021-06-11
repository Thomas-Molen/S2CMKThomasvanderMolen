using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerSpeedRunner.Camera;
using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class Player : RenderAbleObject
    {
        public PlayerMovementHelper Movement = new PlayerMovementHelper();
        private AnimationHelper Animation = new AnimationHelper();
        public CameraMode cameraState = CameraMode.Horizontal;
        private Animations AnimationState;

        private const int BBWidth = 45;
        private const int BBHeight = 55;

        private readonly AnimationObject IdleAnimation;
        private readonly AnimationObject RunningRightAnimation;
        private readonly AnimationObject RunningLeftAnimation;
        private readonly AnimationObject FallingRightAnimation;
        private readonly AnimationObject FallingLeftAnimation;
        private readonly AnimationObject JumpingRightAnimation;
        private readonly AnimationObject JumpingLeftAnimation;
        private readonly AnimationObject DeathAnimation;

        public Player(ContentManager contentManager = null)
        {
            if (contentManager != null)
            {
                SetTextureContentManager(contentManager);
                Texture.SetTexture(Texture.GetTexture2D("Player\\Idle\\IdlePinkMan"));
            }
            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(1, 0), BBWidth, BBHeight));
            AnimationState = Animations.Idle;

            RunningRightAnimation = Animation.CreateAnimation("Player\\Running\\PlayerRunningRight", 12, 24);
            RunningLeftAnimation = Animation.CreateAnimation("Player\\Running\\PlayerRunningLeft", 12, 24);
            IdleAnimation = Animation.CreateAnimation("Player\\Idle\\PlayerIdle", 11, 55);
            FallingRightAnimation = Animation.CreateAnimation("Player\\FallingRight");
            FallingLeftAnimation = Animation.CreateAnimation("Player\\FallingLeft");
            JumpingRightAnimation = Animation.CreateAnimation("Player\\JumpingRight");
            JumpingLeftAnimation = Animation.CreateAnimation("Player\\JumpingLeft");
            DeathAnimation = Animation.CreateAnimation("Player\\Death\\Death", 6, 12);
        }

        public void PlayerUpdate()
        {
            SetCorrectAnimation();
            Movement.PlayerPhysics(this);
        }

        public void Grapple(int timeCharged, CameraHelper camera)
        {
            if (timeCharged < 10)
            {
                return;
            }
            else if (timeCharged >= 30)
            {
                Movement.Grapple(30, this, camera);
            }
            else
            {
                Movement.Grapple(timeCharged, this, camera);
            }
        }

        public AnimationObject GetAnimationState()
        {
            return AnimationState switch
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

        private void SetCorrectAnimation()
        {
            if (Movement.xVelocity == 0 && Movement.yVelocity == 0 && AnimationState != Animations.Idle)
            {
                AnimationState = Animations.Idle;
            }
            else if (Movement.yVelocity < 0 && Movement.xVelocity >= 0 &&AnimationState != Animations.JumpingRight)
            {
                AnimationState = Animations.JumpingRight;
            }
            else if (Movement.yVelocity < 0 && Movement.xVelocity <= 0 && AnimationState != Animations.JumpingRight)
            {
                AnimationState = Animations.JumpingLeft;
            }
            else if (Movement.yVelocity > 0 && Movement.xVelocity >= 0 && AnimationState != Animations.FallingRight)
            {
                AnimationState = Animations.FallingRight;
            }
            else if (Movement.yVelocity > 0 && Movement.xVelocity <= 0 && AnimationState != Animations.FallingRight)
            {
                AnimationState = Animations.FallingLeft;
            }
            else if (Movement.xVelocity > 0 && Movement.yVelocity == 0 && AnimationState != Animations.RunningRight)
            {
                AnimationState = Animations.RunningRight;
            }
            else if (Movement.xVelocity < 0 && Movement.yVelocity == 0 && AnimationState != Animations.RunningLeft)
            {
                AnimationState = Animations.RunningLeft;
            }
        }
    }
}