using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Objects;
using System.Collections.Generic;

namespace PlatformerSpeedRunner.Helper
{
    public class AnimationHelper
    {
        private int loopDurationHelper;
        
        private int animationSuffix;
        private TextureList Textures;
        private List<AnimationObject> animations;

        public AnimationHelper()
        {
            Textures = new TextureList();
            animations = new List<AnimationObject>();
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

        public void CreateAnimation (string animationPrefix, int animationLength = 1, int animationLoopDuration = 1)
        {
            animations.Add(new AnimationObject(animationPrefix, animationLength, animationLoopDuration));
        }

        public AnimationObject GetPlayerAnimation(Animations animationState)
        {
            return animationState switch
            {
                Animations.RunningRight => animations.Find(a => a.animationPrefix == Textures.playerRunningRight),
                Animations.RunningLeft => animations.Find(a => a.animationPrefix == Textures.playerRunningLeft),
                Animations.FallingRight => animations.Find(a => a.animationPrefix == Textures.playerFallingRight),
                Animations.FallingLeft => animations.Find(a => a.animationPrefix == Textures.playerFallingLeft),
                Animations.JumpingRight => animations.Find(a => a.animationPrefix == Textures.playerJumpingRight),
                Animations.JumpingLeft => animations.Find(a => a.animationPrefix == Textures.playerJumpingLeft),
                _ => animations.Find(a => a.animationPrefix == Textures.playerIdle),
            };
        }
    }
}
