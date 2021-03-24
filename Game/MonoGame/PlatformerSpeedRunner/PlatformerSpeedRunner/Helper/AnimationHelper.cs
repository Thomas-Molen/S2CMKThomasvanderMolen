using PlatformerSpeedRunner.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class AnimationHelper
    {
        public string GetAnimation (Animation animation)
        {
            return animation.GetAnimationSprite();
        }

        public Animation CreateAnimation (string animationPrefix, int animationLength = 1, int animationLoopDuration = 1)
        {
            return new Animation(animationPrefix, animationLength, animationLoopDuration);
        }
    }
}
