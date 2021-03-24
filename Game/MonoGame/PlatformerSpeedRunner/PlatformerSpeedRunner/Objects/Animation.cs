using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Objects
{
    public class Animation
    {
        private int animationLength;
        private int animationLoopDuration;
        private int loopDurationHelper = 0;
        private string animationPrefix;
        private int animationSuffix = 1;

        public Animation(string AnimationPrefix, int AnimationLength, int AnimationloopDuration)
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
