using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class CollisionHelper
    {
        public void PlayerFullDetector(PlayerSprite playerSprite, List<ObjectSprite>FullCollisionList)
        {
            var playerFullDetector = new CollisionDetector();

            playerFullDetector.DetectCollisions(playerSprite, FullCollisionList, (player, Object) =>
            {
                if (Convert.ToInt32(player.GetPosition().Y + player.textureHelper.Height) <= Object.positionHelper.position.Y + 20 &&
                    Convert.ToInt32(player.GetPosition().X + player.textureHelper.Width) > Object.positionHelper.position.X &&
                    Convert.ToInt32(player.GetPosition().X) < Object.positionHelper.position.X + Object.textureHelper.Width)
                {
                    player.SetPosition(new Vector2(player.GetPosition().X, Object.positionHelper.position.Y - player.textureHelper.Height));
                    player.yVelocity = 0;
                }
                else if (Convert.ToInt32(player.GetPosition().Y) >= Object.positionHelper.position.Y + Object.textureHelper.Height - 20 &&
                        Convert.ToInt32(player.GetPosition().X) <= Object.positionHelper.position.X + Object.textureHelper.Width &&
                        Convert.ToInt32(player.GetPosition().X + player.textureHelper.Width) >= Object.positionHelper.position.X)
                {
                    player.SetPosition(new Vector2(player.GetPosition().X, Object.positionHelper.position.Y + Object.textureHelper.Height));
                    if (player.yVelocity < 0)
                    {
                        player.yVelocity = 0.1f;
                    }
                }
                else if (Convert.ToInt32(player.positionHelper.position.X) >= Object.positionHelper.position.X + 20 &&
                        Convert.ToInt32(player.positionHelper.position.Y + player.textureHelper.Height) > Object.positionHelper.position.Y &&
                        Convert.ToInt32(player.positionHelper.position.Y) < Object.positionHelper.position.Y + Object.textureHelper.Height)
                {
                    player.SetPosition(new Vector2(Object.positionHelper.position.X + Object.textureHelper.Width, player.positionHelper.position.Y));
                    if (player.xVelocity < 0)
                    {
                        player.xVelocity = 0;
                    }
                }
                else if (Convert.ToInt32(player.positionHelper.position.X + player.textureHelper.Width) < Object.positionHelper.position.X + 20 &&
                        Convert.ToInt32(player.positionHelper.position.Y + player.textureHelper.Height) > Object.positionHelper.position.Y &&
                        Convert.ToInt32(player.positionHelper.position.Y) < Object.positionHelper.position.Y + Object.textureHelper.Height)
                {
                    player.SetPosition(new Vector2(Object.positionHelper.position.X - player.textureHelper.Width, player.positionHelper.position.Y));
                    if (player.xVelocity > 0)
                    {
                        player.xVelocity = 0;
                    }
                }
            });
        }

        public void PlayerTopDetector(PlayerSprite playerSprite, List<ObjectSprite> TopsCollisionList)
        {
            var playerTopsDetector = new CollisionDetector();

            playerTopsDetector.DetectCollisions(playerSprite, TopsCollisionList, (player, Object) =>
            {
                if (Convert.ToInt32(player.positionHelper.position.Y + player.textureHelper.Height) <= Object.positionHelper.position.Y + 20 &&
                    Convert.ToInt32(player.positionHelper.position.X + player.textureHelper.Width) > Object.positionHelper.position.X &&
                    Convert.ToInt32(player.positionHelper.position.X) < Object.positionHelper.position.X + Object.textureHelper.Width)
                {
                    player.SetPosition(new Vector2(player.positionHelper.position.X, Object.positionHelper.position.Y - player.textureHelper.Height));
                    player.yVelocity = 0;
                }
                else if (Convert.ToInt32(player.positionHelper.position.Y) >= Object.positionHelper.position.Y + Object.textureHelper.Height - 20 &&
                        Convert.ToInt32(player.positionHelper.position.X) <= Object.positionHelper.position.X + Object.textureHelper.Width &&
                        Convert.ToInt32(player.positionHelper.position.X + player.textureHelper.Width) >= Object.positionHelper.position.X)
                {
                    player.SetPosition(new Vector2(player.positionHelper.position.X, Object.positionHelper.position.Y + Object.textureHelper.Height));
                    if (player.yVelocity < 0)
                    {
                        player.yVelocity = 0.1f;
                    }
                }
            });
        }
            
        public void PlayerSideDetector(PlayerSprite playerSprite, List<ObjectSprite> SidesCollisionList)
        {
            var playerSidesDetector = new CollisionDetector();

            playerSidesDetector.DetectCollisions(playerSprite, SidesCollisionList, (player, Object) =>
            {
                if (Convert.ToInt32(player.positionHelper.position.X) >= Object.positionHelper.position.X + 20 &&
                        Convert.ToInt32(player.positionHelper.position.Y + player.textureHelper.Height) > Object.positionHelper.position.Y &&
                        Convert.ToInt32(player.positionHelper.position.Y) < Object.positionHelper.position.Y + Object.textureHelper.Height)
                {
                    player.SetPosition(new Vector2(Object.positionHelper.position.X + Object.textureHelper.Width, player.positionHelper.position.Y));
                    if (player.xVelocity < 0)
                    {
                        player.xVelocity = 0;
                    }
                }
                else if (Convert.ToInt32(player.positionHelper.position.X + player.textureHelper.Width) < Object.positionHelper.position.X + 20 &&
                        Convert.ToInt32(player.positionHelper.position.Y + player.textureHelper.Height) > Object.positionHelper.position.Y &&
                        Convert.ToInt32(player.positionHelper.position.Y) < Object.positionHelper.position.Y + Object.textureHelper.Height)
                {
                    player.SetPosition(new Vector2(Object.positionHelper.position.X - player.textureHelper.Width, player.positionHelper.position.Y));
                    if (player.xVelocity > 0)
                    {
                        player.xVelocity = 0;
                    }
                }
            });
        }

        //public bool PlayerDeathDetector(PlayerSprite playerSprite, List<ObjectSprite> DeathCollisionList)
        //{
        //    bool result = false;
        //    var playerDeathDetector = new CollisionDetector<ObjectSprite, PlayerSprite>(DeathCollisionList);

        //    playerDeathDetector.DetectCollisions(playerSprite, (Object, player) =>
        //    {
        //        result = true;
        //    });
        //    return result;
        //}

        //public bool PlayerSpikeHeadDetector(PlayerSprite playerSprite, List<SpikeHeadSprite> SpikeHeadCollisionList)
        //{
        //    bool result = false;
        //    var playerSpikeHeadDetector = new CollisionDetector<SpikeHeadSprite, PlayerSprite>(SpikeHeadCollisionList);

        //    playerSpikeHeadDetector.DetectCollisions(playerSprite, (Object, player) =>
        //    {
        //        result = true;
        //    });
        //    return result;
        //}

        //public RockHeadSprite PlayerRockHeadDetector(PlayerSprite playerSprite, List<RockHeadSprite> RockHeadCollisionList)
        //{
        //    RockHeadSprite result = null;
        //    var playerRockHeadDetector = new CollisionDetector<RockHeadSprite, PlayerSprite>(RockHeadCollisionList);

        //    playerRockHeadDetector.DetectCollisions(playerSprite, (Object, player) =>
        //    {
        //        if (Convert.ToInt32(player.positionHelper.position.Y + player.textureHelper.Height) <= Object.positionHelper.position.Y + 20 &&
        //            Convert.ToInt32(player.positionHelper.position.X + player.textureHelper.Width) > Object.positionHelper.position.X &&
        //            Convert.ToInt32(player.positionHelper.position.X) < Object.positionHelper.position.X + Object.textureHelper.Width)
        //        {
        //            result = Object;

        //            player.SetPosition(new Vector2(player.positionHelper.position.X + Object.velocity, Object.positionHelper.position.Y - player.textureHelper.Height));
        //            player.yVelocity = 0;
        //        }
        //        else if (Convert.ToInt32(player.positionHelper.position.Y) >= Object.positionHelper.position.Y + Object.textureHelper.Height - 20 &&
        //                Convert.ToInt32(player.positionHelper.position.X) <= Object.positionHelper.position.X + Object.textureHelper.Width &&
        //                Convert.ToInt32(player.positionHelper.position.X + player.textureHelper.Width) >= Object.positionHelper.position.X)
        //        {
        //            player.SetPosition(new Vector2(player.positionHelper.position.X, Object.positionHelper.position.Y + Object.textureHelper.Height));
        //            if (player.yVelocity < 0)
        //            {
        //                player.yVelocity = 0.1f;
        //            }
        //        }
        //        else if (Convert.ToInt32(player.positionHelper.position.X) >= Object.positionHelper.position.X + 20 &&
        //                Convert.ToInt32(player.positionHelper.position.Y + player.textureHelper.Height) > Object.positionHelper.position.Y &&
        //                Convert.ToInt32(player.positionHelper.position.Y) < Object.positionHelper.position.Y + Object.textureHelper.Height)
        //        {
        //            player.SetPosition(new Vector2(Object.positionHelper.position.X + Object.textureHelper.Width, player.positionHelper.position.Y));
        //            if (player.xVelocity < 0)
        //            {
        //                player.xVelocity = 0;
        //            }
        //        }
        //        else if (Convert.ToInt32(player.positionHelper.position.X + player.textureHelper.Width) < Object.positionHelper.position.X + 20 &&
        //                Convert.ToInt32(player.positionHelper.position.Y + player.textureHelper.Height) > Object.positionHelper.position.Y &&
        //                Convert.ToInt32(player.positionHelper.position.Y) < Object.positionHelper.position.Y + Object.textureHelper.Height)
        //        {
        //            player.SetPosition(new Vector2(Object.positionHelper.position.X - player.textureHelper.Width, player.positionHelper.position.Y));
        //            if (player.xVelocity > 0)
        //            {
        //                player.xVelocity = 0;
        //            }
        //        }
        //    });
        //    return result;
        //}

        //public CheckPointSprite PlayerCheckPointDetector(PlayerSprite playerSprite, List<CheckPointSprite> CheckPointCollisionList)
        //{
        //    CheckPointSprite result = null;
        //    var playerCheckPointDetector = new CollisionDetector<CheckPointSprite, PlayerSprite>(CheckPointCollisionList);
        //    playerCheckPointDetector.DetectCollisions(playerSprite, (Object, player) =>
        //    {
        //        if (Object.activated == false)
        //        {
        //            result = Object;
        //        }
        //    });
        //    return result;
        //}

        //public bool PlayerEndFlagDetector(PlayerSprite playerSprite, List<ObjectSprite> EndFlagCollisionList)
        //{
        //    bool result = false;
        //    var playerEndFlagDetector = new CollisionDetector<ObjectSprite, PlayerSprite>(EndFlagCollisionList);

        //    playerEndFlagDetector.DetectCollisions(playerSprite, (Object, player) =>
        //    {
        //        result = true;
        //    });
        //    return result;
        //}
    }
}
