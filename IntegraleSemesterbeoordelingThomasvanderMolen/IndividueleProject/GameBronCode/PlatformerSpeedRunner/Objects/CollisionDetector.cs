using PlatformerSpeedRunner.Objects.Base;
using System;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.Objects
{
    public class CollisionDetector
    {
        public bool DetectCollision(Player playerSprite, RenderAbleObject collisionObject)
        {
            playerSprite.BoundingBox.UpdateBoundingBoxes(playerSprite.Position.position);
            foreach (var passiveBB in collisionObject.BoundingBox.boundingBoxes)
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

        public void DetectCollisions(Player playerSprite, List<RenderAbleObject> objectSprites, Action<Player, RenderAbleObject> collisionHandler)
        {
            foreach (var objectSprite in objectSprites)
            {
                if (DetectCollision(playerSprite, objectSprite))
                {
                    collisionHandler(playerSprite, objectSprite);
                }
            }
        }
    }
}
