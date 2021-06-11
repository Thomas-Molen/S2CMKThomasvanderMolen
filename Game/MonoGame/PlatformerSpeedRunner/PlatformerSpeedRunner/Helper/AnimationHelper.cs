using PlatformerSpeedRunner.Objects;

namespace PlatformerSpeedRunner.Helper
{
    public class AnimationHelper
    {
        private int loopDurationHelper;
        
        private int animationSuffix;

        public AnimationHelper()
        {
            loopDurationHelper = 0;
            animationSuffix = 1;
        }

        public string GetAnimation (AnimationObject animation)
        {
            if (animation.animationLength == 1)
            {
                return animation.animationPrefix;
            }
            else
            {
                if (animationSuffix == animation.animationLength)
                {
                    animationSuffix = 1;
                }
                else
                {
                    if (loopDurationHelper >= animation.animationLoopDuration / animation.animationLength)
                    {
                        animationSuffix++;
                        loopDurationHelper = 0;
                    }
                    else
                    {
                        loopDurationHelper++;
                    }
                }
            }
            return animation.animationPrefix + animationSuffix;
        }

        public AnimationObject CreateAnimation (string animationPrefix, int animationLength = 1, int animationLoopDuration = 1)
        {
            return new AnimationObject(animationPrefix, animationLength, animationLoopDuration);
        }
    }
}
