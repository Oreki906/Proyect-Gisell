namespace Login.fomrs
{
    partial class Agenda
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
            this.tblAgenda = new System.Windows.Forms.TableLayoutPanel();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.lblSemana = new System.Windows.Forms.Label();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblAgenda
            // 
            this.tblAgenda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.tblAgenda.ColumnCount = 2;
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAgenda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAgenda.Location = new System.Drawing.Point(0, 49);
            this.tblAgenda.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tblAgenda.Name = "tblAgenda";
            this.tblAgenda.RowCount = 2;
            this.tblAgenda.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAgenda.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAgenda.Size = new System.Drawing.Size(1067, 505);
            this.tblAgenda.TabIndex = 1;
            // 
            // btnAnterior
            // 
            this.btnAnterior.ForeColor = System.Drawing.Color.Black;
            this.btnAnterior.Location = new System.Drawing.Point(37, 16);
            this.btnAnterior.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(100, 28);
            this.btnAnterior.TabIndex = 0;
            this.btnAnterior.Text = "<<";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // lblSemana
            // 
            this.lblSemana.AutoSize = true;
            this.lblSemana.Location = new System.Drawing.Point(519, 16);
            this.lblSemana.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSemana.Name = "lblSemana";
            this.lblSemana.Size = new System.Drawing.Size(44, 16);
            this.lblSemana.TabIndex = 1;
            this.lblSemana.Text = "label1";
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.ForeColor = System.Drawing.Color.Black;
            this.btnSiguiente.Location = new System.Drawing.Point(929, 16);
            this.btnSiguiente.Margin = new System.Windows.Forms.Padding(4);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(100, 28);
            this.btnSiguiente.TabIndex = 2;
            this.btnSiguiente.Text = ">>";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelHeader.Controls.Add(this.btnSiguiente);
            this.panelHeader.Controls.Add(this.lblSemana);
            this.panelHeader.Controls.Add(this.btnAnterior);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1067, 49);
            this.panelHeader.TabIndex = 0;
            // 
            // Agenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.tblAgenda);
            this.Controls.Add(this.panelHeader);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Agenda";
            this.Text = "Agenda";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tblAgenda;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Label lblSemana;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Panel panelHeader;
    }
}