using ProyectoSDL2.Engine.Scripts;
using System;

namespace ProyectoSDL2.Engine.Scripts
{
    public class SpeedPowerUp : GameObject
    {
        private Image image;

        public SpeedPowerUp(int x, int y) : base(x, y)
        {
            image = Engine.LoadImage("assets/speed.png");
        }

        public override void Update()
        {
            Transform playerTransform = GameManager.Instance.Player.Transform;

            if (transform.PosX < playerTransform.PosX + 64 &&
                transform.PosX + 30 > playerTransform.PosX &&
                transform.PosY < playerTransform.PosY + 64 &&
                transform.PosY + 30 > playerTransform.PosY)
            {
                Engine.Debug("PowerUp Agarrado");

                GameManager.Instance.Player.ActivateSpeedBoost(3f, 8);

                this.IsActive = false;
            }
        }

        public override void Render()
        {
            Engine.Draw(image, transform.PosX, transform.PosY);
        }
    }
}