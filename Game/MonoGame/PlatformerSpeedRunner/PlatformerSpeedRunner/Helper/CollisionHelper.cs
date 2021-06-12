using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Objects.Base;
using System;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.Helper
{
    public class CollisionHelper
    {
        public readonly List<RenderAbleObject> TopsCollisionList;
        public readonly List<RenderAbleObject> SidesCollisionList;
        public readonly List<RenderAbleObject> FullCollisionList;
        public readonly List<RenderAbleObject> DeathCollisionList;
        public readonly List<RenderAbleObject> RockHeadCollisionList;
        public readonly List<RenderAbleObject> SpikeHeadCollisionList;
        public readonly List<RenderAbleObject> EndFlagCollisionList;
        private CollisionDetector collisionDetector;

        public CollisionHelper ()
        {
            TopsCollisionList = new List<RenderAbleObject>();
            SidesCollisionList = new List<RenderAbleObject>();
            FullCollisionList = new List<RenderAbleObject>();
            DeathCollisionList = new List<RenderAbleObject>();
            RockHeadCollisionList = new List<RenderAbleObject>();
            SpikeHeadCollisionList = new List<RenderAbleObject>();
            EndFlagCollisionList = new List<RenderAbleObject>();
            collisionDetector = new CollisionDetector();
        }
        public void PlayerFullDetector(Player playerSprite, List<RenderAbleObject>FullCollisionList)
        {
            PlayerTopDetector(playerSprite, FullCollisionList);
            PlayerSideDetector(playerSprite, FullCollisionList);
        }

        public void PlayerTopDetector(Player playerSprite, List<RenderAbleObject> TopsCollisionList)
        {
            collisionDetector.DetectCollisions(playerSprite, TopsCollisionList, (player, Object) =>
            {
                if (IsPlayerOnTop(player, Object))
                {
                    player.Position.SetPosition(new Vector2(player.Position.position.X, Object.Position.position.Y - player.Texture.Height));
                    player.Movement.StopVerticalMovement();
                }
                else if (IsPlayerBelow(player, Object))
                {
                    player.Position.SetPosition(new Vector2(player.Position.position.X, Object.Position.position.Y + Object.Texture.Height));
                    player.Movement.StopVerticalMovement(true);
                }
            });
        }

        public void PlayerSideDetector(Player playerSprite, List<RenderAbleObject> SidesCollisionList)
        {
            collisionDetector.DetectCollisions(playerSprite, SidesCollisionList, (player, Object) =>
            {
                if (IsPlayerToRight(player, Object))
                {
                    player.Position.SetPosition(new Vector2(Object.Position.position.X + Object.Texture.Width, player.Position.position.Y));
                    player.Movement.StopHorizontalMovement();
                }
                else if (IsPlayerToLeft(player, Object))
                {
                    player.Position.SetPosition(new Vector2(Object.Position.position.X - player.Texture.Width, player.Position.position.Y));
                    player.Movement.StopHorizontalMovement();
                }
            });
        }

        public bool PlayerBooleanDetector(Player playerSprite, List<RenderAbleObject> CollisionList)
        {
            bool result = false;
            collisionDetector.DetectCollisions(playerSprite, CollisionList, (player, Object) =>
            {
                result = true;
            });
            return result;
        }

        public bool PlayerBooleanDetector(Player playerSprite, RenderAbleObject CollisionObject)
        {
            return collisionDetector.DetectCollision(playerSprite, CollisionObject);
        }

        public void PlayerRockHeadDetector(Player playerSprite, List<RenderAbleObject> RockHeadCollisionList)
        {
            collisionDetector.DetectCollisions(playerSprite, RockHeadCollisionList, (player, Object) =>
            {
                if (IsPlayerOnTop(player, Object))
                {
                    MovingRockHead rockHead = (MovingRockHead)Object;
                    rockHead.MakeRockheadMad();

                    player.Position.SetPosition(new Vector2(player.Position.position.X + rockHead.Movement.xVelocity, Object.Position.position.Y - player.Texture.Height));
                    player.Movement.StopVerticalMovement();
                }
                else if (IsPlayerBelow(player, Object))
                {
                    player.Position.SetPosition(new Vector2(player.Position.position.X, Object.Position.position.Y + Object.Texture.Height));
                    player.Movement.StopVerticalMovement(true);
                }
                else if (IsPlayerToRight(player, Object))
                {
                    player.Position.SetPosition(new Vector2(Object.Position.position.X + Object.Texture.Width, player.Position.position.Y));
                    player.Movement.StopHorizontalMovement();
                }
                else if (IsPlayerToLeft(player, Object))
                {
                    player.Position.SetPosition(new Vector2(Object.Position.position.X - player.Texture.Width, player.Position.position.Y));
                    player.Movement.StopHorizontalMovement();
                }
            });
        }

        private bool IsPlayerOnTop(Player player, RenderAbleObject Object)
        {
            if (Convert.ToInt32(player.Position.position.Y + player.Texture.Height) <= Object.Position.position.Y + 20 &&
                    Convert.ToInt32(player.Position.position.X + player.Texture.Width) > Object.Position.position.X &&
                    Convert.ToInt32(player.Position.position.X) < Object.Position.position.X + Object.Texture.Width)
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerBelow(Player player, RenderAbleObject Object)
        {
            if (Convert.ToInt32(player.Position.position.Y) >= Object.Position.position.Y + Object.Texture.Height - 20 &&
                        Convert.ToInt32(player.Position.position.X) <= Object.Position.position.X + Object.Texture.Width &&
                        Convert.ToInt32(player.Position.position.X + player.Texture.Width) >= Object.Position.position.X)
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerToLeft(Player player, RenderAbleObject Object)
        {
            if (Convert.ToInt32(player.Position.position.X + player.Texture.Width) < Object.Position.position.X + 20 &&
                        Convert.ToInt32(player.Position.position.Y + player.Texture.Height) > Object.Position.position.Y &&
                        Convert.ToInt32(player.Position.position.Y) < Object.Position.position.Y + Object.Texture.Height)
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerToRight(Player player, RenderAbleObject Object)
        {
            if (Convert.ToInt32(player.Position.position.X) >= Object.Position.position.X + 20 &&
                        Convert.ToInt32(player.Position.position.Y + player.Texture.Height) > Object.Position.position.Y &&
                        Convert.ToInt32(player.Position.position.Y) < Object.Position.position.Y + Object.Texture.Height)
            {
                return true;
            }
            return false;
        }
    }
}
