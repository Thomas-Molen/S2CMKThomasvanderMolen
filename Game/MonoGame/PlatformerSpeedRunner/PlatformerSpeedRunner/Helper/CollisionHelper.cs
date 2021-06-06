using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class CollisionHelper
    {
        private CollisionDetector collisionDetector = new CollisionDetector();
        public void PlayerFullDetector(Player playerSprite, List<BasicObject>FullCollisionList)
        {
            PlayerTopDetector(playerSprite, FullCollisionList);
            PlayerSideDetector(playerSprite, FullCollisionList);
        }

        public void PlayerTopDetector(Player playerSprite, List<BasicObject> TopsCollisionList)
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

        public void PlayerSideDetector(Player playerSprite, List<BasicObject> SidesCollisionList)
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

        public bool PlayerDeathDetector(Player playerSprite, List<BasicObject> DeathCollisionList)
        {
            bool result = false;
            collisionDetector.DetectCollisions(playerSprite, DeathCollisionList, (player, Object) =>
            {
                result = true;
            });
            return result;
        }

        public bool PlayerSpikeHeadDetector(Player playerSprite, List<MovingSpikeHead> SpikeHeadCollisionList)
        {
            bool result = false;
            if (collisionDetector.DetectCollisions(playerSprite, SpikeHeadCollisionList))
            {
                result = true;
            }
            return result;
        }

        public MovingRockHead PlayerRockHeadDetector(Player playerSprite, List<MovingRockHead> RockHeadCollisionList)
        {
            MovingRockHead result = null;
            collisionDetector.DetectCollisions(playerSprite, RockHeadCollisionList, (player, Object) =>
            {
                if (IsPlayerOnTop(player, Object))
                {
                    result = Object;

                    player.Position.SetPosition(new Vector2(player.Position.position.X + Object.velocity, Object.Position.position.Y - player.Texture.Height));
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
            return result;
        }

        public CheckPoint PlayerCheckPointDetector(Player playerSprite, List<CheckPoint> CheckPointCollisionList)
        {
            CheckPoint result = null;
            collisionDetector.DetectCollisions(playerSprite, CheckPointCollisionList, (player, Object) =>
            {
                if (Object.activated == false)
                {
                    result = Object;
                }
            });
            return result;
        }

        public bool PlayerEndFlagDetector(Player playerSprite, List<BasicObject> EndFlagCollisionList)
        {
            bool result = false;
            collisionDetector.DetectCollisions(playerSprite, EndFlagCollisionList, (player, Object) =>
            {
                result = true;
            });
            return result;
        }

        private bool IsPlayerOnTop(Player player, BasicObject Object)
        {
            if (Convert.ToInt32(player.Position.position.Y + player.Texture.Height) <= Object.Position.position.Y + 20 &&
                    Convert.ToInt32(player.Position.position.X + player.Texture.Width) > Object.Position.position.X &&
                    Convert.ToInt32(player.Position.position.X) < Object.Position.position.X + Object.Texture.Width)
            {
                return true;
            }
            return false;
        }
        private bool IsPlayerOnTop(Player player, MovingRockHead Object)
        {
            if (Convert.ToInt32(player.Position.position.Y + player.Texture.Height) <= Object.Position.position.Y + 20 &&
                    Convert.ToInt32(player.Position.position.X + player.Texture.Width) > Object.Position.position.X &&
                    Convert.ToInt32(player.Position.position.X) < Object.Position.position.X + Object.Texture.Width)
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerBelow(Player player, BasicObject Object)
        {
            if (Convert.ToInt32(player.Position.position.Y) >= Object.Position.position.Y + Object.Texture.Height - 20 &&
                        Convert.ToInt32(player.Position.position.X) <= Object.Position.position.X + Object.Texture.Width &&
                        Convert.ToInt32(player.Position.position.X + player.Texture.Width) >= Object.Position.position.X)
            {
                return true;
            }
            return false;
        }
        private bool IsPlayerBelow(Player player, MovingRockHead Object)
        {
            if (Convert.ToInt32(player.Position.position.Y) >= Object.Position.position.Y + Object.Texture.Height - 20 &&
                        Convert.ToInt32(player.Position.position.X) <= Object.Position.position.X + Object.Texture.Width &&
                        Convert.ToInt32(player.Position.position.X + player.Texture.Width) >= Object.Position.position.X)
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerToLeft(Player player, BasicObject Object)
        {
            if (Convert.ToInt32(player.Position.position.X + player.Texture.Width) < Object.Position.position.X + 20 &&
                        Convert.ToInt32(player.Position.position.Y + player.Texture.Height) > Object.Position.position.Y &&
                        Convert.ToInt32(player.Position.position.Y) < Object.Position.position.Y + Object.Texture.Height)
            {
                return true;
            }
            return false;
        }
        private bool IsPlayerToLeft(Player player, MovingRockHead Object)
        {
            if (Convert.ToInt32(player.Position.position.X + player.Texture.Width) < Object.Position.position.X + 20 &&
                        Convert.ToInt32(player.Position.position.Y + player.Texture.Height) > Object.Position.position.Y &&
                        Convert.ToInt32(player.Position.position.Y) < Object.Position.position.Y + Object.Texture.Height)
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerToRight(Player player, BasicObject Object)
        {
            if (Convert.ToInt32(player.Position.position.X) >= Object.Position.position.X + 20 &&
                        Convert.ToInt32(player.Position.position.Y + player.Texture.Height) > Object.Position.position.Y &&
                        Convert.ToInt32(player.Position.position.Y) < Object.Position.position.Y + Object.Texture.Height)
            {
                return true;
            }
            return false;
        }
        private bool IsPlayerToRight(Player player, MovingRockHead Object)
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
