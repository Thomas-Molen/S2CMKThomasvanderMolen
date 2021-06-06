using PlatformerSpeedRunner.Objects;

namespace PlatformerSpeedRunner.Helper
{
    public class AnimationHelper
    {
        public string GetAnimation (AnimationObject animation)
        {
            return animation.GetAnimationSprite();
        }

        public AnimationObject CreateAnimation (string animationPrefix, int animationLength = 1, int animationLoopDuration = 1)
        {
            return new AnimationObject(animationPrefix, animationLength, animationLoopDuration);
        }
    }
}
