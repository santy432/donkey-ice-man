using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class ObjectPool<T> where T : GameObject
    {
        private Queue<T> pool = new Queue<T>();
        private Func<T> factory;

        public ObjectPool(Func<T> factoryMethod, int initialSize = 10)
        {
            factory = factoryMethod;
            for (int i = 0; i < initialSize; i++)
            {
                T obj = factory();
                obj.IsActive = false;
                pool.Enqueue(obj);
            }
        }

        public T Get()
        {
            if (pool.Count > 0)
            {
                T obj = pool.Dequeue();
                obj.IsActive = true;
                return obj;
            }
            // Si el pool está vacío, crea uno nuevo
            T newObj = factory();
            newObj.IsActive = true;
            return newObj;
        }

        public void Return(T obj)
        {
            obj.IsActive = false;
            pool.Enqueue(obj);
        }
    }
}
