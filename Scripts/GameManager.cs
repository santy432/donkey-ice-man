using System.Collections.Generic;

namespace ProyectoSDL2.Engine.Scripts
{
    public class GameManager
    {
        // Instancia privada del Singleton
        private static GameManager instance;

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

        // Guardamos una referencia directa al jugador para acceder fácil a su posición
        public Player Player { get; set; }

        // Método para instanciar y añadir dinámicamente objetos al juego (como balas)
        public void AddObject(GameObject obj)
        {
            GameObjects.Add(obj);
        }

        public void Update()
        {
            // Recorremos la lista de atrás hacia adelante (Count - 1 hasta 0).
            // Esto evita errores lógicos si eliminamos un objeto en medio del ciclo.
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i].IsActive)
                {
                    GameObjects[i].Update();
                }
                else
                {
                    // Si el objeto se marcó como IsActive = false, el GameManager lo destruye automáticamente
                    GameObjects.RemoveAt(i);
                }
            }
        }

        public void Render()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].IsActive)
                {
                    GameObjects[i].Render();
                }
            }
        }
    }
}