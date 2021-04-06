using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Objects
{
    public class CollisionDetector
    {
        private bool DetectCollision(PlayerSprite playerSprite, ObjectSprite objectSprite)
        {
            playerSprite.boundingBoxHelper.UpdateBoundingBoxes(playerSprite.positionHelper.position);
            foreach (var passiveBB in objectSprite.boundingBoxHelper.boundingBoxes)
            {
                foreach (var activeBB in playerSprite.boundingBoxHelper.boundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        //private bool DetectCollision(PlayerSprite playerSprite, SpikeHeadSprite spikeHeadSprite)
        //{
        //    foreach (var passiveBB in spikeHeadSprite.boundingBoxHelper.boundingBoxes)
        //    {
        //        foreach (var activeBB in playerSprite.boundingBoxHelper.boundingBoxes)
        //        {
        //            if (passiveBB.CollidesWith(activeBB))
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}
        //private bool DetectCollision(PlayerSprite playerSprite, RockHeadSprite rockHeadSprite)
        //{
        //    foreach (var passiveBB in rockHeadSprite.boundingBoxHelper.boundingBoxes)
        //    {
        //        foreach (var activeBB in playerSprite.boundingBoxHelper.boundingBoxes)
        //        {
        //            if (passiveBB.CollidesWith(activeBB))
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}
        //private bool DetectCollision(PlayerSprite playerSprite, CheckPointSprite checkPointSprite)
        //{
        //    foreach (var passiveBB in checkPointSprite.boundingBoxHelper.boundingBoxes)
        //    {
        //        foreach (var activeBB in playerSprite.boundingBoxHelper.boundingBoxes)
        //        {
        //            if (passiveBB.CollidesWith(activeBB))
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}

        public void DetectCollisions(PlayerSprite playerSprite, List<ObjectSprite> objectSprites, Action<PlayerSprite, ObjectSprite> collisionHandler)
        {
            foreach (var objectSprite in objectSprites)
            {
                if (DetectCollision(playerSprite, objectSprite))
                {
                    collisionHandler(playerSprite, objectSprite);
                }
            }
        }
        //public SpikeHeadSprite DetectCollisions(PlayerSprite playerSprite, List<SpikeHeadSprite> spikeHeadSprites)
        //{
        //    SpikeHeadSprite result = null;
        //    foreach (var spikeHeadSprite in spikeHeadSprites)
        //    {
        //        var testing = DetectCollision(playerSprite, spikeHeadSprite);
        //        if (testing)
        //        {
        //            result = spikeHeadSprite;
        //        }
        //    }
        //    return result;
        //}
        //public RockHeadSprite DetectCollisions(PlayerSprite playerSprite, List<RockHeadSprite> rockHeadSprites)
        //{
        //    RockHeadSprite result = null;
        //    foreach (var rockHeadSprite in rockHeadSprites)
        //    {
        //        if (DetectCollision(playerSprite, rockHeadSprite))
        //        {
        //            result = rockHeadSprite;
        //        }
        //    }
        //    return result;
        //}
        //public CheckPointSprite DetectCollisions(PlayerSprite playerSprite, List<CheckPointSprite> checkPointSprites)
        //{
        //    CheckPointSprite result = null;
        //    foreach (var checkPointSprite in checkPointSprites)
        //    {
        //        if (DetectCollision(playerSprite, checkPointSprite))
        //        {
        //            result = checkPointSprite;
        //        }
        //    }
        //    return result;
        //}
    }
}
