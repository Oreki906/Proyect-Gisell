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
        private string mySqlDatabase = "proyectogisell";
        private string mySqlUserId = "maestro";
        private string mySqlPassword = "12345";

        // conexión global
        private MySqlConnection conexion;

        private Agenda agenda;

        public Principal()
        {
            InitializeComponent();
            

            this.FormBorderStyle = FormBorderStyle.None;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(40, 40, 40);
            agregar.Visible = false;
            //panel1.BackColor = Color.FromArgb(0, 41, 204);
            //panel2.BackColor = Color.FromArgb(0, 31, 153);
            panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;




            


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
           panel2.BackColor = Color.FromArgb(25, 42, 86);
            btncalificaciones.BackColor = Color.FromArgb(25, 42, 86);
            btncalificaciones.FlatAppearance.BorderSize = 0;
            btnalumnos.BackColor = Color.FromArgb(25, 42, 86);
            btnalumnos.FlatAppearance.BorderSize = 0;
            btnasistencias.BackColor = Color.FromArgb(25, 42, 86);
            btnasistencias.FlatAppearance.BorderSize = 0;
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
            agregar.Visible = true;
            CargarMaterias();

        }

        private void btncalificaciones_Click(object sender, EventArgs e)
        {
            label1.Text = "Calificaciones";
            flowLayoutPanel1.Controls.Clear();
            agregar.Visible = false;
        }

        private void btnalumnos_Click(object sender, EventArgs e)
        {
            /*(Antes de modificar) 
             agregar.Visible=false;
              label1.Text = "Alumnos";
              flowLayoutPanel1.Controls.Clear();
              DataGridView alumnos = new DataGridView();
              flowLayoutPanel1.Controls.Add(alumnos);
              alumnos.Size = flowLayoutPanel1.Size;
              alumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
              alumnos.Columns.Add("alumnos", "Alumnos");
              alumnos.Columns.Add("calificaciones", "Calificaciones");
            */
            label1.Text = "Alumnos";
            flowLayoutPanel1.Controls.Clear();

            // Crea el DataGridView
            DataGridView alumnos = new DataGridView
            {
                Size = flowLayoutPanel1.Size,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            flowLayoutPanel1.Controls.Add(alumnos);
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    string strConn = $"Server={mySqlServer};Database={mySqlDatabase};Uid={mySqlUserId};Pwd={mySqlPassword};";
                    conexion = new MySqlConnection(strConn);
                    conexion.Open();
                }

                // Consulta SQL para traer alumnos y calificaciones
                string consulta = @"
            SELECT 
                CONCAT(a.Nombre, ' ', a.Apellido) AS Alumno,
                IFNULL(c.Calificacion, 'Sin calificación') AS Calificacion
            FROM Alumnos a
            LEFT JOIN Calificacion c ON a.id_Alumno = c.id_Alumno;
        ";
                // Ejecutar consulta y llenar el DataGridView
                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    alumnos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message);
            }
        }


        private void btnasistencias_Click(object sender, EventArgs e)
        {
            btnasistencias.BackColor = Color.FromArgb(25, 42, 86);
            label1.Text = "Asistencias";
            agregar.Visible = false;
            flowLayoutPanel1.Controls.Clear();

            DataGridView asistencias = new DataGridView();
            flowLayoutPanel1.Controls.Add(asistencias);
            asistencias.Size = flowLayoutPanel1.Size;
            asistencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            asistencias.Columns.Add("alumnos", "Alumnos");
            asistencias.Columns.Add("asistencias", "Asistencias");

            asistencias.DefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitle_MouseDown);
          
        }
        private void CrearMateria(string nombre, int idMateria)
        {
            // Panel contenedor
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
                if (conexion == null || conexion.State != ConnectionState.Open)
                    ConectarBD();

               listaplaneaciones lista = new listaplaneaciones(idMateria, nombre, conexion);
                lista.TopLevel = false;
                lista.Dock = DockStyle.Fill;

                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(lista);
                lista.Show();
                

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

        private void agregar_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            listaTarjetas.Clear();

            using (var nuevaMateria = new NuevaMateria())
            {
                var resultado = nuevaMateria.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    string nombreMateria = nuevaMateria.NombreMateria;
                    int cantidadAlumnos = nuevaMateria.CantidadAlumnos;

                    int idUsuario = 1;

                    string insertQuery = "INSERT INTO Materias (Nombre_Materia, Cantidad_Alumnos, id_Usuario) VALUES (@nombre, @cantidad, @idUsuario)";
                    try
                    {
                        if (conexion == null || conexion.State != ConnectionState.Open)
                            ConectarBD();

                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, conexion))
                        {
                            cmd.Parameters.AddWithValue("@nombre", nombreMateria);
                            cmd.Parameters.AddWithValue("@cantidad", cantidadAlumnos);
                            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Materia creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Recargar materias desde la base para actualizar la vista
                        CargarMaterias();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al crear materia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Usuario canceló, no hacer nada
                }
            }
        }
        private void CargarMaterias()
        {
            listaTarjetas.Clear();
            flowLayoutPanel1.Controls.Clear();

            string consulta = "SELECT id_Materia, Nombre_Materia FROM Materias";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                    ConectarBD();

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader.GetString("Nombre_Materia");
                        int idMateria = reader.GetInt32("id_Materia");
                        CrearMateria(nombre, idMateria);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar materias: " + ex.Message);
            }
        }

        private void roundedPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            detalle_usuario usuario = new detalle_usuario();
            usuario.Show();
            this.Hide();
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
            DesconectarBD();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAgenda_Click(object sender, EventArgs e)
        {
            if (agenda == null || agenda.IsDisposed)
            {
                agenda = new Agenda(this, conexion);
                agenda.Show();
            }
            else
            {
                agenda.BringToFront();
                agenda.Focus();
            }
        }
    }
}
