namespace ProyectoSDL2.Engine.Scripts
{
    public class Player
    {
        private Transform transform;

        public Transform PlayerTransform => transform;

        private PlayerInput input;
        private Animation playerAnim;
        private int speed = 5;
        private Health health;

        private List<Image> animationImages = new List<Image>();

        private float speedBoostTimer = 0;
        private bool speedBoostActive = false;

        private bool facingRight = true;

        public bool FacingRight => facingRight;


        public Player(int x, int y)
        {
            transform = new Transform(x, y);
            input = new PlayerInput(transform, speed);
            health = new Health(5);

            animationImages.Add(Engine.LoadImage("assets/mario.png"));
            animationImages.Add(Engine.LoadImage("assets/mario.png"));
            animationImages.Add(Engine.LoadImage("assets/mario.png"));
            animationImages.Add(Engine.LoadImage("assets/mario.png"));

            playerAnim = new Animation(animationImages, 0.1f);
        }

        public void Update()
        {
            input.Update();
            playerAnim.Update();
            health.Colision(transform);

            if (speedBoostActive)
            {
                speedBoostTimer -= Program.DeltaTime;

                if (speedBoostTimer <= 0)
                {
                    input.UpdateSpeed(5); // normal speed
                    speedBoostActive = false;
                }
            }

            if (health.IsDead())
            {
                Engine.Debug("Game Over");
            }
        }

        public bool IsDead() => health.IsDead();

        public void Render()
        {
            Engine.Draw(playerAnim.currentFrame, transform.PosX, transform.PosY);
        }

        public void SetPosition(int x, int y)
        {
            transform.SetPosition(x, y);
        }

        public void TakeDamage(int dmg)
        {
            health.TakeDamage(dmg);
        }

        public void SetDirection(bool right)
        {
            facingRight = right;
        }

        public void ActivateSpeedBoost(float duration, int boostedSpeed)
        {
            input.UpdateSpeed(boostedSpeed);

            speedBoostTimer = duration;
            speedBoostActive = true;
        }
    }
}
