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

        public void ResetGame()
        {

            GameObjects.Clear();


            Player = new Player(100, 650);
            AddObject(Player);

            AddObject(new TankEnemy(200, 150));
            AddObject(new Enemy(200, 150));
            AddObject(new Enemy(200, 400));
            AddObject(new Enemy(200, 650));
            //piso 1
            AddObject(new Portal(900, 650, 'A'));// Portal A
            //Piso 2
            AddObject(new Portal(100, 400, 'B'));// Portal B
            AddObject(new Portal(900, 400, 'C'));// Portal C
            //piso 3
            AddObject(new Portal(100, 150, 'D'));// Portal D

            AddObject(new SpeedPowerUp(450, 400));
        }

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

                        if (Player.IsDead())
                            CurrentState = GAME_STATE.END;
                    }

                    // Chequeamos si el jugador murio para pasar a GAME OVER
                    if (Player != null && Player.IsDead())
                    {
                        CurrentState = GAME_STATE.END;
                    }

                    // Chequeamos si ganamos (cuando no quedan enemigos)
                    if (Player != null && !CheckIfEnemiesExist())
                    {
                        CurrentState = GAME_STATE.VICTORY;
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
                    // Pantalla Inicio
                    Engine.DrawText("DONKEY ICE MAN", 350, 200, 255, 255, 255, uiFont);
                    Engine.DrawText("Presiona ENTER para jugar", 250, 400, 255, 255, 255, uiFont);
                    break;

                case GAME_STATE.GAMEPLAY:
                    // Renderizamos los GameObjects SOLO si estamos jugando
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
        private bool CheckIfEnemiesExist()
        {
            foreach (var obj in GameObjects)
            {
                if (obj.IsActive && (obj is Enemy || obj is TankEnemy))
                {
                    return true; // Todavía hay enemigos vivos
                }
            }
            return false; // Todos fueron derrotados
        }
    }
}