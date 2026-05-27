
namespace ProyectoSDL2.Engine.Scripts
{
    public class PlayerInput
    {
        Transform transform;
        int speed;

        float Timer = 0;    

        public PlayerInput(Transform playerTransform, int playerSpeed)
        {
            transform = playerTransform;
            speed = playerSpeed;
        }



        public void Update()
        {
            Timer += Program.DeltaTime;

            if (Engine.KeyPress(Engine.KEY_A))
            {
                transform.Translate(-1 * speed, 0);
            }

            if (Engine.KeyPress(Engine.KEY_D))
            {
                transform.Translate(1 * speed, 0);
            }

            if (Engine.KeyPress(Engine.KEY_ESP))
            {
                if (Timer >= 1)//no dispares
                {
                    Program.AddBullet(transform.PosX, transform.PosY);
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
