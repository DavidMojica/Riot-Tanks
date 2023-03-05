using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotTanks_1._1
{
    public class buff:Graphic_Object
    {
        int puntos_otorgados;
        string nombre;
        public int Puntos_otorgados { get => puntos_otorgados; set => puntos_otorgados = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public buff() { }
        public buff(string nombre, int x, int y, int w, int h) : base(nombre, x, y, w, h)
        {
            this.Nombre = nombre;
        }
        public virtual int power(int x)
        {
            return 0;
        }
    }

    public class star : buff
    {
        public star(int x, int y) : base("power_star", x, y, 30, 30)
        {
             Puntos_otorgados= 500;
        }
        public override int power(int vidas)
        {
            if (vidas < 4)
            {
                return vidas + 1;
            }
            else return vidas;
        }
    }
    public class grenade : buff
    {
        public grenade(int x, int y) : base("power_grenade", x, y, 30, 30)
        {
            Puntos_otorgados = 400;
        }
    }
    public class helmet : buff
    {
        public helmet(int x, int y) : base("power_helmet", x, y, 30, 30)
        {
            Puntos_otorgados = 400;
        }
    }
    public class tank_buff : buff
    {
        public tank_buff(int x, int y) : base("power_tank", x, y, 30, 30)
        {
            Puntos_otorgados = 300;
        }
    }
    public class time : buff
    {
        public time(int x, int y) : base("power_time", x, y, 30, 30)
        {
            Puntos_otorgados = 400;
        }
    }
}
