using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Objects
{
    public class CollisionDetector
    {
        private bool DetectCollision(Player playerSprite, BasicObject objectSprite)
        {
            playerSprite.BoundingBox.UpdateBoundingBoxes(playerSprite.Position.position);
            foreach (var passiveBB in objectSprite.BoundingBox.boundingBoxes)
            {
                foreach (var activeBB in playerSprite.BoundingBox.boundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool DetectCollision(Player playerSprite, MovingSpikehead spikeHeadSprite)
        {
            playerSprite.BoundingBox.UpdateBoundingBoxes(playerSprite.Position.position);
            spikeHeadSprite.BoundingBox.UpdateBoundingBoxes(spikeHeadSprite.Position.position);
            foreach (var passiveBB in spikeHeadSprite.BoundingBox.boundingBoxes)
            {
                foreach (var activeBB in playerSprite.BoundingBox.boundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool DetectCollision(Player playerSprite, MovingRockHead rockHeadSprite)
        {
            playerSprite.BoundingBox.UpdateBoundingBoxes(playerSprite.Position.position);
            rockHeadSprite.BoundingBox.UpdateBoundingBoxes(rockHeadSprite.Position.position);
            foreach (var passiveBB in rockHeadSprite.BoundingBox.boundingBoxes)
            {
                foreach (var activeBB in playerSprite.BoundingBox.boundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool DetectCollision(Player playerSprite, CheckPoint checkPointSprite)
        {
            playerSprite.BoundingBox.UpdateBoundingBoxes(playerSprite.Position.position);
            foreach (var passiveBB in checkPointSprite.BoundingBox.boundingBoxes)
            {
                foreach (var activeBB in playerSprite.BoundingBox.boundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void DetectCollisions(Player playerSprite, List<BasicObject> objectSprites, Action<Player, BasicObject> collisionHandler)
        {
            foreach (var objectSprite in objectSprites)
            {
                if (DetectCollision(playerSprite, objectSprite))
                {
                    collisionHandler(playerSprite, objectSprite);
                }
            }
        }
        public bool DetectCollisions(Player playerSprite, List<MovingSpikehead> spikeHeadSprites)
        {
            bool result = false;
            foreach (var spikeHeadSprite in spikeHeadSprites)
            {
                if (DetectCollision(playerSprite, spikeHeadSprite))
                {
                    result = true;
                }
            }
            return result;
        }
        public void DetectCollisions(Player playerSprite, List<MovingRockHead> rockHeadSprites, Action<Player, MovingRockHead> collisionHandler)
        {
            foreach (var rockHeadSprite in rockHeadSprites)
            {
                if (DetectCollision(playerSprite, rockHeadSprite))
                {
                    collisionHandler(playerSprite, rockHeadSprite);
                }
            }
        }
        public void DetectCollisions(Player playerSprite, List<CheckPoint> checkPointSprites, Action<Player, CheckPoint> collisionHandler)
        {
            foreach (var checkPointSprite in checkPointSprites)
            {
                if (DetectCollision(playerSprite, checkPointSprite))
                {
                    collisionHandler(playerSprite, checkPointSprite);
                }
            }
        }
    }
}
