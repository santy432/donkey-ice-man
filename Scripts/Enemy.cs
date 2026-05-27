
namespace ProyectoSDL2.Engine.Scripts
{
    public class Enemy : GameObject, IDamageable
    {
        int speed = 3;
        Image playerImg;

        int health = 5;

        Font arialFont;

        public Enemy(int x, int y) : base(x, y)
        {
            transform = new Transform(x, y);
            playerImg = Engine.LoadImage("assets/enemy.png");
            arialFont = Engine.LoadFont("Fonts/arial.ttf", 30);
        }

        public override void Update()
        {
            transform.Translate(speed, 0);

            if (transform.PosX > 800 || transform.PosX < 200)
            {
                speed *= -1;
            }

        }

        public void GetDamaged(int dmg)
        {
            health -= dmg;
            Engine.Debug($"Queda {health} de vida");
            if (health <= 0)
            {
                // desactivamos
                this.IsActive = false;
            }
        }

        public override void Render()
        {
            Engine.DrawText(health.ToString(), transform.PosX + 13, transform.PosY - 30, 0, 0, 0, arialFont);
            Engine.Draw(playerImg, transform.PosX, transform.PosY);
        }

    }
}
