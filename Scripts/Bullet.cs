using System;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet
    {
        Transform transform;

        public Transform BulletTransform => transform;

        //Constructor, se llama cuando se crea
        public Bullet(int startPosX, int startPosY)
        {
            transform = new Transform(startPosX + 100, startPosY + 50);
        }

        public void Update()
        {
            transform.Translate(5, 0);

            for (int i = 0; i < Program.EnemyList.Count; i++)
            {
                Enemy currentEnemy = Program.EnemyList[i];


                if ((transform.PosX < currentEnemy.Transform.PosX + 60) &&
                    (transform.PosX + 7 > currentEnemy.Transform.PosX) &&
                    (transform.PosY < currentEnemy.Transform.PosY + 90) &&
                    (transform.PosY + 26 > currentEnemy.Transform.PosY))
                {
                    currentEnemy.GetDamaged(1);
                    Program.BulletList.Remove(this);
                }
            }

        }

        public void Render()
        {
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }
    }
}
