using System.Diagnostics;

namespace ProyectoSDL2.Engine.Scripts
{
    class Program
    {
        static Player player;

        static List<Enemy> enemyList = new List<Enemy>();
        static List<Bullet> bulletList = new List<Bullet>();
        static List<Portal> portalList = new List<Portal>();

        public static List<Enemy> EnemyList => enemyList;
        public static List<Bullet> BulletList => bulletList;
        public static List<Portal> PortalList => portalList;


        static float deltaTime;

        public static  float DeltaTime => deltaTime;

        static void Main(string[] args)
        {
            Engine.Initialize();

            

            DateTime startTime = DateTime.Now;
            float currentTime;
            
            float lastFrameTime = 0;
            float timerLog = 0;


            player = new Player(100, 650);

            enemyList.Add(new Enemy(200, 200));
            enemyList.Add(new Enemy(200, 400));
            enemyList.Add(new Enemy(200, 650));

            portalList.Add(new Portal(900, 650));
            portalList.Add(new Portal(100, 400));
            portalList.Add(new Portal(900, 400));
            portalList.Add(new Portal(100, 150));

            while (!player.IsDead())
            {
                currentTime = (float)(DateTime.Now - startTime).TotalSeconds; //calculo el tiempo actual
                deltaTime = currentTime - lastFrameTime; //tiempo actual - tiempo del ultimo frame
                lastFrameTime = currentTime; //este es ahora mi ultimo frame
                timerLog += deltaTime;

                Update();
                Render();
            }

            Engine.Debug("GAME OVER");
            Engine.Clear();
            Engine.Show();

        }

        static void Update()
        {
            player.Update();

            CheckPortalCollision();


            for (int i = 0; i < enemyList.Count; i++)
            {
                Enemy enemy = enemyList[i];
                enemy.Update();
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                bullet.Update();
            }
        }

        static void CheckPortalCollision() 
        {
            if (portalList.Count < 4)
                return;

            Portal portalA = portalList[0]; //primer portal = portalA y segundo portal es portalB
            Portal portalB = portalList[1];
            Portal portalC = portalList[2];
            Portal portalD = portalList[3];

            int playerX = player.PlayerTransform.PosX;
            int playerY = player.PlayerTransform.PosY;

            // colision con portal A
            if (Math.Abs(playerX - portalA.PosX) < 50 &&
                Math.Abs(playerY - portalA.PosY) < 50)
            {
                player.SetPosition(portalB.PosX + 60, portalB.PosY);
            }
                                                                    //Si la distancia entre el jugador y el portal es menor de 50, le cambia la posicion al jugador
            // colision con portal B
            else if (Math.Abs(playerX - portalB.PosX) < 50 &&
                     Math.Abs(playerY - portalB.PosY) < 50)
            {
                player.SetPosition(portalA.PosX - 60, portalA.PosY);
            }

            // colision con portal C
            else if (Math.Abs(playerX - portalC.PosX) < 50 &&
                     Math.Abs(playerY - portalC.PosY) < 50)
            {
                player.SetPosition(portalD.PosX + 60, portalD.PosY);
            }

            // colision con portal D
            else if (Math.Abs(playerX - portalD.PosX) < 50 &&
                     Math.Abs(playerY - portalD.PosY) < 50)
            {
                player.SetPosition(portalC.PosX - 60, portalC.PosY);
            }
        }

        static void Render()
        {
            Engine.Clear();
            player.Render();

            for (int i = 0; i < enemyList.Count; i++)
            {
                Enemy enemy = enemyList[i];
                enemy.Render();
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                bullet.Render();
            }
            for (int i = 0; i < portalList.Count; i++)
            {
                Portal portal = portalList[i];
                portal.Render();
            }

            Engine.Show();
        }

        public static void AddBullet(int posX, int posY)
        {
            bulletList.Add(new Bullet(posX, posY));
        }

    }

}

