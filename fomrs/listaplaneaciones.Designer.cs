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
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaneaciones)).BeginInit();
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
            this.dgvPlaneaciones.Location = new System.Drawing.Point(6, 95);
            this.dgvPlaneaciones.Name = "dgvPlaneaciones";
            this.dgvPlaneaciones.RowHeadersWidth = 51;
            this.dgvPlaneaciones.RowTemplate.Height = 24;
            this.dgvPlaneaciones.Size = new System.Drawing.Size(796, 358);
            this.dgvPlaneaciones.TabIndex = 1;
            // 
            // btnNueva
            // 
            this.btnNueva.Location = new System.Drawing.Point(41, 57);
            this.btnNueva.Name = "btnNueva";
            this.btnNueva.Size = new System.Drawing.Size(89, 27);
            this.btnNueva.TabIndex = 2;
            this.btnNueva.Text = "Nueva";
            this.btnNueva.UseVisualStyleBackColor = true;
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);
            // 
            // btnVer
            // 
            this.btnVer.Location = new System.Drawing.Point(136, 57);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(89, 27);
            this.btnVer.TabIndex = 2;
            this.btnVer.Text = "Ver o editar";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnbtnExporatrPDF
            // 
            this.btnbtnExporatrPDF.Location = new System.Drawing.Point(231, 57);
            this.btnbtnExporatrPDF.Name = "btnbtnExporatrPDF";
            this.btnbtnExporatrPDF.Size = new System.Drawing.Size(89, 27);
            this.btnbtnExporatrPDF.TabIndex = 2;
            this.btnbtnExporatrPDF.Text = "Exportar";
            this.btnbtnExporatrPDF.UseVisualStyleBackColor = true;
            this.btnbtnExporatrPDF.Click += new System.EventHandler(this.btnbtnExporatrPDF_Click);
            // 
            // listaplaneaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnbtnExporatrPDF);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.btnNueva);
            this.Controls.Add(this.dgvPlaneaciones);
            this.Controls.Add(this.lblTitulo);
            this.Name = "listaplaneaciones";
            this.Text = "listaplaneaciones";
            this.Load += new System.EventHandler(this.listaplaneaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaneaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvPlaneaciones;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnbtnExporatrPDF;
    }
}