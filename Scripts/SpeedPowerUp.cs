using ProyectoSDL2.Engine.Scripts;
using System;

namespace ProyectoSDL2.Engine.Scripts
{
    public class SpeedPowerUp : GameObject
    {
        private Image image;
        public event Action OnPickedUp;
        
        public SpeedPowerUp(int x, int y) : base(x, y)
        {
            image = Engine.LoadImage("assets/speed.png");
        }

        public override void Update()
        {
            Transform playerTransform = GameManager.Instance.Player.Transform;

            if (CollisionHelper.Overlaps(
                transform.PosX, transform.PosY, 30, 30,                // powerup: 30x30
                playerTransform.PosX, playerTransform.PosY, 64, 64))   // player: 64x64

            {
                Engine.Debug("PowerUp Agarrado");

                OnPickedUp?.Invoke();

                this.IsActive = false;
            }
        }

        public override void Render()
        {
            Engine.Draw(image, transform.PosX, transform.PosY);
        }
    }
}