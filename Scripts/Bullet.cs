using System;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet : GameObject
    {

        int direction; // 1= derecha, -1= izquierda

        //Constructor, se llama cuando se crea
        // Si la dirección es 1 sumamos 70 a X, sino, la dejamos normal. A la Y siempre le sumamos 10.

        private List<GameObject> gameObjects;      // inyectado
        private Action<Bullet> returnToPool;       // inyectado

        public Bullet(List<GameObject> gameObjects, Action<Bullet> returnToPool) : base(0, 0)
        {
            this.gameObjects = gameObjects;
            this.returnToPool = returnToPool;
        }

        public void Reset(int startPosX, int startPosY, int bulletDirection)
        {
            direction = bulletDirection;
            int realX = bulletDirection == 1 ? startPosX + 70 : startPosX;
            transform.SetPosition(realX, startPosY + 10);
        }


        public override void Update()
        {
            transform.Translate(5 * direction, 0);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject obj = gameObjects[i];
                if (!obj.IsActive || obj == this) continue;

                if (CollisionHelper.Overlaps(transform.PosX, transform.PosY, 7, 26,
                                             obj.Transform.PosX, obj.Transform.PosY, 60, 90))
                {
                    if (obj is IDamageable target)
                    {
                        target.GetDamaged(1);
                        returnToPool(this);   
                        return;
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
