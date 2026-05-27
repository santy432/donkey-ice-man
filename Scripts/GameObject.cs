
namespace ProyectoSDL2.Engine.Scripts
{
    public abstract class GameObject
    {
        protected Transform transform;
        public Transform Transform => transform;
        public bool IsActive { get; set; } = true; // util para destruir objetos

        public GameObject(int startPosX, int startPosY)
        {
            transform = new Transform(startPosX, startPosY);
        }

        public abstract void Update();
        public abstract void Render();

    }
}