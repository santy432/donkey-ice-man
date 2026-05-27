using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class TankProjectile : GameObject
    {
        float velX;
        float velY;
        float accumulatedX;
        float accumulatedY;

        //base(startPosX, startPosY) para que el padre inicialice el transform
        public TankProjectile(int startPosX, int startPosY, int targetX, int targetY) : base(startPosX, startPosY)
        {
            int dx = targetX - startPosX;
            int dy = targetY - startPosY;
            float gravity = 0.3f;
            float launchVelY = -10f;

            float a = 0.5f * gravity;
            float b = launchVelY;
            float c = -dy;

            float discriminant = (b * b) - (4 * a * c);
            float timeOfFlight = (-b + MathF.Sqrt(discriminant)) / (2 * a);

            velX = dx / timeOfFlight;
            velY = launchVelY;
        }

        public override void Update()
        {
            float gravity = 0.3f;
            velY += gravity;

            accumulatedX += velX;
            accumulatedY += velY;

            int moveX = (int)accumulatedX;
            int moveY = (int)accumulatedY;

            if (moveX != 0 || moveY != 0)
            {
                transform.Translate(moveX, moveY);
                accumulatedX -= moveX;
                accumulatedY -= moveY;
            }

            // buscamos al jugador a traves de GameManager
            Transform playerTransform = GameManager.Instance.Player.Transform;

            if (transform.PosX < playerTransform.PosX + 64 &&
                transform.PosX + 20 > playerTransform.PosX &&
                transform.PosY < playerTransform.PosY + 64 &&
                transform.PosY + 20 > playerTransform.PosY)
            {
                // damage a traves de gamemanager
                GameManager.Instance.Player.TakeDamage(1);

                this.IsActive = false;
                return;
            }

            if (transform.PosX < -100 || transform.PosX > 1200 || transform.PosY > 900 || transform.PosY < -200)
            {
                this.IsActive = false;
            }
        }

        public override void Render()
        {
            Engine.Draw("assets/hammer.png", transform.PosX, transform.PosY);
        }
    }
}