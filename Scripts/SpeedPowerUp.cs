using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class SpeedPowerUp : GameObject
    {
        private Image image;

        public int PosX => transform.PosX;
        public int PosY => transform.PosY;

        public SpeedPowerUp(int x, int y) : base(x, y)
        {
            image = Engine.LoadImage("assets/speed.png");
        }
        
        public override void Update()
        {

        }

        public override void Render()
        {
            Engine.Draw(image, transform.PosX, transform.PosY);
        }
    }
}