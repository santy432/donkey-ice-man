using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class TankEnemy : GameObject, IDamageable
    {
        int health = 10;
        int speed = 1;
        int preferredDistance = 300;
        float shootTimer = 0f;
        float shootCooldown = 2f;

        Image tankImg;
        Font arialFont;

        public TankEnemy(int x, int y) : base(x, y)
        {
            tankImg = Engine.LoadImage("assets/tank_enemy.png");
            arialFont = Engine.LoadFont("Fonts/arial.ttf", 30);
        }

        public override void Update()
        {
            shootTimer += Program.DeltaTime;

            Transform playerTransform = GameManager.Instance.Player.Transform;
            int dx = playerTransform.PosX - transform.PosX;
            int distance = Math.Abs(dx);
            int direction = dx > 0 ? 1 : -1;

            if (distance > preferredDistance + 50)
                transform.Translate(speed * direction, 0);
            else if (distance < preferredDistance - 50)
                transform.Translate(-speed * direction, 0);

            if (transform.PosX < 0) transform.SetPosition(0, transform.PosY);
            if (transform.PosX > 1000) transform.SetPosition(1000, transform.PosY);

            if (shootTimer >= shootCooldown)
            {
                Shoot();
                shootTimer = 0f;
            }
        }

        void Shoot()
        {
            Transform playerTransform = GameManager.Instance.Player.Transform;
            GameManager.Instance.AddObject(new TankProjectile(
                    transform.PosX,
                    transform.PosY,
                    playerTransform.PosX,
                    playerTransform.PosY
                ));
        }



        public void GetDamaged(int dmg)
        {
            health -= dmg;
            Engine.Debug($"TankEnemy queda {health} de vida");
            if (health <= 0)
                this.IsActive = false;
        }

        public override void Render()
        {
            Engine.DrawText(health.ToString(), transform.PosX + 13, transform.PosY - 30, 0, 0, 0, arialFont);
            Engine.Draw(tankImg, transform.PosX, transform.PosY);
        }
    }
}