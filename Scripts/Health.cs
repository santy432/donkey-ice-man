using ProyectoSDL2.Engine.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine
{
    public class Health
    {
        private int vida;
        private float damageCooldown = 0f;
        private float damageCooldownMax = 1f;

        public Health(int Vida)
        {
            vida = Vida;
        }

        public bool Colision(Transform playerTransform)
        {
            damageCooldown -= Program.DeltaTime;


            for (int i = Program.EnemyList.Count - 1; i >= 0; i--) 
            {
                Enemy enemy = Program.EnemyList[i];


                for (int j = Program.BulletList.Count - 1; j >= 0; j--)
                {
                    Bullet bullet = Program.BulletList[j];
                    if (enemy.Transform.PosX < bullet.BulletTransform.PosX + 7 &&
                        enemy.Transform.PosX + 64 > bullet.BulletTransform.PosX &&
                        enemy.Transform.PosY < bullet.BulletTransform.PosY + 26 &&
                        enemy.Transform.PosY + 99 > bullet.BulletTransform.PosY)
                    {
                        Engine.Debug("Colision bala-enemigo");
                        Program.BulletList.RemoveAt(j);
                        enemy.GetDamaged(1); 
                        return true;
                    }
                }


                if (playerTransform.PosX < enemy.Transform.PosX + 64 &&
                    playerTransform.PosX + 64 > enemy.Transform.PosX &&
                    playerTransform.PosY < enemy.Transform.PosY + 99 &&
                    playerTransform.PosY + 64 > enemy.Transform.PosY)
                {
                    if (damageCooldown <= 0)
                    {
                        Engine.Debug("Jugador golpeado");
                        vida--;
                        damageCooldown = damageCooldownMax;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsDead() => vida <= 0;

    }
}
