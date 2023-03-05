using System.Windows.Forms;
using System.Drawing;
namespace RiotTanks_1._1
{
    public class Graphic_Object
    {
        //Atributos
        PictureBox Imagen;
        int posX, posY;
        private bool colision_tanques;
        private bool colision_bala;
        string nombreObjeto;
        //Propiedades
        public PictureBox imagen { get => Imagen; set => Imagen = value; }
        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }
        public string NombreObjeto { get => nombreObjeto; set => nombreObjeto = value; }
        public bool Colision_Tanques { get => colision_tanques; set => colision_tanques = value; }
        public bool Colision_bala { get => colision_bala; set => colision_bala = value; }

        //Constructores
        public Graphic_Object() { }
        public Graphic_Object(string nombre, int x, int y, int w, int h)
        {
            this.nombreObjeto = nombre;
            Imagen = new PictureBox();
            Imagen.Size = new Size(w, h);
            Imagen.Image = (Image)Properties.Resources.ResourceManager.GetObject(nombre); //Lo configura dependiendo de su nombre ----- Leer plano
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            Imagen.BackColor = Color.Transparent;
            SetPos(x, y);
        }
        virtual public bool impacto()
        {
            return false;
        }
        public virtual bool CambiarImagen(string nombre)
        {
            Imagen.Image = (Image)Properties.Resources.ResourceManager.GetObject(nombre); //Lo configura dependiendo de su nombre          
            return true;
        }
        public void SetPos(int x, int y)
        {
            this.posX = x;
            this.posY = y;
            this.Imagen.Location = new Point(x, y);
        }
        public virtual Rectangle GetBounds()
        {
            return Imagen.Bounds;
        }

    }
}
