﻿using System;
using System.Collections.Generic;
using System.Text;
using PlatformerSpeedRunner.Input.Base;

namespace PlatformerSpeedRunner.Input
{
    public class GameplayInputCommand : BaseInputCommand
    {
        public class GameExit : GameplayInputCommand { }
        public class PlayerMoveLeft : GameplayInputCommand { }
        public class PlayerMoveRight : GameplayInputCommand { }
        public class PlayerMoveNone : GameplayInputCommand { }
        public class PlayerLMB : GameplayInputCommand { }
        public class DebugOn : GameplayInputCommand { }
        public class DebugOff : GameplayInputCommand { }
    }
}
