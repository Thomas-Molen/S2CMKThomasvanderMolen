﻿using System;
using System.Collections.Generic;
using System.Text;
using PlatformerSpeedRunner.Input.Base;

namespace PlatformerSpeedRunner.Input
{
    public class SplashInputCommand : BaseInputCommand
    {
        public class GameSelect : SplashInputCommand { }
        public class GameExit : SplashInputCommand { }
        public class PlayerLMBPress : SplashInputCommand { }
        public class PlayerLMBRelease : SplashInputCommand { }
    }
}
