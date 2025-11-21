namespace Login.fomrs
{
    partial class FichaAlumno
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
            this.btnexportar = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelContenido = new System.Windows.Forms.FlowLayoutPanel();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(323, 64);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(92, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "label1";
            // 
            // btnexportar
            // 
            this.btnexportar.Location = new System.Drawing.Point(856, 12);
            this.btnexportar.Name = "btnexportar";
            this.btnexportar.Size = new System.Drawing.Size(122, 41);
            this.btnexportar.TabIndex = 0;
            this.btnexportar.Text = "exportar a PDF";
            this.btnexportar.UseVisualStyleBackColor = true;
            this.btnexportar.Click += new System.EventHandler(this.btnexportar_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.AutoScroll = true;
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.btnexportar);
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Location = new System.Drawing.Point(2, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(999, 155);
            this.panelHeader.TabIndex = 1;
            // 
            // panelContenido
            // 
            this.panelContenido.AutoScroll = true;
            this.panelContenido.BackColor = System.Drawing.Color.White;
            this.panelContenido.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelContenido.Location = new System.Drawing.Point(2, 153);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(999, 641);
            this.panelContenido.TabIndex = 2;
            this.panelContenido.WrapContents = false;
            // 
            // FichaAlumno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 792);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelHeader);
            this.Name = "FichaAlumno";
            this.Text = "FichaAlumno";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnexportar;
        private System.Windows.Forms.FlowLayoutPanel panelContenido;
    }
}