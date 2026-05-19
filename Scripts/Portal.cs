using ProyectoSDL2.Engine.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine
{
    public class Portal
    {
        Transform transform;
        Image portalImg;

        public Portal(int x, int y)
        {
            transform = new Transform(x, y);
        }

        public void Update()
        {

        }

        public void Render()
        {
            Engine.Draw("assets/portal.png", transform.PosX, transform.PosY);
        }
    }
}
