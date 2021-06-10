using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States;
using PlatformerSpeedRunner.States.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerSpeedRunner.Helper
{
    class ChargeCircleHelper
    {
        private BasicObject chargeCircle;
        public ChargeCircleHelper(BasicObject chargeCircleToAdd)
        {
            chargeCircle = chargeCircleToAdd;
        }

        public void AnimateCharge(int timeCharged, Player player, BaseGameState gameplayState)
        {
            chargeCircle.Position.SetPosition(new Vector2(player.Position.position.X, player.Position.position.Y - 10));

            if (timeCharged < 10)
            {
                chargeCircle.Texture.SetTexture(gameplayState.LoadTexture("Player\\Charging\\ChargingCircle1"));
            }
            else if (timeCharged < 20)
            {
                chargeCircle.Texture.SetTexture(gameplayState.LoadTexture("Player\\Charging\\ChargingCircle2"));
            }
            else if (timeCharged < 30)
            {
                chargeCircle.Texture.SetTexture(gameplayState.LoadTexture("Player\\Charging\\ChargingCircle3"));
            }
            else if (timeCharged >= 30)
            {
                chargeCircle.Texture.SetTexture(gameplayState.LoadTexture("Player\\Charging\\ChargingCircle4"));
            }
        }
    }
}
