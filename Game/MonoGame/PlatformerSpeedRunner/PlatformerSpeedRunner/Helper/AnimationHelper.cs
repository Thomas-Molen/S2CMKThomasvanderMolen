using PlatformerSpeedRunner.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    public class AnimationHelper
    {
        public string RunAnimation(Animation animation)
        {
            return animation.GetAnimationSprite();
        }
    }
}
