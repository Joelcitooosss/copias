namespace Indicadores_Escoria
{
    partial class inicio
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelescritorio = new Panel();
            panel3 = new Panel();
            btninicio = new FontAwesome.Sharp.IconButton();
            nuevapestaña = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            graficas = new FontAwesome.Sharp.IconButton();
            Mantenimientos = new FontAwesome.Sharp.IconButton();
            btnhorometros = new FontAwesome.Sharp.IconButton();
            btnequipos = new FontAwesome.Sharp.IconButton();
            btnbitacora = new FontAwesome.Sharp.IconButton();
            btnreporte = new FontAwesome.Sharp.IconButton();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelescritorio
            // 
            panelescritorio.Dock = DockStyle.Fill;
            panelescritorio.Location = new Point(150, 0);
            panelescritorio.Name = "panelescritorio";
            panelescritorio.Size = new Size(1274, 981);
            panelescritorio.TabIndex = 1;
            panelescritorio.Paint += panelescritorio_Paint;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(150, 100);
            panel3.TabIndex = 0;
            // 
            // btninicio
            // 
            btninicio.Dock = DockStyle.Top;
            btninicio.FlatAppearance.BorderSize = 0;
            btninicio.FlatStyle = FlatStyle.Flat;
            btninicio.Font = new Font("Arial Narrow", 12.75F);
            btninicio.ForeColor = Color.White;
            btninicio.IconChar = FontAwesome.Sharp.IconChar.House;
            btninicio.IconColor = Color.White;
            btninicio.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btninicio.IconSize = 30;
            btninicio.ImageAlign = ContentAlignment.MiddleLeft;
            btninicio.Location = new Point(0, 100);
            btninicio.Margin = new Padding(0);
            btninicio.Name = "btninicio";
            btninicio.Size = new Size(150, 50);
            btninicio.TabIndex = 5;
            btninicio.Text = "Inicio";
            btninicio.TextImageRelation = TextImageRelation.ImageBeforeText;
            btninicio.UseVisualStyleBackColor = true;
            btninicio.Click += Inicio_Click;
            // 
            // nuevapestaña
            // 
            nuevapestaña.Dock = DockStyle.Bottom;
            nuevapestaña.FlatAppearance.BorderSize = 0;
            nuevapestaña.FlatStyle = FlatStyle.Flat;
            nuevapestaña.Font = new Font("Arial Narrow", 12.75F);
            nuevapestaña.ForeColor = Color.White;
            nuevapestaña.IconChar = FontAwesome.Sharp.IconChar.ArrowUpRightFromSquare;
            nuevapestaña.IconColor = Color.White;
            nuevapestaña.IconFont = FontAwesome.Sharp.IconFont.Auto;
            nuevapestaña.IconSize = 30;
            nuevapestaña.ImageAlign = ContentAlignment.MiddleRight;
            nuevapestaña.Location = new Point(0, 931);
            nuevapestaña.Margin = new Padding(0);
            nuevapestaña.Name = "nuevapestaña";
            nuevapestaña.Size = new Size(150, 50);
            nuevapestaña.TabIndex = 6;
            nuevapestaña.Text = "Nueva pestaña";
            nuevapestaña.TextImageRelation = TextImageRelation.ImageBeforeText;
            nuevapestaña.UseVisualStyleBackColor = true;
            nuevapestaña.Click += nuevapestaña_Click_1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(6, 26, 54);
            panel1.Controls.Add(graficas);
            panel1.Controls.Add(Mantenimientos);
            panel1.Controls.Add(btnhorometros);
            panel1.Controls.Add(nuevapestaña);
            panel1.Controls.Add(btnequipos);
            panel1.Controls.Add(btnbitacora);
            panel1.Controls.Add(btnreporte);
            panel1.Controls.Add(btninicio);
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(150, 981);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // graficas
            // 
            graficas.Dock = DockStyle.Top;
            graficas.FlatAppearance.BorderSize = 0;
            graficas.FlatStyle = FlatStyle.Flat;
            graficas.Font = new Font("Arial Narrow", 12.75F);
            graficas.ForeColor = Color.White;
            graficas.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            graficas.IconColor = Color.White;
            graficas.IconFont = FontAwesome.Sharp.IconFont.Solid;
            graficas.IconSize = 30;
            graficas.ImageAlign = ContentAlignment.MiddleLeft;
            graficas.Location = new Point(0, 400);
            graficas.Margin = new Padding(0);
            graficas.Name = "graficas";
            graficas.Size = new Size(150, 50);
            graficas.TabIndex = 12;
            graficas.Text = "Graficas";
            graficas.TextImageRelation = TextImageRelation.ImageBeforeText;
            graficas.UseVisualStyleBackColor = true;
            graficas.Click += graficas_Click;
            // 
            // Mantenimientos
            // 
            Mantenimientos.Dock = DockStyle.Top;
            Mantenimientos.FlatAppearance.BorderSize = 0;
            Mantenimientos.FlatStyle = FlatStyle.Flat;
            Mantenimientos.Font = new Font("Arial Narrow", 12.75F);
            Mantenimientos.ForeColor = Color.White;
            Mantenimientos.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            Mantenimientos.IconColor = Color.White;
            Mantenimientos.IconFont = FontAwesome.Sharp.IconFont.Solid;
            Mantenimientos.IconSize = 30;
            Mantenimientos.ImageAlign = ContentAlignment.MiddleLeft;
            Mantenimientos.Location = new Point(0, 350);
            Mantenimientos.Margin = new Padding(0);
            Mantenimientos.Name = "Mantenimientos";
            Mantenimientos.Size = new Size(150, 50);
            Mantenimientos.TabIndex = 11;
            Mantenimientos.Text = "Mantenimientos";
            Mantenimientos.TextImageRelation = TextImageRelation.ImageBeforeText;
            Mantenimientos.UseVisualStyleBackColor = true;
            Mantenimientos.Click += Mantenimientos_Click;
            // 
            // btnhorometros
            // 
            btnhorometros.Dock = DockStyle.Top;
            btnhorometros.FlatAppearance.BorderSize = 0;
            btnhorometros.FlatStyle = FlatStyle.Flat;
            btnhorometros.Font = new Font("Arial Narrow", 12.75F);
            btnhorometros.ForeColor = Color.White;
            btnhorometros.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            btnhorometros.IconColor = Color.White;
            btnhorometros.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnhorometros.IconSize = 30;
            btnhorometros.ImageAlign = ContentAlignment.MiddleLeft;
            btnhorometros.Location = new Point(0, 300);
            btnhorometros.Margin = new Padding(0);
            btnhorometros.Name = "btnhorometros";
            btnhorometros.Size = new Size(150, 50);
            btnhorometros.TabIndex = 10;
            btnhorometros.Text = "Horometros";
            btnhorometros.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnhorometros.UseVisualStyleBackColor = true;
            btnhorometros.Click += btnhorometros_Click_1;
            // 
            // btnequipos
            // 
            btnequipos.Dock = DockStyle.Top;
            btnequipos.FlatAppearance.BorderSize = 0;
            btnequipos.FlatStyle = FlatStyle.Flat;
            btnequipos.Font = new Font("Arial Narrow", 12.75F);
            btnequipos.ForeColor = Color.White;
            btnequipos.IconChar = FontAwesome.Sharp.IconChar.Truck;
            btnequipos.IconColor = Color.White;
            btnequipos.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnequipos.IconSize = 30;
            btnequipos.ImageAlign = ContentAlignment.MiddleLeft;
            btnequipos.Location = new Point(0, 250);
            btnequipos.Margin = new Padding(0);
            btnequipos.Name = "btnequipos";
            btnequipos.Size = new Size(150, 50);
            btnequipos.TabIndex = 1;
            btnequipos.Text = "Equipos";
            btnequipos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnequipos.UseVisualStyleBackColor = true;
            btnequipos.Click += btnequipos_Click;
            // 
            // btnbitacora
            // 
            btnbitacora.Dock = DockStyle.Top;
            btnbitacora.FlatAppearance.BorderSize = 0;
            btnbitacora.FlatStyle = FlatStyle.Flat;
            btnbitacora.Font = new Font("Arial Narrow", 12.75F);
            btnbitacora.ForeColor = Color.White;
            btnbitacora.IconChar = FontAwesome.Sharp.IconChar.File;
            btnbitacora.IconColor = Color.White;
            btnbitacora.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnbitacora.IconSize = 30;
            btnbitacora.ImageAlign = ContentAlignment.MiddleLeft;
            btnbitacora.Location = new Point(0, 200);
            btnbitacora.Margin = new Padding(0);
            btnbitacora.Name = "btnbitacora";
            btnbitacora.Size = new Size(150, 50);
            btnbitacora.TabIndex = 9;
            btnbitacora.Text = "Bitacora";
            btnbitacora.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnbitacora.UseVisualStyleBackColor = true;
            btnbitacora.Click += btnbitacora_Click;
            // 
            // btnreporte
            // 
            btnreporte.Dock = DockStyle.Top;
            btnreporte.FlatAppearance.BorderSize = 0;
            btnreporte.FlatStyle = FlatStyle.Flat;
            btnreporte.Font = new Font("Arial Narrow", 12.75F);
            btnreporte.ForeColor = Color.White;
            btnreporte.IconChar = FontAwesome.Sharp.IconChar.FileCircleCheck;
            btnreporte.IconColor = Color.White;
            btnreporte.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnreporte.IconSize = 30;
            btnreporte.ImageAlign = ContentAlignment.MiddleLeft;
            btnreporte.Location = new Point(0, 150);
            btnreporte.Margin = new Padding(0);
            btnreporte.Name = "btnreporte";
            btnreporte.Size = new Size(150, 50);
            btnreporte.TabIndex = 8;
            btnreporte.Text = "Reporte";
            btnreporte.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnreporte.UseVisualStyleBackColor = true;
            btnreporte.Click += btnreporte_Click;
            // 
            // inicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 981);
            Controls.Add(panelescritorio);
            Controls.Add(panel1);
            Name = "inicio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += inicio_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panelescritorio;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton Horometros;
        private FontAwesome.Sharp.IconButton Reporte;
        private FontAwesome.Sharp.IconButton Bitacora;
        private FontAwesome.Sharp.IconButton btninicio;
        private FontAwesome.Sharp.IconButton nuevapestaña;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton btnequipos;
        private FontAwesome.Sharp.IconButton btnbitacora;
        private FontAwesome.Sharp.IconButton btnreporte;
        private FontAwesome.Sharp.IconButton btnhorometros;
        private FontAwesome.Sharp.IconButton Mantenimientos;
        private FontAwesome.Sharp.IconButton graficas;
    }
}
