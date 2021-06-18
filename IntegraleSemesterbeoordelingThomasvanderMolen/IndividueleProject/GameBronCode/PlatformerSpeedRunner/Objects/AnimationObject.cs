using PlatformerSpeedRunner.Helper;

namespace PlatformerSpeedRunner.Objects
{
    public class AnimationObject
    {
        public string animationPrefix { get; private set; }
        public int animationLength { get; private set; }
        public int animationLoopDuration { get; private set; }

        public AnimationObject(string AnimationPrefix, int AnimationLength, int AnimationloopDuration)
        {
            animationPrefix = AnimationPrefix;
            animationLength = AnimationLength;
            animationLoopDuration = AnimationloopDuration;
        }
    }
}
