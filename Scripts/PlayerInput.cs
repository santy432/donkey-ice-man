
namespace ProyectoSDL2.Engine.Scripts
{
    public class PlayerInput
    {
        Transform transform;
        int speed;

        public PlayerInput(Transform playerTransform, int playerSpeed)
        {
            transform = playerTransform;
            speed = playerSpeed;
        }



        public void Update()
        {
            if (Engine.KeyPress(Engine.KEY_A))
            {
                transform.Translate(-1 * speed, 0);
            }

            if (Engine.KeyPress(Engine.KEY_D))
            {
                transform.Translate(1 * speed, 0);
            }

            if (Engine.KeyPress(Engine.KEY_W))
            {
                transform.Translate(0, -1 * speed);
            }

            if (Engine.KeyPress(Engine.KEY_S))
            {
                transform.Translate(0, 1 * speed);
            }

            if (Engine.KeyPress(Engine.KEY_ESP))
            {
                //no dispares
                Program.AddBullet(transform.PosX, transform.PosY);
            }
        }
    }
}
