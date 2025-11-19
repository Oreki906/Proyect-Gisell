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
    public partial class NuevaMateria : Form
    {
        public string NombreMateria { get; private set; }
        public int CantidadAlumnos { get; private set; }

        private TextBox txtNombre;
        private NumericUpDown numCantidad;

        // Constructor para crear
        public NuevaMateria()
        {
            InitializeComponent();
            ConfigurarForm();
        }

        // Constructor para editar 
        public NuevaMateria(string nombreExistente, int cantidadExistente) : this()
        {
            txtNombre.Text = nombreExistente;
            numCantidad.Value = cantidadExistente;
        }

        private void ConfigurarForm()
        {
            this.Text = "Nueva Materia";
            this.Size = new Size(300, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblNombre = new Label() { Text = "Nombre:", Location = new Point(10, 20), AutoSize = true };
            txtNombre = new TextBox() { Location = new Point(80, 18), Width = 180 };

            Label lblCantidad = new Label() { Text = "Cantidad Alumnos:", Location = new Point(10, 60), AutoSize = true };
            numCantidad = new NumericUpDown() { Location = new Point(130, 58), Width = 130, Minimum = 0, Maximum = 1000 };

            Button btnAceptar = new Button() { Text = "Aceptar", Location = new Point(60, 100), DialogResult = DialogResult.OK };
            Button btnCancelar = new Button() { Text = "Cancelar", Location = new Point(160, 100), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblCantidad);
            this.Controls.Add(numCantidad);
            this.Controls.Add(btnAceptar);
            this.Controls.Add(btnCancelar);

            this.AcceptButton = btnAceptar;
            this.CancelButton = btnCancelar;

            btnAceptar.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                NombreMateria = txtNombre.Text.Trim();
                CantidadAlumnos = (int)numCantidad.Value;
            };
        }
    }

}
