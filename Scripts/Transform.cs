using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Transform
    {
        int posX, posY; 

        public int PosX => posX;
        public int PosY => posY;

        public Transform(int startPosX, int startPosY)
        {
            posX = startPosX;
            posY = startPosY;
        }

        public void Translate(int movementX, int movementY)
        {
            posX += movementX;
            posY += movementY;
        }

        public void SetPosition(int x, int y) //PARA TELETRANSPORTAR
        {
            posX = x;
            posY = y;
        }

        //rotacion
        //calcular distancia
        //etc

    }
}
