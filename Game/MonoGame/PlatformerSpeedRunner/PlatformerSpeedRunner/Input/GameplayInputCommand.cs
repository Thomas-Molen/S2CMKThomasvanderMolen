using System;
using System.Collections.Generic;
using System.Text;
using PlatformerSpeedRunner.Input.Base;

namespace PlatformerSpeedRunner.Input
{
    public class GameplayInputCommand : BaseInputCommand
    {
        public class ExitDown : GameplayInputCommand { }
        public class ExitUp : GameplayInputCommand { }
        public class PlayerMoveLeft : GameplayInputCommand { }
        public class PlayerMoveRight : GameplayInputCommand { }
        public class PlayerMoveUp : GameplayInputCommand { }
        public class PlayerMoveNone : GameplayInputCommand { }
        public class PlayerLMBHold : GameplayInputCommand { }
        public class PlayerLMBRelease : GameplayInputCommand { }
        public class RestartUp : GameplayInputCommand { }
        public class RestartDown : GameplayInputCommand { }
        public class DebugOn : GameplayInputCommand { }
        public class DebugOff : GameplayInputCommand { }
    }
}
