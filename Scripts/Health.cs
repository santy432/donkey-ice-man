using ProyectoSDL2.Engine.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Health
    {
        private int vida;
        private float damageCooldown = 0f;
        private float damageCooldownMax = 1f;


        public event Action<int> OnDamaged; 
        public event Action OnDeath;

        public Health(int vida) { this.vida = vida; }

        public bool Colision(Transform playerTransform)
        {
            damageCooldown -= Program.DeltaTime;

            for (int i = 0; i < GameManager.Instance.GameObjects.Count; i++)
            {
                GameObject obj = GameManager.Instance.GameObjects[i];

                if (!obj.IsActive) continue;

                if (obj is IEnemy enemy)
                {
                    if (playerTransform.PosX < enemy.Transform.PosX + 64 &&
                        playerTransform.PosX + 64 > enemy.Transform.PosX &&
                        playerTransform.PosY < enemy.Transform.PosY + 99 &&
                        playerTransform.PosY + 64 > enemy.Transform.PosY)
                    {
                        if (damageCooldown <= 0)
                        {
                            Engine.Debug("Jugador golpeado por enemigo");
                            vida--;
                            damageCooldown = damageCooldownMax;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsDead() => vida <= 0;

        public void TakeDamage(int dmg)
        {
            if (damageCooldown <= 0f)
            {
                vida -= dmg;
                damageCooldown = damageCooldownMax;
                OnDamaged?.Invoke(vida);

                if (vida <= 0){
                    OnDeath?.Invoke();
                }
            }
        }
    }
}