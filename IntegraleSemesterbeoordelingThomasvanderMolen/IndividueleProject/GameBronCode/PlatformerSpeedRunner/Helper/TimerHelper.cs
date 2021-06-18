using Microsoft.Xna.Framework;
using System;

namespace PlatformerSpeedRunner.Helper
{
    public class TimerHelper
    {
        public TimeSpan elapsedTime { get; private set; }
        private TimeSpan startingTime;
        private TimeSpan checkPointTime;
        private GameTime localGameTime;

        public TimerHelper()
        {
            localGameTime = new GameTime();
            checkPointTime = new TimeSpan(0);
        }

        public void UpdateTimer(GameTime gameTime)
        {
            if (localGameTime.ElapsedGameTime == new TimeSpan(0))
            {
                startingTime = gameTime.TotalGameTime;
                localGameTime = gameTime;
            }
            else
            {
                localGameTime = gameTime;
            }
            elapsedTime = gameTime.TotalGameTime - startingTime;
        }

        public string GetElapsedTimeString()
        {
           return elapsedTime.ToString().Substring(0, elapsedTime.ToString().Length - 4);
        }

        public void ResetCheckpointTimer()
        {
            startingTime += elapsedTime;
            startingTime += -checkPointTime;
        }

        public void SetCheckpointTime()
        {
            checkPointTime = elapsedTime;
        }
    }
}
