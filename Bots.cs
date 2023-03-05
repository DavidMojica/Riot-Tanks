using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotTanks_1._1
{
    public class bot:Tank
    {
        int dir;
        bool buscarDireccion = true;
        int id;
        public bot() { }
        public bot(string nombre, int x, int y, int id) : base(nombre, x, y, 28, 28)
        {      

        }

        public int Dir { get => dir; set => dir = value; }
        public bool BuscarDireccion { get => buscarDireccion; set => buscarDireccion = value; }
        public int Id { get => id; set => id = value; }

        public void moverse(List<Graphic_Object> Envs, List<Tank> Bots)
        {
            if (buscarDireccion)
            {
                var seed = Environment.TickCount;
                Random rand = new Random(seed);
                for (int i = 0; i < 10; i++)
                {
                    dir = rand.Next(1, 5);
                }
                buscarDireccion = false;
            }
            else
            {
                if (!this.EvaluarColision(Envs))
                {
                    switch (dir)
                    {
                        case 1:
                            this.MoveUp();
                            break;
                        case 2:
                            this.MoveDown();
                            break;
                        case 3:
                            this.MoveLeft();
                            break;
                        case 4:
                            this.MoveRight();
                            break;
                        default: break;
                    }
                }
                else
                {
                    this.Rebote(10);
                    buscarDireccion = true;
                }
            }
        }      
    }
    public class FastTank : bot
    {
        public FastTank() { }
        public FastTank(string nombre, int x, int y, int w, int h) : base(nombre, x, y, w)
        {
            Puntaje_destruido = 200;
            Direccion = "up";
            Estado_animacion = 1;
            Velocidad_movimiento = 7;
            Velocidad_disparo = 3;
            Vida = 2;
            Daño = 1;
        }
    }
    public class ArmoredTank : bot
    {
        ArmoredTank() { }
        public ArmoredTank(string nombre, int x, int y, int w, int h) : base(nombre, x, y, w)
        {
            Puntaje_destruido = 400;
            Direccion = "up";
            Estado_animacion = 1;
            Velocidad_movimiento = 4;
            Velocidad_disparo = 4;
            Vida = 5;
            Daño = 1;
        }
    }
    public class BasicTank : bot
    {
        public BasicTank(string nombre, int x, int y, int w, int h) : base(nombre, x, y, w)
        {
            Puntaje_destruido = 100;
            Direccion = "up";
            Estado_animacion = 1;
            Velocidad_movimiento = 5;
            Velocidad_disparo = 4;
            Vida = 2;
            Daño = 1;
        }
    }
    public class EnergyTank : bot
    {
        public EnergyTank(string nombre, int x, int y, int w, int h) : base(nombre, x, y, w)
        {
            Puntaje_destruido = 300;
            Direccion = "up";
            Estado_animacion = 1;
            Velocidad_movimiento = 6;
            Velocidad_disparo = 2;
            Vida = 3;
            Daño = 1;
        }
    }
}
