namespace Login.fomrs
{
    partial class Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btncalificaciones = new System.Windows.Forms.Button();
            this.btnalumnos = new System.Windows.Forms.Button();
            this.btnasistencias = new System.Windows.Forms.Button();
            this.btnmaterias = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1632, 50);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btncalificaciones);
            this.panel2.Controls.Add(this.btnalumnos);
            this.panel2.Controls.Add(this.btnasistencias);
            this.panel2.Controls.Add(this.btnmaterias);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(349, 806);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint_1);
            // 
            // btncalificaciones
            // 
            this.btncalificaciones.BackColor = System.Drawing.Color.White;
            this.btncalificaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncalificaciones.Font = new System.Drawing.Font("Nirmala Text", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncalificaciones.ForeColor = System.Drawing.Color.White;
            this.btncalificaciones.Location = new System.Drawing.Point(0, 275);
            this.btncalificaciones.Name = "btncalificaciones";
            this.btncalificaciones.Size = new System.Drawing.Size(346, 52);
            this.btncalificaciones.TabIndex = 3;
            this.btncalificaciones.Text = "Calidicaciones";
            this.btncalificaciones.UseVisualStyleBackColor = false;
            this.btncalificaciones.Click += new System.EventHandler(this.btncalificaciones_Click);
            // 
            // btnalumnos
            // 
            this.btnalumnos.BackColor = System.Drawing.Color.White;
            this.btnalumnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnalumnos.Font = new System.Drawing.Font("Nirmala Text", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnalumnos.ForeColor = System.Drawing.Color.White;
            this.btnalumnos.Location = new System.Drawing.Point(0, 333);
            this.btnalumnos.Name = "btnalumnos";
            this.btnalumnos.Size = new System.Drawing.Size(346, 52);
            this.btnalumnos.TabIndex = 2;
            this.btnalumnos.Text = "Alumnos";
            this.btnalumnos.UseVisualStyleBackColor = false;
            this.btnalumnos.Click += new System.EventHandler(this.btnalumnos_Click);
            // 
            // btnasistencias
            // 
            this.btnasistencias.BackColor = System.Drawing.Color.White;
            this.btnasistencias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnasistencias.Font = new System.Drawing.Font("Nirmala Text", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnasistencias.ForeColor = System.Drawing.Color.White;
            this.btnasistencias.Location = new System.Drawing.Point(0, 391);
            this.btnasistencias.Name = "btnasistencias";
            this.btnasistencias.Size = new System.Drawing.Size(346, 52);
            this.btnasistencias.TabIndex = 1;
            this.btnasistencias.Text = "Asistencias ";
            this.btnasistencias.UseVisualStyleBackColor = false;
            this.btnasistencias.Click += new System.EventHandler(this.btnasistencias_Click);
            // 
            // btnmaterias
            // 
            this.btnmaterias.BackColor = System.Drawing.Color.White;
            this.btnmaterias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmaterias.Font = new System.Drawing.Font("Nirmala Text", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmaterias.ForeColor = System.Drawing.Color.White;
            this.btnmaterias.Location = new System.Drawing.Point(0, 217);
            this.btnmaterias.Name = "btnmaterias";
            this.btnmaterias.Size = new System.Drawing.Size(346, 52);
            this.btnmaterias.TabIndex = 0;
            this.btnmaterias.Text = "Materias";
            this.btnmaterias.UseVisualStyleBackColor = false;
            this.btnmaterias.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(349, 50);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1283, 806);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(90, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1632, 856);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Principal";
            this.Text = "Principal";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnmaterias;
        private System.Windows.Forms.Button btncalificaciones;
        private System.Windows.Forms.Button btnalumnos;
        private System.Windows.Forms.Button btnasistencias;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
    }
}