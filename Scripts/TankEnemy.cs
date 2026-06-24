using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class TankEnemy : GameObject, IDamageable,IEnemy
    {
        int health = 10;
        int speed = 1;
        int preferredDistance = 300;
        float shootTimer = 0f;
        float shootCooldown = 2f;

        Image tankImg;
        Font arialFont;

        public event Action OnEnemyDied;

        public TankEnemy(int x, int y) : base(x, y)
        {
            tankImg = Engine.LoadImage("assets/tank_enemy.png");
            arialFont = Engine.LoadFont("Fonts/arial.ttf", 30);
        }

        public override void Update()
        {
            if (GameManager.Instance.Player != null) if (GameManager.Instance.Player != null)
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
        }

        void Shoot()
        {
            Transform p = GameManager.Instance.Player.Transform;
            GameManager.Instance.GetProjectile(
                transform.PosX, transform.PosY,
                p.PosX, p.PosY
            );
        }



        public void GetDamaged(int dmg)
        {
            health -= dmg;
            if (health <= 0)
            {
                this.IsActive = false;
                OnEnemyDied?.Invoke();    
            }
        }

        public override void Render()
        {
            Engine.DrawText(health.ToString(), transform.PosX + 13, transform.PosY - 30, 0, 0, 0, arialFont);
            Engine.Draw(tankImg, transform.PosX, transform.PosY);
        }
    }
}