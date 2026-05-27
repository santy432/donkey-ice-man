using ProyectoSDL2.Engine.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Portal : GameObject
    {
        public int PosX => transform.PosX;
        public int PosY => transform.PosY;

        public Portal(int x, int y) : base(x, y)
        {
        }

        public override void Update()
        {

        }

        public override void Render()
        {
            Engine.Draw("assets/portal.png", transform.PosX, transform.PosY);
        }
    }
}
