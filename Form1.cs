/*                         --------------------Bloque de Informacion-----------------------
                                            Autor: David Mojica V. 
                            --------------------Riot Tanks: Hit & Run----------------------
#INSTRUCCIONES:
Moverse con WASD, disparar con 'J'. <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<---------------------------------

#Objetivos.
El juego consiste en una única cosa: Usted debe de defender la bandera (en forma de aguila) de los disparos tanto enemigos como propios.
El nivel termina cuando se acaba el tiempo límite, de esta forma avanzas al siguiente nivel.
El juego termina cuando superaste todos los niveles (actualmente 3) o cuando pierdes ya sea que se te acaben las vidas o que destruyan la bandera.

#Base de datos
El juego cuenta con una base de datos en SQLSERVER que almacena el nombre y la puntuación de los jugadores que llegaron hasta el final del juego o que perdieron en el trayecto.
Al hacer click en ver puntuaciones, se obtendrán los datos de la base de datos y se listarán los jugadores por orden de puntaje.

                            ----------------------ALGUNOS BUFFOS QUE SE GENERAN ALEATORIAMENTE EN EL MAPA------------------

                                                -Estrella: Añade 1 vida adicional hasta un tope de 4 vidas. Otorga 500 puntos.
                                                -Granada: Destruye todos los tanques enemigos que hayan en el campo. Otorga 400 puntos.
                                                -Casco: Reestablece la salud del jugador a 2.
                                                -Tiempo: Detiene el movimiento de los tanques enemigos por 4 segundos. No dejarán de disparar.
                                                -Tanque: Irá mejorando visualmente el tanque hasta un tope de 4 niveles. Cada nivel de mejora otorga más puntos que el anterior.*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RiotTanks_1._1
{
    public partial class Form1 : Form
    {
        //-------------Contenedores-----------//
        List<Graphic_Object> Envs = new List<Graphic_Object>();
        List<Bullet> Bullets_Aliados = new List<Bullet>();
        List<Bullet> Bullets_Enemigos = new List<Bullet>();
        List<Tank> bots = new List<Tank>();
        List<Tank> Aliados = new List<Tank>();
        List <Label> Labels = new List<Label>();
        List<string> reacts = new List<string>();
        List<Player> jugadores_puntuaciones = new List<Player>();
        //--------Objetos------//
        Env_Flag flag = new Env_Flag();
        buff buff = null;
        conexionbd conectar = new conexionbd();
        //---------------Instancias de Audio-----------//
        Audio audio = new Audio();
        Audio audioplayer = new Audio();  //<---- Se crearon varias instancias de audio para evitar la saturación y que un sonido anule al otro. No se logró por completo, pero se mitigó.
        Audio audio2 = new Audio();
        //---------------Variables---------------//
        int tiempo;            //Mide el tiempo, por si se requiere en alguna accion en un momento determinado.
        int tiempo_restante;   //Cada que el nivel ser carga desde cargar_niveles() el tiempo se reestablece a 120. Define el tiempo que hay que resistir por nivel.
        int nivel=1;           //Variable que modifica el nivel inicial. 
        int puntuacion_ganar=5000000; //Puntuacion límite para ganar dentro de un nivel. Es imposible que alguien logre esta puntuación en una partida normal, pero se usó para hacer validaciones en etapas tempranas de la programación.
        int num_enemigos = 4;  //Controla el máximo de enemigos que pueden existir en un nivel al mismo tiempo.
        //------Variables de bufos-----//
        int timer_reactivar = 0;   
        int spawn_buffos;          //Tiempo que pasa para que un buffo se genere. Esta dentro del rango del tiempo_min_buff y tiempo_max_buff.
        int tiempo_min_buffo=25;   //Tiempo minimo que pasa para generarse un buffo. Está en segundos.
        int tiempo_max_buffo=32;   //Tiempo maximo que pasa para generarse un buffo. Está en segundos.
        int medir_buffos=0;        //Auxiliar que nos ayuda a contar cuanto tiempo pasó despues de que un buffo se generó.
        int tiempo_buffos=0;       //Auxiliar que nos ayuda a contar cuanto tiempo pasó sin que haya un bufo en el mapa.
        int tiempo_limite_buffo=15;  //Se genera otro buffo si el buffo actual existente no es recogido en este tiempo. Mide en segs.

        Random rand_buffos = new Random();
        Player player = new Player();
        public Form1()
        {
            InitializeComponent();           
        }
        //--------------Botones Menu Principal--------------//
        private void btn_puntuaciones_Click(object sender, EventArgs e)
        {
            if (!lvpuntuaciones.Enabled)
            {
                btn_puntuaciones.Text = "Ocultar puntuaciones";
                lvpuntuaciones.Show();
                jugadores_puntuaciones = conectar.ListarPuntuaciones();
                foreach (Player player in jugadores_puntuaciones)
                {
                    ListViewItem item = new ListViewItem();
                    item = lvpuntuaciones.Items.Add(player.Nombre);
                    item.SubItems.Add(player.Puntuacion.ToString());
                }
                lvpuntuaciones.Enabled = true;
            }
            else
            {
                btn_puntuaciones.Text = "Mostrar puntuaciones";
                lvpuntuaciones.Items.Clear();
                lvpuntuaciones.Enabled = false;
                lvpuntuaciones.Hide();
            }
        }
        private void btn_jugar_Click(object sender, EventArgs e)
        {
            if (txt_nombre.Text.Length > 3)
            {
                player = new Player(370, 510, txt_nombre.Text);             
                panel_menu_principal.Dispose();           
                cargar_niveles();
                spawn_buffos = rand_buffos.Next(tiempo_min_buffo, tiempo_max_buffo);
            }
            else MessageBox.Show("El nombre del jugador es demasiado corto.", "Ingrese un nombre valido");
        }

        //-------------Timers y medidores de tiempo-----------//
        private void timer_buffos_Tick(object sender, EventArgs e)
        {                      
            if (buff == null)
            {
                tiempo_buffos++;
                if (tiempo_buffos == spawn_buffos)
                {
                    generar_buff();
                }
            }
        }
        private void timer_contar_buff_Tick(object sender, EventArgs e)
        {
            medir_buffos++;
            try
            {
                if (medir_buffos == tiempo_limite_buffo)
                {
                    Controls.Remove(buff.imagen);
                    buff = null;
                    generar_buff();
                }
            }
            catch { }
        }
        private void timer_enemigos_Tick(object sender, EventArgs e)
        {
            foreach (bot bot in bots)
            {
                bot.Animacion();
                bot.moverse(Envs, bots);
            }
        }
        private void timer_partida_Tick(object sender, EventArgs e)
        {
            Labels[0].Text = player.Puntuacion.ToString();
            player.Animacion();
            try
            {
                foreach (Bullet bullet in Bullets_Aliados)
                {
                    bullet.mover_bullet(bullet.Direccion, bullet);

                    if (bullet.colision_bullet(Bullets_Aliados, Envs) != -1)
                    {
                        int id = bullet.colision_bullet(Bullets_Aliados, Envs);
                        int idd = bullet.colision_bullet2(Bullets_Aliados, Envs);
                        if (id != -1)
                        {
                            audioplayer.SeleccionarAudio(4);
                            audioplayer.Reproducir();
                            this.Controls.Remove(Bullets_Aliados[id].imagen);
                            Bullets_Aliados.Remove(Bullets_Aliados[id]);
                            if (!Envs[idd].CambiarImagen(null))
                            {
                                Controls.Remove(Envs[idd].imagen);
                            }
                            Envs.Remove(Envs[idd]);
                        }
                    }
                    if (bullet.colision_bullet3(Bullets_Aliados, bots) != -1)
                    {
                        int iddd = bullet.colision_bullet3(Bullets_Aliados, bots);
                        int id4 = bullet.colision_bullet4(Bullets_Aliados, bots);
                        if (iddd != -1)
                        {
                            bots[iddd].Vida = bots[iddd].PierdeVida(1, bots[iddd].Vida);
                            audio2.SeleccionarAudio(5);
                            audio2.Reproducir();
                            if (bots[iddd].Vida <= 0)
                            {
                                player.Puntuacion += bots[iddd].Puntaje_destruido;
                                this.Controls.Remove(bots[iddd].imagen);
                                bots.Remove(bots[iddd]);
                                audio2.SeleccionarAudio(8);
                                audio2.Reproducir();
                            }
                            Controls.Remove(Bullets_Aliados[id4].imagen);
                            Bullets_Aliados.Remove(Bullets_Aliados[id4]);
                        }
                    }
                }
                foreach (Bullet bullet in Bullets_Enemigos)
                {
                    bullet.mover_bullet(bullet.Direccion, bullet);
                    if (bullet.colision_bullet(Bullets_Enemigos, Envs) != -1)
                    {
                        int id = bullet.colision_bullet(Bullets_Enemigos, Envs);
                        int idd = bullet.colision_bullet2(Bullets_Enemigos, Envs);
                        if (id != -1)
                        {
                            this.Controls.Remove(Bullets_Enemigos[id].imagen);
                            Bullets_Enemigos.Remove(Bullets_Enemigos[id]);
                            if (!Envs[idd].CambiarImagen(null))
                            {
                                Controls.Remove(Envs[idd].imagen);
                            }
                            Envs.Remove(Envs[idd]);
                        }
                    }
                    if (bullet.colision_bullet3(Bullets_Enemigos, Aliados) != -1)
                    {
                        int iddd = bullet.colision_bullet3(Bullets_Enemigos, Aliados);
                        int id4 = bullet.colision_bullet4(Bullets_Enemigos, Aliados);
                        if (iddd != -1)
                        {
                            Aliados[iddd].Vida = Aliados[iddd].PierdeVida(1, Aliados[iddd].Vida);
                            if (player.Vida <= 0)
                            {
                                if (player.Vidas > 0)
                                {
                                    PlayerRespawn(0);
                                }
                                else finalizar(0);
                            }
                            if (Aliados[iddd].Vida <= 0)
                            {
                                Controls.Remove(Aliados[iddd].imagen);
                                Aliados.Remove(Aliados[iddd]);
                            }
                            Controls.Remove(Bullets_Enemigos[id4].imagen);
                            Bullets_Enemigos.Remove(Bullets_Enemigos[id4]);
                        }
                    }
                }
            }
            catch { }
            if (flag.Destruido) finalizar(0);
            if (player.Puntuacion >= puntuacion_ganar) finalizar(1);
            if (tiempo_restante <= 0) finalizar(1);
            try
            {
                if (player.EvaluarColision(buff))
                {
                    evaluar_buff();
                    player.Puntuacion += buff.Puntos_otorgados;
                    Controls.Remove(buff.imagen);
                    buff = null;
                    tiempo_buffos = 0;
                    timer_contar_buff.Enabled = false;
                }
            }
            catch { }
        }
        private void timer_combat_Tick(object sender, EventArgs e)
        {
            tiempo += 1;

            tiempo_restante -= 1;
            Labels[1].Text = tiempo_restante.ToString();
            Labels[3].Text = player.Vidas.ToString();
            Labels[2].Text = player.Vida.ToString();
            if (!timer_enemigos.Enabled && reacts.Count == 0)
            {
                reacts.Add("a");
                timer_reactivar = tiempo + 4;
            }
            if (tiempo == timer_reactivar)
            {
                reacts.Clear();
                timer_enemigos.Enabled = true;
            }
            foreach (bot bot in bots)
            {
                if (((float)tiempo % bot.Velocidad_disparo) == 0)
                {
                    Bullet bullet = bot.Disparar();
                    Bullets_Enemigos.Add(bullet);
                    Controls.Add(bullet.imagen);
                    audio.SeleccionarAudio(1);
                    audio.Reproducir();
                }
            }
            if (bots.Count < num_enemigos)
            {
                Random rand = new Random();
                int randx = rand.Next(120, 180);
                switch (rand.Next(1, 5))
                {
                    case 1:
                        BasicTank botb = new BasicTank("tank_basic_yellow_up_1", randx, 60, 28, 28);
                        Controls.Add(botb.imagen);
                        bots.Add(botb); break;
                    case 2:
                        FastTank bot = new FastTank("tank_fast_yellow_up_1", randx, 60, 28, 30);
                        Controls.Add(bot.imagen);
                        bots.Add(bot); break;
                    case 3:
                        ArmoredTank bota = new ArmoredTank("tank_armored_yellow_up_1", randx, 60, 28, 28);
                        Controls.Add(bota.imagen);
                        bots.Add(bota); break;
                    case 4:
                        EnergyTank bote = new EnergyTank("tank_energy_yellow_up_1", randx, 60, 28, 28);
                        Controls.Add(bote.imagen);
                        bots.Add(bote); break;
                }
            }
        }
        //-------Cargar niveles, inciar partida y terminarla------//
        private void PlayerRespawn(int opc)
        {
            player.SetPos(370, 510);
            Controls.Add(player.imagen);
            Aliados.Add(player);
            switch (opc)
            {
                case 0:
                    player.Vidas -= 1;
                    player.Vida = 2;
                    break;
                default: 
                    break;                                   
            }                    
            audio.SeleccionarAudio(3);
            audio.Reproducir();
        }
        private void cargar_niveles()
        {
            string cargasteel = "coordenadas_steel_" + nivel;
            string cargabrick = "coordenadas_brick_" + nivel;
            string cargabush = "coordenadas_bush_" + nivel;
            string cargawater = "coordenadas_water_" + nivel;
            string cargafrost = "coordenadas_frost_" + nivel;

            limpiarlistas();
            PlayerRespawn(1);
            tiempo_restante = 15;
            timer_combat.Enabled = true;
            timer_partida.Enabled = true;
            timer_enemigos.Enabled = true;

            try //Si no puede cargar más archivos, significa que no hay más niveles y que el juego terminó en victoria para el jugador
            {
                //Muros Steel
                string _steel = (string)Properties.Resources.ResourceManager.GetObject(cargasteel);
                var coordenadas_steel = _steel.Split('\r');
                for (int i = 0; i < coordenadas_steel.Length - 1; i++)
                {
                    string aux = coordenadas_steel[i].Trim();
                    var coordAux = aux.Split(';');
                    Env_Steel steel = new Env_Steel(int.Parse(coordAux[0]), int.Parse(coordAux[1]));
                    this.Controls.Add(steel.imagen);
                    Envs.Add(steel);
                }

                //Ladrillos
                string _brick = (string)Properties.Resources.ResourceManager.GetObject(cargabrick);
                var coordenadas_brick = _brick.Split('\r');
                for (int i = 0; i < coordenadas_brick.Length - 1; i++)
                {
                    string aux = coordenadas_brick[i].Trim();
                    var coordAux = aux.Split(';');
                    Env_Brick brick = new Env_Brick(int.Parse(coordAux[0]), int.Parse(coordAux[1]));
                    this.Controls.Add(brick.imagen);
                    Envs.Add(brick);
                }
                //Flag
                flag = new Env_Flag(270, 510);
                this.Controls.Add(flag.imagen);
                Envs.Add(flag);

                //Bushes
                string _bush = (string)Properties.Resources.ResourceManager.GetObject(cargabush);
                var coordenadas_bush = _bush.Split('\r');
                for (int i = 0; i < coordenadas_bush.Length - 1; i++)
                {
                    string aux = coordenadas_bush[i].Trim();
                    var coordAux = aux.Split(';');
                    Env_Bush bush = new Env_Bush(int.Parse(coordAux[0]), int.Parse(coordAux[1]));
                    this.Controls.Add(bush.imagen);
                    Envs.Add(bush);
                }

                //Water
                string _water = (string)Properties.Resources.ResourceManager.GetObject(cargawater);
                var coordenadas_water = _water.Split('\r');
                for (int i = 0; i < coordenadas_water.Length - 1; i++)
                {
                    string aux = coordenadas_water[i].Trim();
                    var coordAux = aux.Split(';');
                    Env_Water water = new Env_Water(int.Parse(coordAux[0]), int.Parse(coordAux[1]));
                    this.Controls.Add(water.imagen);
                    Envs.Add(water);
                }

                //Frost
                string _frost = (string)Properties.Resources.ResourceManager.GetObject(cargafrost);
                var coordenadas_frost = _frost.Split('\r');
                for (int i = 0; i < coordenadas_frost.Length - 1; i++)
                {
                    string aux = coordenadas_frost[i].Trim();
                    var coordAux = aux.Split(';');
                    Env_Frost frost = new Env_Frost(int.Parse(coordAux[0]), int.Parse(coordAux[1]));
                    this.Controls.Add(frost.imagen);
                    Envs.Add(frost);
                }
            }
            catch
            {
                finalizar(2);
            }
        }
        public void finalizar(int opc)
        {
            timer_partida.Enabled = false;
            timer_combat.Enabled = false;
            switch (opc)
            {
                case 0:
                    audio.SeleccionarAudio(9);
                    audio.Reproducir();
                    if (MessageBox.Show($"Perdio en el nivel: {nivel}\nDesea volver al menu principal?", "Pierdes.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var i = conectar.GuardarJugador(player.Nombre, player.Puntuacion);
                        if (i > 0)
                            MessageBox.Show("Puntuacion guardada con exito");
                        Application.Restart();
                    }
                    else
                    {
                        var i = conectar.GuardarJugador(player.Nombre, player.Puntuacion);
                        if (i > 0)
                            MessageBox.Show("Puntuacion guardada con exito");
                       Application.Exit();
                    }
                    break;
                case 1:
                    audio.SeleccionarAudio(10);
                    audio.Reproducir();
                    MessageBox.Show("Avanzas al siguiente nivel!");
                    nivel += 1;
                    cargar_niveles();
                    break;
                case 2:
                    timer_buffos.Enabled = false;
                    timer_combat.Enabled = false;
                    timer_contar_buff.Enabled = false;
                    timer_enemigos.Enabled = false;
                    timer_partida.Enabled = false;
                    var x = conectar.GuardarJugador(player.Nombre, player.Puntuacion);
                    MessageBox.Show($"Felicidades!\nUsted ha ganado el juego con una puntuacion de: {player.Puntuacion}\nSerá recordado como un héroe ", "Ganó");
                    Application.Restart();
                    break;
            }
        }     
        void limpiarlistas()
        {
            Controls.Clear();
            bots.Clear();
            Envs.Clear();
            Bullets_Aliados.Clear();
            Bullets_Enemigos.Clear();
            Labels.Clear();
            cargalabels();
        }
        //-----------Actvidad de buffos--------//
        void generar_buff()
        {
            int x = rand_buffos.Next(30, 510);
            int y = rand_buffos.Next(30, 510);
            switch (rand_buffos.Next(0, 5))
            {
                case 0:
                    star star = new star(x, y);
                    buff = star;
                    Controls.Add(buff.imagen);
                    break;
                case 1:
                    grenade grenade = new grenade(x, y);
                    buff = grenade;
                    Controls.Add(buff.imagen);
                    break;
                case 2:
                    tank_buff tank_Buff = new tank_buff(x, y);
                    buff = tank_Buff;
                    Controls.Add(buff.imagen);
                    break;
                case 3:
                    helmet helmet = new helmet(x, y);
                    buff = helmet;
                    Controls.Add(buff.imagen);
                    break;
                case 4:
                    time time = new time(x, y);
                    buff = time;
                    Controls.Add(buff.imagen);
                    break;
            }
            timer_contar_buff.Enabled = true;
            medir_buffos = 0;
        }        
        void evaluar_buff()
        {
            if (buff is star)
            {
                player.Vidas = buff.power(player.Vidas);              
            }
            else if (buff is grenade)
            {
                foreach(bot bot in bots) Controls.Remove(bot.imagen);
                bots.Clear();
            }
            else if (buff is helmet)
            {
                player.Vida = 2;
            }
            else if (buff is tank_buff)
            {
                string[] rec = player.NombreObjeto.Split('_');
                if (rec[1] == "normal")
                {
                    player.NombreObjeto = $"{rec[0]}_normal2_{rec[2]}_{rec[3]}";
                    player.Puntuacion += buff.Puntos_otorgados;
                }
                else if (rec[1] == "normal2")
                {
                    player.Puntuacion += buff.Puntos_otorgados * 2;
                    player.NombreObjeto = $"{rec[0]}_normal3_{rec[2]}_{rec[3]}";
                }
                else if (rec[1] == "normal3")
                {
                    player.NombreObjeto = $"{rec[0]}_normal4_{rec[2]}_{rec[3]}";
                    player.Puntuacion += buff.Puntos_otorgados*3;
                }
                else player.Puntuacion += buff.Puntos_otorgados*4;
            }
            else if (buff is time)
            {
                timer_enemigos.Enabled = false;   
            }

        }
        //-----------Cargar labels laterales--------//
        private void cargalabels()
        {
            Label puntuacion = new Label();
            puntuacion.Location = new Point(690, 100);
            puntuacion.Name = "lbl_puntuacion";
            puntuacion.ForeColor = Color.Lime;
            puntuacion.Text = puntuacion.ToString();
            Controls.Add(puntuacion);
            Labels.Add(puntuacion);
            //tiempo
            Label lbltiempo = new Label();
            lbltiempo.Location = new Point(690, 60);
            lbltiempo.Name = "lbl_tiempo";
            lbltiempo.ForeColor = Color.Lime;
            lbltiempo.Text = 0.ToString();
            Controls.Add(lbltiempo);
            Labels.Add(lbltiempo);
            //salud
            Label lblsalud = new Label();
            lblsalud.Location = new Point(690, 140);
            lblsalud.Name = "lbl_salud";
            lblsalud.ForeColor = Color.Lime;
            lblsalud.Text = player.Vida.ToString();
            Controls.Add(lblsalud);
            Labels.Add(lblsalud);

            Label lblvidas = new Label();
            lblvidas.Location = new Point(690, 180);
            lblvidas.Name = "lbl_salud";
            lblvidas.ForeColor = Color.Lime;
            lblvidas.Text = player.Vidas.ToString();
            Controls.Add(lblvidas);
            Labels.Add(lblvidas);
            ///583; 61
            Label lbltiemporestante = new Label();
            lbltiemporestante.Location = new Point(590, 60);
            lbltiemporestante.Name = "lbl_puntuacion_ann";
            lbltiemporestante.ForeColor = Color.Lime;
            lbltiemporestante.Text = "Tiempo restante";
            Controls.Add(lbltiemporestante);
            Labels.Add(lbltiemporestante);

            Label lblpuntaje_an = new Label();
            lblpuntaje_an.Location = new Point(630, 100);
            lblpuntaje_an.Name = "lbl_salud";
            lblpuntaje_an.ForeColor = Color.Lime;
            lblpuntaje_an.Text = "Puntaje:";
            Controls.Add(lblpuntaje_an);
            Labels.Add(lblpuntaje_an);
            //730; 257

            Label lblsalud_an = new Label();
            lblsalud_an.Location = new Point(635, 140);
            lblsalud_an.Name = "lbl_salud";
            lblsalud_an.ForeColor = Color.Lime;
            lblsalud_an.Text = "Salud:";
            Controls.Add(lblsalud_an);
            Labels.Add(lblsalud_an);

            //731; 355
            Label lblvidas_an = new Label();
            lblvidas_an.Location = new Point(635, 180);
            lblvidas_an.Name = "lbl_salud";
            lblvidas_an.ForeColor = Color.Lime;
            lblvidas_an.Text = "Vidas: ";
            Controls.Add(lblvidas_an);
            Labels.Add(lblvidas_an);
        }
        //--------------Keys-------------//
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char letra = e.KeyChar;
            letra = char.ToUpper(letra);
            switch (letra)
            {
                case 'W':
                    if (!player.EvaluarColision(Envs)) player.MoveUp();
                    else player.Rebote(player.Velocidad + 3);
                    break;
                case 'S':
                    if (!player.EvaluarColision(Envs)) player.MoveDown();
                    else player.Rebote(player.Velocidad + 3);
                    break;
                case 'A':
                    if (!player.EvaluarColision(Envs)) player.MoveLeft();
                    else player.Rebote(player.Velocidad + 3);
                    break;
                case 'D':
                    if (!player.EvaluarColision(Envs)) player.MoveRight();
                    else player.Rebote(player.Velocidad + 3);
                    break;
                case 'J':                 
                    Bullet bullet = player.Disparar();
                    Bullets_Aliados.Add(bullet);
                    Controls.Add(bullet.imagen);
                    audioplayer.SeleccionarAudio(2);
                    audioplayer.Reproducir();
                    break;
            }
        }

        
    }
}
