using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Objects.Base;
using System;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.Helper
{
    public class CollisionHelper
    {
        private CollisionDetector collisionDetector = new CollisionDetector();
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
                    player.Movement.yVelocity = 0;
                }
                else if (IsPlayerBelow(player, Object))
                {
                    player.Position.SetPosition(new Vector2(player.Position.position.X, Object.Position.position.Y + Object.Texture.Height));
                    if (player.Movement.yVelocity < 0)
                    {
                        player.Movement.yVelocity = 0.1f;
                    }
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
                    if (player.Movement.xVelocity < 0)
                    {
                        player.Movement.xVelocity = 0;
                    }
                }
                else if (IsPlayerToLeft(player, Object))
                {
                    player.Position.SetPosition(new Vector2(Object.Position.position.X - player.Texture.Width, player.Position.position.Y));
                    if (player.Movement.xVelocity > 0)
                    {
                        player.Movement.xVelocity = 0;
                    }
                }
            });
        }

        public bool PlayerDeathDetector(Player playerSprite, List<RenderAbleObject> CollisionList)
        {
            bool result = false;
            collisionDetector.DetectCollisions(playerSprite, CollisionList, (player, Object) =>
            {
                result = true;
            });
            return result;
        }

        public bool PlayerSpikeHeadDetector(Player playerSprite, List<RenderAbleObject> CollisionList)
        {
            if (collisionDetector.DetectCollisions(playerSprite, CollisionList))
            {
                return true;
            }
            return false;
        }

        public void PlayerRockHeadDetector(Player playerSprite, List<RenderAbleObject> RockHeadCollisionList)
        {
            collisionDetector.DetectCollisions(playerSprite, RockHeadCollisionList, (player, Object) =>
            {
                if (IsPlayerOnTop(player, Object))
                {
                    MovingRockHead rockHead = (MovingRockHead)Object;
                    rockHead.MakeRockheadMad();

                    player.Position.SetPosition(new Vector2(player.Position.position.X + rockHead.velocity, Object.Position.position.Y - player.Texture.Height));
                    player.Movement.yVelocity = 0;
                }
                else if (IsPlayerBelow(player, Object))
                {
                    player.Position.SetPosition(new Vector2(player.Position.position.X, Object.Position.position.Y + Object.Texture.Height));
                    if (player.Movement.yVelocity < 0)
                    {
                        player.Movement.yVelocity = 0.1f;
                    }
                }
                else if (IsPlayerToRight(player, Object))
                {
                    player.Position.SetPosition(new Vector2(Object.Position.position.X + Object.Texture.Width, player.Position.position.Y));
                    if (player.Movement.xVelocity < 0)
                    {
                        player.Movement.xVelocity = 0;
                    }
                }
                else if (IsPlayerToLeft(player, Object))
                {
                    player.Position.SetPosition(new Vector2(Object.Position.position.X - player.Texture.Width, player.Position.position.Y));
                    if (player.Movement.xVelocity > 0)
                    {
                        player.Movement.xVelocity = 0;
                    }
                }
            });
        }

        public bool PlayerCheckPointDetector(Player playerSprite, CheckPoint checkPoint)
        {
            if (collisionDetector.DetectCollision(playerSprite, checkPoint) && !checkPoint.activated)
            {
                return true;
            }
            return false;
        }

        public bool PlayerEndFlagDetector(Player playerSprite, List<RenderAbleObject> EndFlagCollisionList)
        {
            bool result = false;
            collisionDetector.DetectCollisions(playerSprite, EndFlagCollisionList, (player, Object) =>
            {
                result = true;
            });
            return result;
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
