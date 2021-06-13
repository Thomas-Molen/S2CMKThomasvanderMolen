using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class Player : RenderAbleObject
    {
        public PlayerMovementHelper Movement = new PlayerMovementHelper();
        private AnimationHelper Animation = new AnimationHelper();
        private Animations movementState;
        private Vector2 spawnPoint;

        private const int BBWidth = 45;
        private const int BBHeight = 55;

        public Player(ContentManager contentManager = null)
        {
            spawnPoint = new Vector2(200, 750);
            Position.SetPosition(spawnPoint);
            if (contentManager != null)
            {
                SetTextureContentManager(contentManager);
                Texture.SetTexture(Textures.playerIdle);
            }
            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(1, 0), BBWidth, BBHeight));

            Animation.CreateAnimation(Textures.playerRunningRight, 12, 24);
            Animation.CreateAnimation(Textures.playerRunningLeft, 12, 24);
            Animation.CreateAnimation(Textures.playerIdle, 11, 55);
            Animation.CreateAnimation(Textures.playerFallingRight);
            Animation.CreateAnimation(Textures.playerFallingLeft);
            Animation.CreateAnimation(Textures.playerJumpingRight);
            Animation.CreateAnimation(Textures.playerJumpingLeft);
        }

        public void PlayerUpdate()
        {
            SetMovementState();
            Texture.SetTexture(Texture.GetTexture2D(Animation.GetAnimation(Animation.GetPlayerAnimation(movementState))));
            Movement.PlayerPhysics(this);
        }

        public void SetSpawnPoint(Vector2 newSpawnPoint)
        {
            spawnPoint = newSpawnPoint;
        }

        public void Respawn()
        {
            Movement.ResetVelocity();
            Position.SetPosition(spawnPoint);
        }

        private void SetMovementState()
        {
            if (Movement.xVelocity == 0 && Movement.yVelocity == 0 && movementState != Animations.Idle)
            {
                movementState = Animations.Idle;
            }
            else if (Movement.yVelocity < 0 && Movement.xVelocity >= 0 && movementState != Animations.JumpingRight)
            {
                movementState = Animations.JumpingRight;
            }
            else if (Movement.yVelocity < 0 && Movement.xVelocity <= 0 && movementState != Animations.JumpingRight)
            {
                movementState = Animations.JumpingLeft;
            }
            else if (Movement.yVelocity > 0 && Movement.xVelocity >= 0 && movementState != Animations.FallingRight)
            {
                movementState = Animations.FallingRight;
            }
            else if (Movement.yVelocity > 0 && Movement.xVelocity <= 0 && movementState != Animations.FallingRight)
            {
                movementState = Animations.FallingLeft;
            }
            else if (Movement.xVelocity > 0 && Movement.yVelocity == 0 && movementState != Animations.RunningRight)
            {
                movementState = Animations.RunningRight;
            }
            else if (Movement.xVelocity < 0 && Movement.yVelocity == 0 && movementState != Animations.RunningLeft)
            {
                movementState = Animations.RunningLeft;
            }
        }
    }
}