using System;

namespace ProyectoSDL2.Engine.Scripts
{
    class Program
    {
        static float deltaTime;
        public static float DeltaTime => deltaTime;

        static void Main(string[] args)
        {
            Engine.Initialize();

            DateTime startTime = DateTime.Now;
            float currentTime;
            float lastFrameTime = 0;

            // 1. Creamos el jugador y lo registramos en el GameManager
            Player player = new Player(100, 650);
            GameManager.Instance.Player = player;
            GameManager.Instance.AddObject(player);

            // 2. Registramos los enemigos y objetos directamente en el GameManager
            GameManager.Instance.AddObject(new TankEnemy(200, 150));
            GameManager.Instance.AddObject(new Enemy(200, 150));
            GameManager.Instance.AddObject(new Enemy(200, 400));
            GameManager.Instance.AddObject(new Enemy(200, 650));

            GameManager.Instance.AddObject(new Portal(900, 650));
            GameManager.Instance.AddObject(new Portal(100, 400));
            
            GameManager.Instance.AddObject(new SpeedPowerUp(450, 400));

            // Bucle principal del juego
            while (!GameManager.Instance.Player.IsDead())
            {
                currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                // 3. LLAMAMOS AL GAMEMANAGER
                Update();
                Render();
            }

            Engine.Debug("GAME OVER");
            Engine.Clear();
            Engine.Show();
        }

        static void Update()
        {
            // El GameManager se encarga de hacer el Update de todos los objetos en su lista
            GameManager.Instance.Update();
            
            // Aquí puedes mantener funciones globales temporales de colisiones complejas si lo deseas
            // CheckPortalCollision(); 
        }

        static void Render()
        {
            Engine.Clear();
            
            // El GameManager dibuja todo de forma automática y ordenada
            GameManager.Instance.Render();
            
            Engine.Show();
        }

        // Modificamos este método para que cuando crees una bala, se sume al GameManager
        public static void AddBullet(int posX, int posY, int direction)
        {
            GameManager.Instance.AddObject(new Bullet(posX, posY, direction));
        }
    }
}