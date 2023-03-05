namespace RiotTanks_1._1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer_partida = new System.Windows.Forms.Timer(this.components);
            this.timer_combat = new System.Windows.Forms.Timer(this.components);
            this.panel_menu_principal = new System.Windows.Forms.Panel();
            this.lvpuntuaciones = new System.Windows.Forms.ListView();
            this.lvJugador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvpuntuacion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_puntuaciones = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.btn_jugar = new System.Windows.Forms.Button();
            this.label_menu = new System.Windows.Forms.Label();
            this.timer_buffos = new System.Windows.Forms.Timer(this.components);
            this.timer_enemigos = new System.Windows.Forms.Timer(this.components);
            this.timer_contar_buff = new System.Windows.Forms.Timer(this.components);
            this.panel_menu_principal.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_partida
            // 
            this.timer_partida.Tick += new System.EventHandler(this.timer_partida_Tick);
            // 
            // timer_combat
            // 
            this.timer_combat.Interval = 1000;
            this.timer_combat.Tick += new System.EventHandler(this.timer_combat_Tick);
            // 
            // panel_menu_principal
            // 
            this.panel_menu_principal.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel_menu_principal.BackgroundImage = global::RiotTanks_1._1.Properties.Resources.env_brick;
            this.panel_menu_principal.Controls.Add(this.lvpuntuaciones);
            this.panel_menu_principal.Controls.Add(this.btn_puntuaciones);
            this.panel_menu_principal.Controls.Add(this.label1);
            this.panel_menu_principal.Controls.Add(this.txt_nombre);
            this.panel_menu_principal.Controls.Add(this.btn_jugar);
            this.panel_menu_principal.Controls.Add(this.label_menu);
            this.panel_menu_principal.Location = new System.Drawing.Point(193, 31);
            this.panel_menu_principal.Name = "panel_menu_principal";
            this.panel_menu_principal.Size = new System.Drawing.Size(455, 489);
            this.panel_menu_principal.TabIndex = 4;
            // 
            // lvpuntuaciones
            // 
            this.lvpuntuaciones.BackColor = System.Drawing.Color.Orange;
            this.lvpuntuaciones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvJugador,
            this.lvpuntuacion});
            this.lvpuntuaciones.Enabled = false;
            this.lvpuntuaciones.Font = new System.Drawing.Font("MV Boli", 11.25F);
            this.lvpuntuaciones.GridLines = true;
            this.lvpuntuaciones.HideSelection = false;
            this.lvpuntuaciones.Location = new System.Drawing.Point(71, 140);
            this.lvpuntuaciones.Name = "lvpuntuaciones";
            this.lvpuntuaciones.Size = new System.Drawing.Size(335, 206);
            this.lvpuntuaciones.TabIndex = 5;
            this.lvpuntuaciones.UseCompatibleStateImageBehavior = false;
            this.lvpuntuaciones.View = System.Windows.Forms.View.Details;
            this.lvpuntuaciones.Visible = false;
            // 
            // lvJugador
            // 
            this.lvJugador.Text = "Jugador";
            this.lvJugador.Width = 165;
            // 
            // lvpuntuacion
            // 
            this.lvpuntuacion.Text = "Puntuacion";
            this.lvpuntuacion.Width = 163;
            // 
            // btn_puntuaciones
            // 
            this.btn_puntuaciones.BackColor = System.Drawing.Color.Transparent;
            this.btn_puntuaciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_puntuaciones.FlatAppearance.BorderSize = 0;
            this.btn_puntuaciones.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btn_puntuaciones.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.btn_puntuaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_puntuaciones.Font = new System.Drawing.Font("Trebuchet MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_puntuaciones.ForeColor = System.Drawing.Color.Orange;
            this.btn_puntuaciones.Location = new System.Drawing.Point(80, 352);
            this.btn_puntuaciones.Name = "btn_puntuaciones";
            this.btn_puntuaciones.Size = new System.Drawing.Size(342, 104);
            this.btn_puntuaciones.TabIndex = 4;
            this.btn_puntuaciones.Text = "Ver puntuaciones";
            this.btn_puntuaciones.UseVisualStyleBackColor = false;
            this.btn_puntuaciones.Click += new System.EventHandler(this.btn_puntuaciones_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(139, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ingrese su nombre";
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(127, 188);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(252, 20);
            this.txt_nombre.TabIndex = 2;
            // 
            // btn_jugar
            // 
            this.btn_jugar.BackColor = System.Drawing.Color.Transparent;
            this.btn_jugar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_jugar.FlatAppearance.BorderSize = 0;
            this.btn_jugar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btn_jugar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.btn_jugar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_jugar.Font = new System.Drawing.Font("Trebuchet MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_jugar.ForeColor = System.Drawing.Color.Orange;
            this.btn_jugar.Location = new System.Drawing.Point(80, 249);
            this.btn_jugar.Name = "btn_jugar";
            this.btn_jugar.Size = new System.Drawing.Size(342, 77);
            this.btn_jugar.TabIndex = 1;
            this.btn_jugar.Text = "Jugar";
            this.btn_jugar.UseVisualStyleBackColor = false;
            this.btn_jugar.Click += new System.EventHandler(this.btn_jugar_Click);
            // 
            // label_menu
            // 
            this.label_menu.AutoSize = true;
            this.label_menu.BackColor = System.Drawing.Color.Transparent;
            this.label_menu.Font = new System.Drawing.Font("Leelawadee UI", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_menu.ForeColor = System.Drawing.Color.Orange;
            this.label_menu.Location = new System.Drawing.Point(107, 13);
            this.label_menu.Name = "label_menu";
            this.label_menu.Size = new System.Drawing.Size(228, 100);
            this.label_menu.TabIndex = 0;
            this.label_menu.Text = "Riot Tanks: \r\nHit and Run";
            // 
            // timer_buffos
            // 
            this.timer_buffos.Enabled = true;
            this.timer_buffos.Interval = 1000;
            this.timer_buffos.Tick += new System.EventHandler(this.timer_buffos_Tick);
            // 
            // timer_enemigos
            // 
            this.timer_enemigos.Tick += new System.EventHandler(this.timer_enemigos_Tick);
            // 
            // timer_contar_buff
            // 
            this.timer_contar_buff.Interval = 1000;
            this.timer_contar_buff.Tick += new System.EventHandler(this.timer_contar_buff_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(854, 574);
            this.Controls.Add(this.panel_menu_principal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Riot Tanks: Hit & Run";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.panel_menu_principal.ResumeLayout(false);
            this.panel_menu_principal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_partida;
        private System.Windows.Forms.Timer timer_combat;
        private System.Windows.Forms.Panel panel_menu_principal;
        private System.Windows.Forms.Button btn_jugar;
        private System.Windows.Forms.Label label_menu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.Button btn_puntuaciones;
        private System.Windows.Forms.ListView lvpuntuaciones;
        private System.Windows.Forms.ColumnHeader lvJugador;
        private System.Windows.Forms.ColumnHeader lvpuntuacion;
        private System.Windows.Forms.Timer timer_buffos;
        private System.Windows.Forms.Timer timer_enemigos;
        private System.Windows.Forms.Timer timer_contar_buff;
    }
}

