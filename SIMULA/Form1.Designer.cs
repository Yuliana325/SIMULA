using System.Windows.Forms;
using System.Xml.Linq;

namespace SIMULA
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMemoriaUtilizada;
        private System.Windows.Forms.Button BtnContinuar;
        private System.Windows.Forms.Button BtnDetener;
        private System.Windows.Forms.Timer TimerCola;
        private System.Windows.Forms.Timer TimerPila;

        private System.Windows.Forms.Label[] colaLabels = new System.Windows.Forms.Label[10];
        private System.Windows.Forms.Label[] pilaLabels = new System.Windows.Forms.Label[20];



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InicializarColaLabels()
        {
            for (int i = 0; i < colaLabels.Length; i++)
            {
                colaLabels[i] = new Label
                {
                    BackColor = System.Drawing.Color.Orange,
                    ForeColor = System.Drawing.Color.White,
                    Text = "Vacío",
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Font = new System.Drawing.Font("Arial", 10),
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Size = new System.Drawing.Size(180, 36),
                    Location = new System.Drawing.Point(186, 106 + (i * 40)) // Ubica las etiquetas en columnas
                };

                this.Controls.Add(colaLabels[i]);
            }
        }

        // Lista para verificar si un proceso ha sido seleccionado
        private bool[] procesosSeleccionados;

        private void InicializarPilaLabels()
        {
            procesosSeleccionados = new bool[pilaLabels.Length]; // Inicializamos el arreglo de selección

            for (int i = 0; i < pilaLabels.Length; i++)
            {
                pilaLabels[i] = new Label
                {
                    BackColor = System.Drawing.Color.White,
                    ForeColor = System.Drawing.Color.Black,
                    Text = "10 MB", // Cambia el texto a "10 MB" para reflejar el tamaño de la partición
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Font = new System.Drawing.Font("Arial", 10),
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Size = new System.Drawing.Size(200, 18),
                    Location = new System.Drawing.Point(429, 106 + (i * 20)) // Ubica las etiquetas en otra columna
                };

                // Agregar el evento de clic para cada etiqueta de la pila
                pilaLabels[i].Click += PilaLabel_Click;

                this.Controls.Add(pilaLabels[i]);
            }
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblMemoriaUtilizada = new Label();
            BtnContinuar = new Button();
            BtnDetener = new Button();
            TimerCola = new System.Windows.Forms.Timer(components);
            TimerPila = new System.Windows.Forms.Timer(components);
            lblMemoriaTotal = new Label();
            PrimerAjuste = new Button();
            MejorAjuste = new Button();
            PeorAjuste = new Button();
            label2 = new Label();
            label1 = new Label();
            label21 = new Label();
            label3 = new Label();
            Eliminar = new Button();
            txtIdEliminar = new TextBox();
            btnEliminar = new Button();
            label4 = new Label();
            lblMemoriaDisponible = new Label();
            lblEstasEn = new Label();
            SuspendLayout();
            // 
            // lblMemoriaUtilizada
            // 
            lblMemoriaUtilizada.Font = new Font("Sitka Subheading", 14.2499981F);
            lblMemoriaUtilizada.ForeColor = Color.Red;
            lblMemoriaUtilizada.Location = new Point(642, 136);
            lblMemoriaUtilizada.Name = "lblMemoriaUtilizada";
            lblMemoriaUtilizada.Size = new Size(300, 50);
            lblMemoriaUtilizada.TabIndex = 1;
            lblMemoriaUtilizada.Text = "MEMORIA UTILIZADA: 0 MB";
            // 
            // BtnContinuar
            // 
            BtnContinuar.BackColor = Color.FromArgb(92, 225, 230);
            BtnContinuar.Font = new Font("Sitka Subheading", 14.2499981F);
            BtnContinuar.Location = new Point(660, 283);
            BtnContinuar.Name = "BtnContinuar";
            BtnContinuar.Size = new Size(110, 40);
            BtnContinuar.TabIndex = 2;
            BtnContinuar.Text = "Continuar";
            BtnContinuar.UseVisualStyleBackColor = false;
            BtnContinuar.Click += BtnContinuar_Click;
            // 
            // BtnDetener
            // 
            BtnDetener.BackColor = Color.FromArgb(255, 145, 77);
            BtnDetener.Font = new Font("Sitka Subheading", 14.2499981F);
            BtnDetener.Location = new Point(792, 283);
            BtnDetener.Name = "BtnDetener";
            BtnDetener.Size = new Size(110, 40);
            BtnDetener.TabIndex = 3;
            BtnDetener.Text = "Detener";
            BtnDetener.UseVisualStyleBackColor = false;
            BtnDetener.Click += BtnDetener_Click;
            // 
            // TimerCola
            // 
            TimerCola.Tick += TimerCola_Tick_1;
            // 
            // TimerPila
            // 
            TimerPila.Tick += TimerPila_Tick_1;
            // 
            // lblMemoriaTotal
            // 
            lblMemoriaTotal.AutoSize = true;
            lblMemoriaTotal.Font = new Font("Sitka Subheading", 14.2499981F);
            lblMemoriaTotal.ForeColor = Color.Goldenrod;
            lblMemoriaTotal.Location = new Point(642, 102);
            lblMemoriaTotal.Name = "lblMemoriaTotal";
            lblMemoriaTotal.Size = new Size(242, 28);
            lblMemoriaTotal.TabIndex = 4;
            lblMemoriaTotal.Text = "MEMORIA TOTAL: 500 MBs";
            lblMemoriaTotal.Click += lblMemoriaTotal_Click;
            // 
            // PrimerAjuste
            // 
            PrimerAjuste.BackColor = Color.PaleGreen;
            PrimerAjuste.Font = new Font("Sitka Subheading", 14.2499981F);
            PrimerAjuste.Location = new Point(5, 187);
            PrimerAjuste.Name = "PrimerAjuste";
            PrimerAjuste.Size = new Size(110, 40);
            PrimerAjuste.TabIndex = 79;
            PrimerAjuste.Text = "PRIMER";
            PrimerAjuste.UseVisualStyleBackColor = false;
            PrimerAjuste.Click += PrimerAjuste_Click;
            // 
            // MejorAjuste
            // 
            MejorAjuste.BackColor = Color.PaleGreen;
            MejorAjuste.Font = new Font("Sitka Subheading", 14.2499981F);
            MejorAjuste.Location = new Point(5, 267);
            MejorAjuste.Name = "MejorAjuste";
            MejorAjuste.Size = new Size(110, 40);
            MejorAjuste.TabIndex = 80;
            MejorAjuste.Text = "MEJOR";
            MejorAjuste.UseVisualStyleBackColor = false;
            MejorAjuste.Click += MejorAjuste_Click;
            // 
            // PeorAjuste
            // 
            PeorAjuste.BackColor = Color.PaleGreen;
            PeorAjuste.Font = new Font("Sitka Subheading", 14.2499981F);
            PeorAjuste.Location = new Point(5, 350);
            PeorAjuste.Name = "PeorAjuste";
            PeorAjuste.Size = new Size(110, 40);
            PeorAjuste.TabIndex = 81;
            PeorAjuste.Text = "PEOR";
            PeorAjuste.UseVisualStyleBackColor = false;
            PeorAjuste.Click += PeorAjuste_Click;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(151, 140, 235);
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Font = new Font("Sitka Subheading", 14.2499981F);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(186, 65);
            label2.Name = "label2";
            label2.Size = new Size(180, 35);
            label2.TabIndex = 83;
            label2.Text = "COLA";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(151, 140, 235);
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Sitka Subheading", 14.2499981F);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(439, 65);
            label1.Name = "label1";
            label1.Size = new Size(180, 35);
            label1.TabIndex = 84;
            label1.Text = "PILA";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Sitka Subheading", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label21.ForeColor = SystemColors.ButtonHighlight;
            label21.Location = new Point(217, 9);
            label21.Name = "label21";
            label21.Size = new Size(419, 39);
            label21.TabIndex = 85;
            label21.Text = "DISTRIBUCION DE PROCESOS ISC";
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(151, 140, 235);
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Font = new Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(5, 102);
            label3.Name = "label3";
            label3.Size = new Size(110, 50);
            label3.TabIndex = 86;
            label3.Text = "ELIJE UN TIPO DE AJUSTE";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // Eliminar
            // 
            Eliminar.Location = new Point(0, 0);
            Eliminar.Name = "Eliminar";
            Eliminar.Size = new Size(75, 23);
            Eliminar.TabIndex = 0;
            // 
            // txtIdEliminar
            // 
            txtIdEliminar.Font = new Font("Sitka Subheading", 14.2499981F);
            txtIdEliminar.Location = new Point(687, 409);
            txtIdEliminar.Name = "txtIdEliminar";
            txtIdEliminar.PlaceholderText = "ID:";
            txtIdEliminar.Size = new Size(85, 31);
            txtIdEliminar.TabIndex = 87;
            txtIdEliminar.TextAlign = HorizontalAlignment.Center;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Salmon;
            btnEliminar.Font = new Font("Sitka Subheading", 14.2499981F);
            btnEliminar.Location = new Point(792, 403);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(110, 46);
            btnEliminar.TabIndex = 88;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click_1;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Sitka Subheading", 14.2499981F);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(687, 345);
            label4.Name = "label4";
            label4.Size = new Size(215, 31);
            label4.TabIndex = 89;
            label4.Text = "PROCESO A ELIMINAR POR MEDIO DE SU ID:";
            // 
            // lblMemoriaDisponible
            // 
            lblMemoriaDisponible.AutoSize = true;
            lblMemoriaDisponible.Font = new Font("Sitka Subheading", 14.2499981F);
            lblMemoriaDisponible.ForeColor = SystemColors.ButtonHighlight;
            lblMemoriaDisponible.Location = new Point(642, 172);
            lblMemoriaDisponible.Name = "lblMemoriaDisponible";
            lblMemoriaDisponible.Size = new Size(269, 28);
            lblMemoriaDisponible.TabIndex = 90;
            lblMemoriaDisponible.Text = "MEMORIA DISPONIBLE: 0 MBs";
            // 
            // lblEstasEn
            // 
            lblEstasEn.Font = new Font("Sitka Subheading", 15.7499981F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblEstasEn.ForeColor = Color.Fuchsia;
            lblEstasEn.Location = new Point(673, 216);
            lblEstasEn.Name = "lblEstasEn";
            lblEstasEn.Size = new Size(202, 64);
            lblEstasEn.TabIndex = 91;
            lblEstasEn.Text = " SELECCIONASTE ";
            lblEstasEn.TextAlign = ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            BackColor = Color.PaleGoldenrod;
            ClientSize = new Size(945, 589);
            Controls.Add(lblEstasEn);
            Controls.Add(lblMemoriaDisponible);
            Controls.Add(label4);
            Controls.Add(btnEliminar);
            Controls.Add(txtIdEliminar);
            Controls.Add(label3);
            Controls.Add(label21);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(PeorAjuste);
            Controls.Add(MejorAjuste);
            Controls.Add(PrimerAjuste);
            Controls.Add(lblMemoriaTotal);
            Controls.Add(lblMemoriaUtilizada);
            Controls.Add(BtnContinuar);
            Controls.Add(BtnDetener);
            ForeColor = SystemColors.ControlText;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Simulador de Memoria";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblMemoriaTotal;
        private Button PrimerAjuste;
        private Button MejorAjuste;
        private Button PeorAjuste;
        private Label label2;
        private Label label1;
        private Label label21;
        private Label label3;
        private Button Eliminar;
        private TextBox txtIdEliminar;
        private Button btnEliminar;
        private Label label4;
        private Label lblMemoriaDisponible;
        private Label lblEstasEn;
    }
}

