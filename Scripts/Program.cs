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

            // Bucle principal del juego
            while (true)
            {
                currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                Update();
                Render();
            }
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
            GameManager.Instance.GetBullet(posX, posY, direction);
        }
    }
}