namespace ProyectoSDL2.Engine.Scripts
{
    public class Player : GameObject
    {

        private PlayerInput input;
        private Animation walkRightAnim;
        private Animation walkLeftAnim;
        private int speed = 5;
        private Health health;

        private Image playerRight; // para determinar izq o derecha
        private Image playerLeft;

        private float speedBoostTimer = 0;
        private bool speedBoostActive = false;

        private bool facingRight = true;

        public bool FacingRight => facingRight;

        private bool isMoving;

        public event Action OnDied;


        public Player(int x, int y) : base(x, y)
        {
            input = new PlayerInput(transform, speed);
            health = new Health(5);

            health.OnDamaged += OnPlayerDamaged;
            health.OnDeath += OnPlayerDeath;


            List<Image> rightFrames = new List<Image>()
            {
                 Engine.LoadImage("assets/mario_right_1.png"),
                 Engine.LoadImage("assets/mario_right_2.png"),
                 Engine.LoadImage("assets/mario_right_3.png")
            };

            List<Image> leftFrames = new List<Image>()
            {
                Engine.LoadImage("assets/mario_left_1.png"),
                Engine.LoadImage("assets/mario_left_2.png"),
                Engine.LoadImage("assets/mario_left_3.png")
            };

            walkRightAnim = new Animation(rightFrames, 0.15f); // frames cambian cada 0.15
            walkLeftAnim = new Animation(leftFrames, 0.15f);

        }

        private void OnPlayerDamaged(int vidaRestante)
        {
            Engine.Debug($"Jugador recibió daño! Vida restante: {vidaRestante}");

        }

        private void OnPlayerDeath()
        {
            Engine.Debug("El jugador murió!");
            OnDied?.Invoke();
        }

        public override void Update()
        {
            input.Update();
            health.Colision(transform);

            for (int i = 0; i < GameManager.Instance.GameObjects.Count; i++)
            {
                GameObject obj = GameManager.Instance.GameObjects[i];

                if (obj is Portal portal && portal.IsActive)
                {
                    // portal collision check
                    if (transform.PosX < portal.Transform.PosX + 60 &&
                        transform.PosX + 64 > portal.Transform.PosX &&
                        transform.PosY < portal.Transform.PosY + 60 &&
                        transform.PosY + 64 > portal.Transform.PosY)
                    {

                        // Portal A (Piso 1) -> Te lleva al Portal B (Piso 2)
                        if (portal.Id == 'A')
                        {
                            Engine.Debug("Teletransportando al Piso 2 (Portal B)!");
                            transform.SetPosition(170, 400);
                            break; // evitar doble tp en mismo frame
                        }
                        // Portal C (Piso 2) -> Te lleva al Portal D (Piso 3)
                        else if (portal.Id == 'C')
                        {
                            Engine.Debug("Teletransportando al Piso 3 (Portal D)!");
                            transform.SetPosition(170, 150);
                            break;
                        }

                        // Portal B (Piso 2) -> devuelve al Portal A (Piso 1)
                        else if (portal.Id == 'B')
                        {
                            Engine.Debug("Volviendo al Piso 1 (Portal A)!");
                            transform.SetPosition(820, 650);
                            break;
                        }
                        // Portal D (Piso 3) -> devuelve al Portal C (Piso 2)
                        else if (portal.Id == 'D')
                        {
                            Engine.Debug("Volviendo al Piso 2 (Portal C)!");
                            transform.SetPosition(820, 400);
                            break;
                        }
                    }
                }
            }

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

            if (input.IsMoving)
            {
                if (facingRight)
                    walkRightAnim.Update();
                else
                    walkLeftAnim.Update();
            }
        }

        public bool IsDead() => health.IsDead();

        public override void Render()
        {
            if (facingRight)
            {
                Engine.Draw(
                    walkRightAnim.currentFrame,
                    transform.PosX,
                    transform.PosY
                );
            }
            else
            {
                Engine.Draw(
                    walkLeftAnim.currentFrame,
                    transform.PosX,
                    transform.PosY
                );
            }
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
