using System;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet
    {
        Transform transform;

        int direction; // 1= derecha, -1= izquierda

        public Transform BulletTransform => transform;

        //Constructor, se llama cuando se crea
        public Bullet(int startPosX, int startPosY, int bulletDirection) //bulletDirection 
        {
            direction = bulletDirection;

            if (direction == 1)
            {
                transform = new Transform(startPosX + 70, startPosY + 10);
            }
            else
            {
                transform = new Transform(startPosX, startPosY + 10);
            }
        }

        public void Update()
        {
            transform.Translate(5 * direction, 0); // direction es 1 cuando mira a la derecha y 2 cuando mira a la izquierda

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

            for (int i = 0; i < Program.TankEnemyList.Count; i++)
            {
                TankEnemy tankEnemy = Program.TankEnemyList[i];

                if ((transform.PosX < tankEnemy.Transform.PosX + 60) &&
                    (transform.PosX + 7 > tankEnemy.Transform.PosX) &&
                    (transform.PosY < tankEnemy.Transform.PosY + 90) &&
                    (transform.PosY + 26 > tankEnemy.Transform.PosY))
                {
                    tankEnemy.GetDamaged(1);
                    Program.BulletList.Remove(this);
                    return;
                }
            }

        }

        public void Render()
        {
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }
    }
}
