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
        public char Id { get; private set; }

        public Portal(int x, int y, char id) : base(x, y)
        {
            Id = id;
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
