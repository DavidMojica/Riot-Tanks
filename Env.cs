using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
namespace RiotTanks_1._1
{
    internal class Env : Graphic_Object
    {
        string nombre;
        int vida;     
        public string Nombre { get => nombre; set => nombre = value; }
        public int Vida { get => vida; set => vida = value; }

        public Env() { }
        public Env(string nombre, int x, int y, int w, int h) : base(nombre, x, y, w, h)
        {
            this.Nombre = nombre;
        }
    } 
    class Env_Brick : Env
    {
        public Env_Brick(int x, int y) : base("env_brick", x, y, 30, 30)
        {
            Vida = 2;
            Colision_Tanques = true;
            Colision_bala = true;
        }
        public override bool CambiarImagen(string nombre)
        {
            return false;
        }
        public override bool impacto()
        {
            if (Vida > 1)
            {
                Vida -= 1;
            }
            else
            {
                CambiarImagen(null); //o boom por unos segundos
                return true;
            }
            return false;
        }
    }
    class Env_Steel : Env
    {
        public Env_Steel(int x, int y) : base("env_steel", x, y, 30, 30)
        {
            Colision_bala = true;
            Colision_Tanques = true;
            Vida = 0;
        }
        public override bool impacto()
        {
            base.impacto();
            return false;
        }           
    }
    class Env_Flag : Env
    {
        bool destruido;
        public bool Destruido { get => destruido; set => destruido = value; }
        public Env_Flag() { }
        public Env_Flag(int x, int y) : base("env_flag", x, y, 30, 30)
        {
            Destruido = false;
            Colision_Tanques = true;
            Colision_bala = true;
        }
        public override bool impacto()
        {
            CambiarImagen("env_flag_2");
            Destruido = true;
            return true;
        }
    }
    class Env_Bush : Env
    {
        public Env_Bush(int x, int y) : base("env_bush", x, y, 30, 30)
        {
            Colision_Tanques = false;
            Colision_bala = false;
            Vida = 0;
        }
    }
    class Env_Water : Env
    {
        public Env_Water(int x, int y) : base("env_water", x, y, 30, 30)
        {
            Colision_Tanques = true;
            Colision_bala = false;
            Vida = 0;
        }        
    }
    class Env_Frost : Env
    {
        public Env_Frost(int x, int y) : base("env_frost", x, y, 30, 30)
        {
            Colision_Tanques = false;
            Colision_bala = true;
            Vida = 0;
        }
    }
}