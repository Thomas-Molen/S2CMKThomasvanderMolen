using System;
using System.Collections.Generic;
using System.Text;
using PlatformerSpeedRunner.Input.Base;

namespace PlatformerSpeedRunner.Input
{
    public class SplashInputCommand : BaseInputCommand
    {
        public class GameSelect : SplashInputCommand { }
        public class GameExit : SplashInputCommand { }
        public class PlayerLMB : SplashInputCommand { }
    }
}
