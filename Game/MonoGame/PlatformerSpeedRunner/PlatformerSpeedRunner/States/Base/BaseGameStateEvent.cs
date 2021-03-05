using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.States.Base
{
    public class BaseGameStateEvent
    {
        public class Nothing : BaseGameStateEvent { }
        public class GameQuit : BaseGameStateEvent { }
    }
}
