using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    partial class mantenimientos
    {
        private IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle encabezado = new DataGridViewCellStyle();
            DataGridViewCellStyle celdas = new DataGridViewCellStyle();
            panel1 = new Panel();
            lblTitulo = new Label();
            panel2 = new Panel();
            lblIndicacion = new Label();
            panelBotones = new FlowLayoutPanel();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnGuardar = new Button();
            panel3 = new Panel();
            tablaMantenimientos = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panelBotones.SuspendLayout();
            panel3.SuspendLayout();
            ((ISupportInitialize)tablaMantenimientos).BeginInit();
            SuspendLayout();
            //
            // panel1
            //
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1269, 72);
            panel1.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1269, 72);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Registro y consulta de mantenimientos";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // panel2
            //
            panel2.BackColor = Color.White;
            panel2.Controls.Add(lblIndicacion);
            panel2.Controls.Add(panelBotones);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 72);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(12, 5, 12, 7);
            panel2.Size = new Size(1269, 55);
            panel2.TabIndex = 1;
            //
            // lblIndicacion
            //
            lblIndicacion.Dock = DockStyle.Fill;
            lblIndicacion.Font = new Font("Segoe UI", 9F);
            lblIndicacion.ForeColor = Color.FromArgb(70, 70, 70);
            lblIndicacion.Location = new Point(12, 5);
            lblIndicacion.Name = "lblIndicacion";
            lblIndicacion.Padding = new Padding(3, 0, 0, 0);
            lblIndicacion.Size = new Size(727, 43);
            lblIndicacion.TabIndex = 0;
            lblIndicacion.Text = "Captura en la última fila o modifica una celda. Los campos grises se completan automáticamente al elegir el ECO.";
            lblIndicacion.TextAlign = ContentAlignment.MiddleLeft;
            //
            // panelBotones
            //
            panelBotones.Controls.Add(btnLimpiar);
            panelBotones.Controls.Add(btnEliminar);
            panelBotones.Controls.Add(btnEditar);
            panelBotones.Controls.Add(btnGuardar);
            panelBotones.Dock = DockStyle.Right;
            panelBotones.FlowDirection = FlowDirection.RightToLeft;
            panelBotones.Location = new Point(739, 5);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(518, 43);
            panelBotones.TabIndex = 1;
            panelBotones.WrapContents = false;
            //
            // btnLimpiar
            //
            btnLimpiar.Location = new Point(386, 4);
            btnLimpiar.Margin = new Padding(4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(128, 34);
            btnLimpiar.TabIndex = 3;
            btnLimpiar.Text = "Cancelar / recargar";
            btnLimpiar.UseVisualStyleBackColor = true;
            //
            // btnEliminar
            //
            btnEliminar.Enabled = false;
            btnEliminar.Location = new Point(262, 4);
            btnEliminar.Margin = new Padding(4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(116, 34);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar fila";
            btnEliminar.UseVisualStyleBackColor = true;
            //
            // btnEditar
            //
            btnEditar.Enabled = false;
            btnEditar.Location = new Point(138, 4);
            btnEditar.Margin = new Padding(4);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(116, 34);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "Editar celda";
            btnEditar.UseVisualStyleBackColor = true;
            //
            // btnGuardar
            //
            btnGuardar.Enabled = false;
            btnGuardar.Location = new Point(6, 4);
            btnGuardar.Margin = new Padding(4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(124, 34);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar fila";
            btnGuardar.UseVisualStyleBackColor = true;
            //
            // panel3
            //
            panel3.Controls.Add(tablaMantenimientos);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 127);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(10, 0, 10, 10);
            panel3.Size = new Size(1269, 511);
            panel3.TabIndex = 2;
            //
            // tablaMantenimientos
            //
            tablaMantenimientos.AllowUserToAddRows = true;
            tablaMantenimientos.AllowUserToDeleteRows = false;
            tablaMantenimientos.AllowUserToOrderColumns = true;
            tablaMantenimientos.AllowUserToResizeRows = false;
            tablaMantenimientos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;
            tablaMantenimientos.BackgroundColor = SystemColors.Window;
            tablaMantenimientos.BorderStyle = BorderStyle.Fixed3D;
            encabezado.Alignment = DataGridViewContentAlignment.MiddleLeft;
            encabezado.BackColor = SystemColors.Control;
            encabezado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            encabezado.ForeColor = SystemColors.WindowText;
            encabezado.SelectionBackColor = SystemColors.Highlight;
            encabezado.SelectionForeColor = SystemColors.HighlightText;
            encabezado.WrapMode = DataGridViewTriState.True;
            tablaMantenimientos.ColumnHeadersDefaultCellStyle = encabezado;
            tablaMantenimientos.ColumnHeadersHeight = 42;
            tablaMantenimientos.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            celdas.Alignment = DataGridViewContentAlignment.MiddleLeft;
            celdas.BackColor = SystemColors.Window;
            celdas.Font = new Font("Segoe UI", 9F);
            celdas.ForeColor = SystemColors.ControlText;
            celdas.SelectionBackColor = SystemColors.Highlight;
            celdas.SelectionForeColor = SystemColors.HighlightText;
            celdas.WrapMode = DataGridViewTriState.False;
            tablaMantenimientos.DefaultCellStyle = celdas;
            tablaMantenimientos.Dock = DockStyle.Fill;
            tablaMantenimientos.EditMode = DataGridViewEditMode.EditOnEnter;
            tablaMantenimientos.Location = new Point(10, 0);
            tablaMantenimientos.Margin = new Padding(0);
            tablaMantenimientos.MultiSelect = false;
            tablaMantenimientos.Name = "tablaMantenimientos";
            tablaMantenimientos.ReadOnly = false;
            tablaMantenimientos.RowHeadersWidth = 48;
            tablaMantenimientos.RowTemplate.Height = 28;
            tablaMantenimientos.SelectionMode =
                DataGridViewSelectionMode.CellSelect;
            tablaMantenimientos.Size = new Size(1249, 501);
            tablaMantenimientos.TabIndex = 0;
            //
            // mantenimientos
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1269, 638);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1000, 600);
            Name = "mantenimientos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registro de mantenimientos";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((ISupportInitialize)tablaMantenimientos).EndInit();
            ResumeLayout(false);
        }

        private Panel panel1;
        private Label lblTitulo;
        private Panel panel2;
        private Label lblIndicacion;
        private FlowLayoutPanel panelBotones;
        private Button btnLimpiar;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnGuardar;
        private Panel panel3;
        private DataGridView tablaMantenimientos;
    }
}
