namespace PlatformerSpeedRunner.Objects
{
    public class AnimationObject
    {
        private int animationLength;
        private int animationLoopDuration;
        private int loopDurationHelper = 0;
        private string animationPrefix;
        private int animationSuffix = 1;

        public AnimationObject(string AnimationPrefix, int AnimationLength, int AnimationloopDuration)
        {
            animationLength = AnimationLength;
            animationPrefix = AnimationPrefix;
            animationLoopDuration = AnimationloopDuration;
        }

        public string GetAnimationSprite()
        {
            if (animationLength == 1)
            {
                return animationPrefix;
            }
            else
            {
                if (animationSuffix == animationLength)
                {
                    animationSuffix = 1;
                }
                else
                {
                    if (loopDurationHelper >= animationLoopDuration / animationLength)
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
            return animationPrefix + animationSuffix;
        }
    }
}
