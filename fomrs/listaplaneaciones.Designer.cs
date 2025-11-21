namespace Login.fomrs
{
    partial class listaplaneaciones
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvPlaneaciones = new System.Windows.Forms.DataGridView();
            this.btnNueva = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.btnbtnExporatrPDF = new System.Windows.Forms.Button();
            this.dgvUnidades = new System.Windows.Forms.DataGridView();
            this.btnGuardarUnidades = new System.Windows.Forms.Button();
            this.btnAgregarAlumno = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaneaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnidades)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(322, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(81, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "label1";
            // 
            // dgvPlaneaciones
            // 
            this.dgvPlaneaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlaneaciones.Location = new System.Drawing.Point(14, 455);
            this.dgvPlaneaciones.Name = "dgvPlaneaciones";
            this.dgvPlaneaciones.RowHeadersWidth = 51;
            this.dgvPlaneaciones.RowTemplate.Height = 24;
            this.dgvPlaneaciones.Size = new System.Drawing.Size(389, 215);
            this.dgvPlaneaciones.TabIndex = 1;
            // 
            // btnNueva
            // 
            this.btnNueva.Location = new System.Drawing.Point(14, 422);
            this.btnNueva.Name = "btnNueva";
            this.btnNueva.Size = new System.Drawing.Size(89, 27);
            this.btnNueva.TabIndex = 2;
            this.btnNueva.Text = "Nueva";
            this.btnNueva.UseVisualStyleBackColor = true;
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);
            // 
            // btnVer
            // 
            this.btnVer.Location = new System.Drawing.Point(109, 422);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(89, 27);
            this.btnVer.TabIndex = 2;
            this.btnVer.Text = "Ver o editar";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnbtnExporatrPDF
            // 
            this.btnbtnExporatrPDF.Location = new System.Drawing.Point(204, 422);
            this.btnbtnExporatrPDF.Name = "btnbtnExporatrPDF";
            this.btnbtnExporatrPDF.Size = new System.Drawing.Size(89, 27);
            this.btnbtnExporatrPDF.TabIndex = 2;
            this.btnbtnExporatrPDF.Text = "Exportar";
            this.btnbtnExporatrPDF.UseVisualStyleBackColor = true;
            this.btnbtnExporatrPDF.Click += new System.EventHandler(this.btnbtnExporatrPDF_Click);
            // 
            // dgvUnidades
            // 
            this.dgvUnidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnidades.Location = new System.Drawing.Point(12, 104);
            this.dgvUnidades.Name = "dgvUnidades";
            this.dgvUnidades.RowHeadersWidth = 51;
            this.dgvUnidades.RowTemplate.Height = 24;
            this.dgvUnidades.Size = new System.Drawing.Size(898, 310);
            this.dgvUnidades.TabIndex = 3;
            this.dgvUnidades.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUnidades_CellContentClick);
            // 
            // btnGuardarUnidades
            // 
            this.btnGuardarUnidades.Location = new System.Drawing.Point(38, 62);
            this.btnGuardarUnidades.Name = "btnGuardarUnidades";
            this.btnGuardarUnidades.Size = new System.Drawing.Size(106, 32);
            this.btnGuardarUnidades.TabIndex = 4;
            this.btnGuardarUnidades.Text = "button1";
            this.btnGuardarUnidades.UseVisualStyleBackColor = true;
            this.btnGuardarUnidades.Click += new System.EventHandler(this.btnGuardarUnidades_Click);
            // 
            // btnAgregarAlumno
            // 
            this.btnAgregarAlumno.Location = new System.Drawing.Point(166, 62);
            this.btnAgregarAlumno.Name = "btnAgregarAlumno";
            this.btnAgregarAlumno.Size = new System.Drawing.Size(95, 31);
            this.btnAgregarAlumno.TabIndex = 5;
            this.btnAgregarAlumno.Text = "agregar";
            this.btnAgregarAlumno.UseVisualStyleBackColor = true;
            this.btnAgregarAlumno.Click += new System.EventHandler(this.btnAgregarAlumno_Click);
            // 
            // listaplaneaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(734, 578);
            this.Controls.Add(this.btnAgregarAlumno);
            this.Controls.Add(this.btnGuardarUnidades);
            this.Controls.Add(this.dgvUnidades);
            this.Controls.Add(this.btnbtnExporatrPDF);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.btnNueva);
            this.Controls.Add(this.dgvPlaneaciones);
            this.Controls.Add(this.lblTitulo);
            this.Name = "listaplaneaciones";
            this.Text = "listaplaneaciones";
            this.Load += new System.EventHandler(this.listaplaneaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaneaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvPlaneaciones;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnbtnExporatrPDF;
        private System.Windows.Forms.DataGridView dgvUnidades;
        private System.Windows.Forms.Button btnGuardarUnidades;
        private System.Windows.Forms.Button btnAgregarAlumno;
    }
}