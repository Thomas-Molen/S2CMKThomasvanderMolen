using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.States.Base;

namespace PlatformerSpeedRunner.Helper
{
    class ChargeCircleHelper
    {
        private BasicObject chargeCircle;
        public ChargeCircleHelper(BasicObject chargeCircleToAdd)
        {
            chargeCircle = chargeCircleToAdd;
        }

        public void AnimateCharge(int timeCharged, Player player)
        {
            chargeCircle.Position.SetPosition(new Vector2(player.Position.position.X, player.Position.position.Y - 10));

            if (timeCharged < 10)
            {
                chargeCircle.Texture.SetTexture(chargeCircle.Texture.GetTexture2D("Player\\Charging\\ChargingCircle1"));
            }
            else if (timeCharged < 20)
            {
                chargeCircle.Texture.SetTexture(chargeCircle.Texture.GetTexture2D("Player\\Charging\\ChargingCircle2"));
            }
            else if (timeCharged < 30)
            {
                chargeCircle.Texture.SetTexture(chargeCircle.Texture.GetTexture2D("Player\\Charging\\ChargingCircle3"));
            }
            else if (timeCharged >= 30)
            {
                chargeCircle.Texture.SetTexture(chargeCircle.Texture.GetTexture2D("Player\\Charging\\ChargingCircle4"));
            }
        }
    }
}
