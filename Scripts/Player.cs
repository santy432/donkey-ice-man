namespace ProyectoSDL2.Engine.Scripts
{
    public class Player
    {
        private Transform transform;

        public Transform PlayerTransform => transform;

        private PlayerInput input;
        private Animation playerAnim;
        private int speed = 5;
        private Health health;

        private List<Image> animationImages = new List<Image>();


        public Player(int x, int y)
        {
            transform = new Transform(x, y);
            input = new PlayerInput(transform, speed);
            health = new Health(5); 

            animationImages.Add(Engine.LoadImage("assets/mario.jpg"));
            animationImages.Add(Engine.LoadImage("assets/mario.jpg"));
            animationImages.Add(Engine.LoadImage("assets/mario.jpg"));
            animationImages.Add(Engine.LoadImage("assets/mario.jpg"));

            playerAnim = new Animation(animationImages, 0.1f);
        }

        public void Update()
        {
            input.Update();
            playerAnim.Update();
            health.Colision(transform);

            if (health.IsDead())
            {
                Engine.Debug("Game Over");

            }
        }

        public bool IsDead() => health.IsDead();

        public void Render()
        {
            Engine.Draw(playerAnim.currentFrame, transform.PosX, transform.PosY);
        }

    }
}
