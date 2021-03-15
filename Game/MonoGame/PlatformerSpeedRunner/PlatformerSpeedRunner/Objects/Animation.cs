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
        private int animationLocation = 0;
        private int animationLoopDuration;
        private int loopDurationHelper = 0;
        private string[] animationSprites;

        public Animation(string[] AnimationArray, int AnimationloopDuration)
        {
            animationLength = AnimationArray.Length - 1;
            animationSprites = AnimationArray;
            animationLoopDuration = AnimationloopDuration;
        }

        public string GetAnimationSprite()
        {
            string result = animationSprites[animationLocation];
            if (animationLocation == animationLength)
            {
                animationLocation = 0;
            }
            else
            {
                if (loopDurationHelper >= animationLoopDuration/animationLength)
                {
                    animationLocation++;
                    loopDurationHelper = 0;
                }
                else
                {
                    loopDurationHelper++;
                }
            }
            return result;
        }
    }
}
