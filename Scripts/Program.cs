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

            //creamos el jugador y lo registramos en el gameManager
            Player player = new Player(100, 650);
            GameManager.Instance.Player = player;
            GameManager.Instance.AddObject(player);

            // registramos los enemigos y objetos directamente en el gameManager
            GameManager.Instance.AddObject(new TankEnemy(200, 150));
            GameManager.Instance.AddObject(new Enemy(200, 150));
            GameManager.Instance.AddObject(new Enemy(200, 400));
            GameManager.Instance.AddObject(new Enemy(200, 650));

            // Piso 1
            GameManager.Instance.AddObject(new Portal(900, 650, 'A')); // Portal A

            // Piso 2
            GameManager.Instance.AddObject(new Portal(100, 400, 'B')); // Portal B
            GameManager.Instance.AddObject(new Portal(900, 400, 'C')); // Portal C

            // Piso 3
            GameManager.Instance.AddObject(new Portal(100, 150, 'D')); // Portal D

            GameManager.Instance.AddObject(new SpeedPowerUp(450, 400));

            // Bucle principal del juego
            while (!GameManager.Instance.Player.IsDead())
            {
                currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                Update();
                Render();
            }

            Engine.Debug("GAME OVER");
            Engine.Clear();
            Engine.Show();
        }

        static void Update()
        {

            GameManager.Instance.Update();

            //CheckPortalCollision(); 
        }

        static void Render()
        {
            Engine.Clear();
            
            GameManager.Instance.Render();
            
            Engine.Show();
        }

        // cuando se creaa una bala, se suma al gameManager
        public static void AddBullet(int posX, int posY, int direction)
        {
            GameManager.Instance.AddObject(new Bullet(posX, posY, direction));
        }
    }
}