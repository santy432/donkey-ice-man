using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    //Agregado para resolver problema de alta dependencia del gamemanager
    public static class CollisionHelper
    {
        public static bool Overlaps(
            int ax, int ay, int aw, int ah,
            int bx, int by, int bw, int bh)
        {
            return ax < bx + bw &&
                   ax + aw > bx &&
                   ay < by + bh &&
                   ay + ah > by;
        }
    }
}
