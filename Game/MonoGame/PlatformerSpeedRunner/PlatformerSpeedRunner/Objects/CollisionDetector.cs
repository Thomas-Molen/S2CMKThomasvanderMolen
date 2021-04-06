using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Objects
{
    public class CollisionDetector
    {
        private bool DetectCollision(PlayerSprite playerSprite, ObjectSprite objectSprite)
        {
            foreach (var passiveBB in objectSprite.BoundingBoxes)
            {
                foreach (var activeBB in playerSprite.BoundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public CollisionDetector(List<P> passiveObjects)
            {
                basePassiveObjects = passiveObjects;
            }

            public void DetectCollisions(A activeObject, Action<P, A> collisionHandler)
            {
                foreach (var passiveObject in basePassiveObjects)
                {
                    if (DetectCollision(passiveObject, activeObject))
                    {
                        collisionHandler(passiveObject, activeObject);
                    }
                }
            }

            public void DetectCollisions(List<A> activeObjects, Action<P, A> collisionHandler)
            {
                foreach (var passiveObject in basePassiveObjects)
                {
                    foreach (var activeObject in activeObjects)
                    {
                        if (DetectCollision(passiveObject, activeObject))
                        {
                            collisionHandler(passiveObject, activeObject);
                        }
                    }
                }
            }

            
    }
}
