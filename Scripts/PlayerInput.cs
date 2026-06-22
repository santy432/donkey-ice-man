
namespace ProyectoSDL2.Engine.Scripts
{
    public class PlayerInput
    {
        Transform transform;
        int speed;

        private bool isMoving;
        public bool IsMoving => isMoving;
        float Timer = 0;    

        public PlayerInput(Transform playerTransform, int playerSpeed)
        {
            transform = playerTransform;
            speed = playerSpeed;
        }



        public void Update()
        {
            isMoving = false;

            Timer += Program.DeltaTime;

            if (Engine.KeyPress(Engine.KEY_A))
            {
                transform.Translate(-1 * speed, 0);
                isMoving = true;

               GameManager.Instance.Player.SetDirection(false);
            }

            if (Engine.KeyPress(Engine.KEY_D))
            {
                transform.Translate(1 * speed, 0);
                isMoving = true;

                GameManager.Instance.Player.SetDirection(true);
            }

            if (Engine.KeyPress(Engine.KEY_ESP))
            {
                if (Timer >= 1)//no dispares
                {
                    int direction = GameManager.Instance.Player.FacingRight ? 1 : -1; // ternary operator, basicamente "if right = true, direction = 1", else direction = -1

                    Program.AddBullet(transform.PosX, transform.PosY, direction);
                    Timer = 0;
                }
            
            }
        }

        public void UpdateSpeed(int newSpeed)
        {
            speed = newSpeed;
        }
    }
}
