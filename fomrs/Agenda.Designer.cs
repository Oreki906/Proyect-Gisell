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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.lblSemana = new System.Windows.Forms.Label();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.tblAgenda = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.btnSiguiente);
            this.panelHeader.Controls.Add(this.lblSemana);
            this.panelHeader.Controls.Add(this.btnAnterior);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(800, 40);
            this.panelHeader.TabIndex = 0;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(697, 13);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(75, 23);
            this.btnSiguiente.TabIndex = 2;
            this.btnSiguiente.Text = ">>";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // lblSemana
            // 
            this.lblSemana.AutoSize = true;
            this.lblSemana.Location = new System.Drawing.Point(389, 13);
            this.lblSemana.Name = "lblSemana";
            this.lblSemana.Size = new System.Drawing.Size(35, 13);
            this.lblSemana.TabIndex = 1;
            this.lblSemana.Text = "label1";
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(28, 13);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 23);
            this.btnAnterior.TabIndex = 0;
            this.btnAnterior.Text = "<<";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // tblAgenda
            // 
            this.tblAgenda.ColumnCount = 2;
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAgenda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAgenda.Location = new System.Drawing.Point(0, 40);
            this.tblAgenda.Name = "tblAgenda";
            this.tblAgenda.RowCount = 2;
            this.tblAgenda.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAgenda.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAgenda.Size = new System.Drawing.Size(800, 410);
            this.tblAgenda.TabIndex = 1;
            // 
            // Agenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tblAgenda);
            this.Controls.Add(this.panelHeader);
            this.Name = "Agenda";
            this.Text = "Agenda";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Label lblSemana;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.TableLayoutPanel tblAgenda;
    }
}