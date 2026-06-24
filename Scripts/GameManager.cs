using System.Collections.Generic;

namespace ProyectoSDL2.Engine.Scripts
{

    public enum GAME_STATE
    {
        START = 0,
        GAMEPLAY = 1,
        END = 2,
        VICTORY = 3
    }

    public class GameManager
    {
        private ObjectPool<Bullet> bulletPool;
        private ObjectPool<TankProjectile> projectilePool;

        // Instancia privada del Singleton
        private static GameManager instance;

        public GAME_STATE CurrentState = GAME_STATE.START;
        private Font uiFont;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        // Lista maestra que contiene absolutamente todos los GameObjects en pantalla
        public List<GameObject> GameObjects { get; private set; } = new List<GameObject>();

        // Guardamos una referencia directa al jugador para acceder fácil a su posicion
        public Player Player { get; set; }

        private GameManager()
        {
            // Cargamos la fuente una sola vez para dibujar los textos del menu
            uiFont = Engine.LoadFont("Fonts/arial.ttf", 40);
        }

        // Metodo para instanciar y añadir dinamicamente objetos al juego (como balas)
        public void AddObject(GameObject obj)
        {
            GameObjects.Add(obj);
        }

        private void OnEnemyDied()
        {
            enemiesAlive--;
            Engine.Debug($"Enemigo eliminado. Quedan: {enemiesAlive}");

            if (enemiesAlive <= 0)
                CurrentState = GAME_STATE.VICTORY;
        }

        public void ResetGame()
        {

            GameObjects.Clear();
            enemiesAlive = 0;


            Player = new Player(100, 650);
            Player.OnDied += () => CurrentState = GAME_STATE.END;
            AddObject(Player);

            var tank = (TankEnemy)EnemyFactory.Create(EnemyType.Tank, 200, 150);
            tank.OnEnemyDied += OnEnemyDied;
            AddObject(tank);
            enemiesAlive++;



            var enemy1 = (Enemy)EnemyFactory.Create(EnemyType.Basic, 200, 150);
            enemy1.OnEnemyDied += OnEnemyDied;
            enemiesAlive++;
            AddObject(enemy1);



            var enemy2 = (Enemy)EnemyFactory.Create(EnemyType.Basic, 200, 400);
            enemy2.OnEnemyDied += OnEnemyDied;
            enemiesAlive++;
            AddObject(enemy2);


            var enemy3 = (Enemy)EnemyFactory.Create(EnemyType.Basic, 200, 650);
            enemy3.OnEnemyDied += OnEnemyDied;
            enemiesAlive++;
            AddObject(enemy3);



            //piso 1
            AddObject(new Portal(900, 650, 'A'));// Portal A
            //Piso 2
            AddObject(new Portal(100, 400, 'B'));// Portal B
            AddObject(new Portal(900, 400, 'C'));// Portal C
            //piso 3
            AddObject(new Portal(100, 150, 'D'));// Portal D

            AddObject(new SpeedPowerUp(450, 400));

            bulletPool = new ObjectPool<Bullet>(() => new Bullet(GameObjects, ReturnBullet),initialSize: 20 );
            projectilePool = new ObjectPool<TankProjectile>(() => new TankProjectile(), initialSize: 10);

            var speedPowerUp = new SpeedPowerUp(450, 400);
            speedPowerUp.OnPickedUp += () =>
            {
                Player.ActivateSpeedBoost(7f, 8);
                Engine.Debug("PowerUp recogido!");
            };
            AddObject(speedPowerUp);
        }

        private int enemiesAlive = 0;

        public void Update()
        {
            switch (CurrentState)
            {
                case GAME_STATE.START:
                    // Si estamos en el menú y presionan Enter, iniciamos el juego
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        ResetGame();
                        CurrentState = GAME_STATE.GAMEPLAY;
                    }
                    break;

                case GAME_STATE.GAMEPLAY:
                    // Recorremos la lista de atrás hacia adelante (Count - 1 hasta 0).
                    // Esto evita errores logicos si eliminamos un objeto en medio del ciclo.
                    if (Player != null)
                    {
                        for (int i = GameObjects.Count - 1; i >= 0; i--)
                        {
                            if (GameObjects[i].IsActive)
                                GameObjects[i].Update();
                            else
                                GameObjects.RemoveAt(i);
                        }

                    }

                    break;

                case GAME_STATE.END:

                case GAME_STATE.VICTORY:
                    // Si perdimos o ganamos, al presionar R volvemos al menu principal
                    if (Engine.KeyPress(Engine.KEY_R))
                    {
                        CurrentState = GAME_STATE.START;
                    }
                    break;
            }
        }

        public void Render()
        {

            switch (CurrentState)
            {
                case GAME_STATE.START:
                    // Pantalla inicio
                    Engine.DrawText("DONKEY ICE MAN", 350, 200, 255, 255, 255, uiFont);
                    Engine.DrawText("Presiona ENTER para jugar", 250, 400, 255, 255, 255, uiFont);
                    break;

                case GAME_STATE.GAMEPLAY:
                    for (int i = 0; i < GameObjects.Count; i++)
                    {
                        if (GameObjects[i].IsActive)
                        {
                            GameObjects[i].Render();
                        }
                    }
                    break;

                case GAME_STATE.END:
                    // Pantalla game Over
                    Engine.DrawText("GAME OVER", 380, 250, 255, 0, 0, uiFont);
                    Engine.DrawText("Presiona R para volver al Menu", 220, 450, 255, 255, 255, uiFont);
                    break;

                case GAME_STATE.VICTORY:
                    //pantalla Victoria
                    Engine.DrawText("¡VICTORIA!", 380, 250, 0, 255, 0, uiFont);
                    Engine.DrawText("Presiona R para volver al Menu", 220, 450, 255, 255, 255, uiFont);
                    break;
            }
        }

        // Función que busca si queda algún Enemigo vivo en la lista



        public Bullet GetBullet(int x, int y, int direction)
        {
            Bullet b = bulletPool.Get();
            b.Reset(x, y, direction);
            AddObject(b);
            return b;
        }

        public void ReturnBullet(Bullet b) => bulletPool.Return(b);


        public TankProjectile GetProjectile(int x, int y, int tx, int ty)
        {
            TankProjectile p = projectilePool.Get();
            p.Reset(x, y, tx, ty);
            AddObject(p);
            return p;
        }


        public void ReturnProjectile(TankProjectile p) => projectilePool.Return(p);
    }
}