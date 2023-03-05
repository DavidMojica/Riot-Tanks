using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotTanks_1._1
{
    class PlayerTank : Tank
    {      
        public PlayerTank() { }
        public PlayerTank(string nombre, int x, int y, int w, int h) : base(nombre, x, y, w, h)
        {
            Direccion = "left";
            Estado_animacion = 1;
            Velocidad_movimiento = 3;
            Velocidad_disparo = 5;
            Vida = 2;
            Daño = 1;
        }
    }
    internal class Player : PlayerTank
    {
        string nombre;
        int vidas;
        int puntuacion;
        public int Vidas { get => vidas; set => vidas = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Puntuacion { get => puntuacion; set => puntuacion = value; }

        public Player() { }

        public Player(int x, int y,string nombre) : base("tank_normal_green_up_1", x, y, 28, 28)
        {
            this.Nombre = nombre;
            vidas = 3;
        }
    }
}
