using System;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet : GameObject
    {

        int direction; // 1= derecha, -1= izquierda

        //Constructor, se llama cuando se crea
        // Si la dirección es 1 sumamos 70 a X, sino, la dejamos normal. A la Y siempre le sumamos 10.
        public Bullet(int startPosX, int startPosY, int bulletDirection)
               : base(bulletDirection == 1 ? startPosX + 70 : startPosX, startPosY + 10)
        {
            direction = bulletDirection;
        }

        public override void Update()
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

        public override void Render()
        {
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }
    }
}
