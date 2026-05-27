using System.Diagnostics;

namespace ProyectoSDL2.Engine.Scripts
{
    class Program
    {
        static Player player;

        static List<Enemy> enemyList = new List<Enemy>();
        static List<Bullet> bulletList = new List<Bullet>();
        static List<Portal> portalList = new List<Portal>();
        static List<TankEnemy> tankEnemyList = new List<TankEnemy>();
        static List<TankProjectile> tankProjectileList = new List<TankProjectile>();
        static List<SpeedPowerUp> speedPowerUpList = new List<SpeedPowerUp>();

        public static List<TankEnemy> TankEnemyList => tankEnemyList;
        public static List<TankProjectile> TankProjectileList => tankProjectileList;
        public static Player Player => player;

        public static List<Enemy> EnemyList => enemyList;
        public static List<Bullet> BulletList => bulletList;
        public static List<Portal> PortalList => portalList;
        public static List<SpeedPowerUp> SpeedPowerUpList => speedPowerUpList;


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

            tankEnemyList.Add(new TankEnemy(200, 150));

            enemyList.Add(new Enemy(200, 150));
            enemyList.Add(new Enemy(200, 400));
            enemyList.Add(new Enemy(200, 650));

            portalList.Add(new Portal(900, 650));
            portalList.Add(new Portal(100, 400));
            portalList.Add(new Portal(900, 400));
            portalList.Add(new Portal(100, 150));

            speedPowerUpList.Add(new SpeedPowerUp(450, 400));

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
            CheckSpeedPowerUpCollision();


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
            for (int i = 0; i < tankEnemyList.Count; i++)
            {
                tankEnemyList[i].Update();
            }
            for (int i = 0; i < tankProjectileList.Count; i++)
            {
                tankProjectileList[i].Update();
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

        static void CheckSpeedPowerUpCollision()
        {
            for (int i = 0; i < speedPowerUpList.Count; i++)
            {
                SpeedPowerUp powerUp = speedPowerUpList[i];

                if (Math.Abs(player.PlayerTransform.PosX - powerUp.PosX) < 50 &&
                    Math.Abs(player.PlayerTransform.PosY - powerUp.PosY) < 50)
                {
                    player.ActivateSpeedBoost(5f, 10); // (boost duration in seconds, boosted speed)

                    speedPowerUpList.RemoveAt(i);
                    i--;
                }
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
            for (int i = 0; i < tankEnemyList.Count; i++)
            {
                tankEnemyList[i].Render();
            }
            for (int i = 0; i < tankProjectileList.Count; i++)
            {
                tankProjectileList[i].Render();
            }

            for (int i = 0; i < speedPowerUpList.Count; i++)
            {
                speedPowerUpList[i].Render();
            }

            Engine.Show();
        }

        public static void AddBullet(int posX, int posY, int direction)
        {
            bulletList.Add(new Bullet(posX, posY, direction));
        }


        public static void AddTankProjectile(int posX, int posY, int targetX, int targetY)
        {
            tankProjectileList.Add(new TankProjectile(posX, posY, targetX, targetY));
        }
    }

}

