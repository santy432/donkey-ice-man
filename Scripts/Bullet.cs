using System;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet : GameObject
    {

        int direction; // 1= derecha, -1= izquierda

        //Constructor, se llama cuando se crea
        // Si la dirección es 1 sumamos 70 a X, sino, la dejamos normal. A la Y siempre le sumamos 10.

        public Bullet() : base(0, 0) { }

        public void Reset(int startPosX, int startPosY, int bulletDirection)
        {
            direction = bulletDirection;
            int realX = bulletDirection == 1 ? startPosX + 70 : startPosX;
            transform.SetPosition(realX, startPosY + 10);
        }

        public override void Update()
        {
            transform.Translate(5 * direction, 0); // direction es 1 cuando mira a la derecha y 2 cuando mira a la izquierda

            for (int i = 0; i < GameManager.Instance.GameObjects.Count; i++)
            {
                GameObject obj = GameManager.Instance.GameObjects[i];


                // ignoramos bala o a objetos inactivos
                if (!obj.IsActive || obj == this) continue;

                // checkeo colisión general
                if ((transform.PosX < obj.Transform.PosX + 60) &&
                    (transform.PosX + 7 > obj.Transform.PosX) &&
                    (transform.PosY < obj.Transform.PosY + 90) &&
                    (transform.PosY + 26 > obj.Transform.PosY))
                {
                    // objeto con etiqueta iDamageable = hace daño
                    if (obj is IDamageable damageableTarget)
                    {
                        damageableTarget.GetDamaged(1); // le hace daño (sea tank, enemy o algun jefe)
                        GameManager.Instance.ReturnBullet(this);
                        return; // salimos del bucle para que la bala no atraviese y dañe a dos juntos
                    }
                }
            }
        }

        public override void Render()
        {
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }
    }
}
