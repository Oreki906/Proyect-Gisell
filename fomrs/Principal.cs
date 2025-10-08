using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.fomrs
{
    public partial class Principal : Form

    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public Principal()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            


        }
       


        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            panel2.BackColor= Color.FromArgb(25, 42, 86);
            btncalificaciones.BackColor = Color.FromArgb(25, 42, 86);
            btnalumnos.BackColor = Color.FromArgb(25, 42, 86);
            btnasistencias.BackColor = Color.FromArgb(25, 42, 86);
            btnmaterias.BackColor = Color.FromArgb(25, 42, 86);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            panel3.BackColor = Color.FromArgb(40, 40, 40);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.RoyalBlue;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void btncalificaciones_Click(object sender, EventArgs e)
        {
            
        }

        private void btnalumnos_Click(object sender, EventArgs e)
        {
            
        }

        private void btnasistencias_Click(object sender, EventArgs e)
        {
            btnasistencias.BackColor = Color.FromArgb(25, 42, 86);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitle_MouseDown);

        }
    }
}
