namespace ProyectoSDL2.Engine.Scripts
{
    public class Player
    {
        private Transform transform;
        private PlayerInput input;
        private Animation playerAnim;
        private int speed = 5;
        private float health;

        private List<Image> animationImages = new List<Image>();

        public float Health => health;

        public void SetDamage(int damage)
        {
            
            health -= damage;

            if(health < 0)
            {
                // murio
            }
        } 

        public Player(int x, int y)
        {
            transform = new Transform(x, y);
            input = new PlayerInput(transform,speed);

            animationImages.Add(Engine.LoadImage("assets/mario.jpg"));
            animationImages.Add(Engine.LoadImage("assets/mario.jpg"));
            animationImages.Add(Engine.LoadImage("assets/mario.jpg"));
            animationImages.Add(Engine.LoadImage("assets/mario.jpg"));


            playerAnim = new Animation(animationImages,0.1f);
            
        }

        public void Update()
        {
            input.Update();
            playerAnim.Update();
        }

        public void Render()
        {
            Engine.Draw(playerAnim.currentFrame, transform.PosX, transform.PosY);
        }

    }
}
