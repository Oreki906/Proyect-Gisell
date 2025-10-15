using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Login.fomrs
{
    public partial class Principal : Form

    {

        //Datos de conexión
        private string mySqlServer = "localhost";
        private string mySqlDatabase = "ProyectoGisell";
        private string mySqlUserId = "root";
        private string mySqlPassword = "";

        // conexión global
        private MySqlConnection conexion;

        public Principal()
        {
            InitializeComponent();
            ConectarBD(); 
            
            this.FormBorderStyle = FormBorderStyle.None;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(40, 40, 40);
        }

       
        private void ConectarBD()
        {
            try
            {
                string strConn = $"Server={mySqlServer};Database={mySqlDatabase};Uid={mySqlUserId};Pwd={mySqlPassword};";
                conexion = new MySqlConnection(strConn);
                conexion.Open();
               
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error al conectar a MySQL: {ex.Message}",
                                "Error de Conexión",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

       
        private void DesconectarBD()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
                MessageBox.Show(" Conexión cerrada correctamente.",
                                "Desconexión",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
        //para ejecutar comandos no borrar.... Julio 
        
        private void EjecutaComando(string ConsultaSQL)
        {
            try
            {
                if (conexion == null || conexion.State != System.Data.ConnectionState.Open)
                    ConectarBD(); // reconecta si se cerró

                using (MySqlCommand cmd = new MySqlCommand(ConsultaSQL, conexion))
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Comando ejecutado correctamente.",
                                    "Éxito",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error de MySQL: {ex.Message}",
                                "Error SQL",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general: {ex.Message}",
                                "Error del sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

       
        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DesconectarBD();
        }
    




        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // En esta lista guardamos las tarjetas que corresponden a las materias.
        private List<Panel> listaTarjetas = new List<Panel>();

        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
       


        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            panel2.BackColor= Color.FromArgb(25, 42, 86);
            btncalificaciones.BackColor = Color.FromArgb(25, 42, 86);
            btncalificaciones.FlatAppearance.BorderSize = 0;
            btnalumnos.BackColor = Color.FromArgb(25, 42, 86);
            btnalumnos.FlatAppearance.BorderSize = 0;
            btnasistencias.BackColor = Color.FromArgb(25, 42, 86);
            btnasistencias.FlatAppearance.BorderSize= 0;
            btnmaterias.BackColor = Color.FromArgb(25, 42, 86);
            btnmaterias.FlatAppearance.BorderSize = 0;
        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.RoyalBlue;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Materias";
            flowLayoutPanel1.Controls.Clear();
            foreach (var t in listaTarjetas)
            {
                flowLayoutPanel1.Controls.Add(t);
            }

        }

        private void btncalificaciones_Click(object sender, EventArgs e)
        {
            label1.Text = "Calificaciones";
            flowLayoutPanel1.Controls.Clear();
        }

        private void btnalumnos_Click(object sender, EventArgs e)
        {
            label1.Text = "Alumnos";
            flowLayoutPanel1.Controls.Clear();
            DataGridView alumnos = new DataGridView();
            flowLayoutPanel1.Controls.Add(alumnos);
            alumnos.Size = flowLayoutPanel1.Size;
            alumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            alumnos.Columns.Add("alumnos", "Alumnos");
            alumnos.Columns.Add("calificaciones", "Calificaciones");
        }

        private void btnasistencias_Click(object sender, EventArgs e)
        {
            btnasistencias.BackColor = Color.FromArgb(25, 42, 86);
            label1.Text = "Asistencias";
            flowLayoutPanel1.Controls.Clear();

            DataGridView asistencias = new DataGridView();
            flowLayoutPanel1.Controls.Add(asistencias);
            asistencias.Size = flowLayoutPanel1.Size;
            asistencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            asistencias.Columns.Add("alumnos", "Alumnos");
            asistencias.Columns.Add("asistencias", "Asistencias");

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitle_MouseDown);

        }
        private void CrearMateria(string nombre)
        {
            // Panel contenedor (la “tarjeta”)
            Panel tarjeta = new Panel();
            tarjeta.Width = 250;
            tarjeta.Height = 100;
            tarjeta.Margin = new Padding(10);
            tarjeta.BackColor = Color.FromArgb(20, 25, 60);
            tarjeta.BorderStyle = BorderStyle.FixedSingle;
            tarjeta.Padding = new Padding(5);

            // Etiqueta con el nombre
            Label lbl = new Label();
            lbl.Text = nombre;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lbl.Dock = DockStyle.Top;
            lbl.TextAlign = ContentAlignment.MiddleCenter;

            // Botón para abrir el formulario de la materia
            Button btnAbrir = new Button();
            btnAbrir.Text = "Abrir";
            btnAbrir.Dock = DockStyle.Bottom;
            btnAbrir.Height = 30;
            btnAbrir.FlatStyle = FlatStyle.Flat;
            btnAbrir.ForeColor = Color.White;
            btnAbrir.BackColor = Color.FromArgb(40, 60, 120);

            btnAbrir.Click += (s, e) =>
            {
               // FormMateria form = new FormMateria(nombre);
                //((FormPrincipal)Application.OpenForms["FormPrincipal"]).AbrirFormulario(form);
            };

            // Agregar controles a la tarjeta
            tarjeta.Controls.Add(lbl);
            tarjeta.Controls.Add(btnAbrir);

            // Agregar controles a la lista de tarjetas
            listaTarjetas.Add(tarjeta);

            // Agregar tarjetas al FlowLayoutPanel
            foreach (var t in listaTarjetas)
            {
                flowLayoutPanel1.Controls.Add(tarjeta);
            }

        }
        private int contador = 1;
        private void button1_Click_1(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var t in listaTarjetas)
            {
                flowLayoutPanel1.Controls.Add(t);
            }
            CrearMateria($"Materia {contador}");
            contador++;
        }
    }
}
