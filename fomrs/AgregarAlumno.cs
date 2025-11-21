using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Login.fomrs
{
    public partial class AgregarAlumno : Form
    {
        private MySqlConnection conexion;
        private int idMateria;

        public string NombreAlumno => txtNombre.Text.Trim();
        public string ApellidoAlumno => txtApellido.Text.Trim();

        public AgregarAlumno(MySqlConnection conexion, int idMateria)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.idMateria = idMateria;
        }

      
        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (NombreAlumno == "" || ApellidoAlumno == "")
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
