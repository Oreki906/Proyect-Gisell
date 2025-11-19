using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Login.fomrs
{
    public partial class detalle_usuario : Form
    {

        private Form ventanaPrincipal;

        public detalle_usuario()
        {
            InitializeComponent();
        }


        
        private void detalle_usuario_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }
       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Principal principal = new Principal();
            principal.Show();
            this.Close();
        }
    }
}
