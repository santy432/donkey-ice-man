using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public enum EnemyType { Basic, Tank }

    public static class EnemyFactory
    {
        public static GameObject Create(EnemyType type, int x, int y)
        {
            switch (type)
            {
                case EnemyType.Tank:
                    return new TankEnemy(x, y);
                default:
                    return new Enemy(x, y);
            }
        }
    }
}
