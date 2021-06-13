using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;

namespace PlatformerSpeedRunner.Helper
{
    public class ChargeCircleHelper
    {
        private BasicObject chargeCircle;
        private TextureList Textures;
        public int timeCharged { get; private set; } = 0;
        public ChargeCircleHelper(BasicObject chargeCircleToAdd)
        {
            chargeCircle = chargeCircleToAdd;
            Textures = new TextureList();
        }

        public void AnimateCharge(Player player)
        {
            chargeCircle.Position.SetPosition(new Vector2(player.Position.position.X, player.Position.position.Y - 10));
            timeCharged++;

            if (timeCharged < 10)
            {
                chargeCircle.Texture.SetTexture(Textures.chargingCircle1);
            }
            else if (timeCharged < 20)
            {
                chargeCircle.Texture.SetTexture(Textures.chargingCircle2);
            }
            else if (timeCharged < 30)
            {
                chargeCircle.Texture.SetTexture(Textures.chargingCircle3);
            }
            else if (timeCharged >= 30)
            {
                chargeCircle.Texture.SetTexture(Textures.chargingCircle4);
            }
        }

        public void ResetCharge()
        {
            timeCharged = 0;
        }
    }
}
