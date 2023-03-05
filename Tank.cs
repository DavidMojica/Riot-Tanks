using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
namespace RiotTanks_1._1
{
    public class Tank : Graphic_Object
    {
        //----------------Atributos---------------//
        private int vida;
        private int velocidad_movimiento;
        private int daño;
        private float velocidad_disparo;
        private int cantidad_vidas;
        private string direccion;
        private int estado_animacion;
        private int velocidad;
        private int puntaje_destruido;
        //--------------Propiedades-------------//
        public int Vida { get => vida; set => vida = value; }
        public int Velocidad_movimiento { get => velocidad_movimiento; set => velocidad_movimiento = value; }
        public int Daño { get => daño; set => daño = value; }
        public float Velocidad_disparo { get => velocidad_disparo; set => velocidad_disparo = value; }
        public int Cantidad_vidas { get => cantidad_vidas; set => cantidad_vidas = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Estado_animacion { get => estado_animacion; set => estado_animacion = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Puntaje_destruido { get => puntaje_destruido; set => puntaje_destruido = value; }
        //-------------Constructores------------//
        public Tank()
        {
            Direccion = "up";
            Estado_animacion = 1;
        }
        public Tank(string nombre, int x, int y, int w, int h) : base(nombre, x, y, w, h) { }
        //---------------Metodos----------------//
        public int PierdeVida(int daño_recibido,int vida_actual)
        {
            vida -= daño_recibido;
            return vida;
        }
        public void MoveUp()
        {
            this.PosY -= Velocidad_movimiento;
            Direccion = "up";
            SetPos(PosX, PosY);
        }
        public void MoveDown()
        {
            this.PosY += Velocidad_movimiento;
            Direccion = "down";
            SetPos(PosX, PosY);
        }
        public void MoveRight()
        {
            this.PosX += Velocidad_movimiento;
            Direccion = "right";
            SetPos(PosX, PosY);
        }
        public void MoveLeft()
        {
            this.PosX -= Velocidad_movimiento;
            Direccion = "left";
            SetPos(PosX, PosY);
        }
        public Bullet Disparar()
        {
            this.imagen.Location = new Point(this.PosX, this.PosY);
            return new Bullet(this.PosX,this.PosY,this.Direccion);          
        }
        public void Animacion()
        {
            string[] seccionar = NombreObjeto.ToString().Split('_');
            string nombreRecurso = seccionar[0]+"_"+seccionar[1]+"_"+seccionar[2];
            switch (Estado_animacion)
            {
                case 1:
                    nombreRecurso = nombreRecurso + '_' + Direccion+ "_"+Estado_animacion;
                    imagen.Image = (Image)Properties.Resources.ResourceManager.GetObject(nombreRecurso);
                    Estado_animacion = 2;
                    break;
                case 2:
                    nombreRecurso = nombreRecurso + '_' + Direccion + "_" + Estado_animacion;
                    imagen.Image = (Image)Properties.Resources.ResourceManager.GetObject(nombreRecurso);
                    Estado_animacion = 1;
                    break;
                default: break;
            }
        }
        public bool EvaluarColision(List<Graphic_Object> Envs)
        {
            for (int i = 0; i < Envs.Count; i++) 
                if(Envs[i].Colision_Tanques)
                    if (this.GetBounds().IntersectsWith(Envs[i].GetBounds())) 
                        return true;
            return false;
        }
        public bool EvaluarColision(Graphic_Object objeto)
        {

            if (this.GetBounds().IntersectsWith(objeto.GetBounds())) return true;
            return false;
        }
        public void Rebote(int vel)
        {
            switch (Direccion)
            {
                case "up":
                    this.PosY += vel;
                    Direccion = "down";
                    break;
                case "down":
                    this.PosY -= vel;
                    Direccion = "up";
                    break;
                case "left":
                    this.PosX += vel;
                    Direccion = "right";
                    break;
                case "right":
                    this.PosX -= vel;
                    Direccion = "left";
                    break;
                default: break;
            }
            SetPos(PosX, PosY);
        }
    }
}
