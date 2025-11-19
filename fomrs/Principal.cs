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
        public MySqlConnection conexion;

        public Principal(int idUsuario = 0)
        {
            InitializeComponent();
            ConectarBD();
            
            this.FormBorderStyle = FormBorderStyle.None;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(40, 40, 40);
            agregar.Visible = false;

            idUsuarioActual = idUsuario;
            CargarDatosUsuario();
        }


        public void ConectarBD()
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
        public void DesconectarBD()
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
            CargarMaterias();
            label1.Text = "Materias";
            flowLayoutPanel1.Controls.Clear();
            foreach (var t in listaTarjetas)
            {
                flowLayoutPanel1.Controls.Add(t);
            }
            agregar.Visible = true;
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
            label1.Text = "Asistencias";
            agregar.Visible = false;
            flowLayoutPanel1.Controls.Clear();

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                    ConectarBD();
                // Panel contenedor
                Panel contenedor = new Panel
                {
                    Width = flowLayoutPanel1.Width - 20,
                    Height = flowLayoutPanel1.Height - 20,
                    BackColor = Color.FromArgb(50, 50, 50),
                    AutoScroll = true
                };
                // DataGridView
                DataGridView asistencias = new DataGridView
                {
                    Dock = DockStyle.Top,
                    Height = contenedor.Height - 50,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    AllowUserToAddRows = false,
                    ReadOnly = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect
                };
                contenedor.Controls.Add(asistencias);
                // Cargar datos
                string consulta = "SELECT Nombre, Apellido, Matricula, Grado FROM Alumnos";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    asistencias.DataSource = dt;
                    // Columna de estado de asistencia
                    DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn
                    {
                        HeaderText = "Asistencia",
                        Name = "EstadoAsistencia",
                        DataSource = new string[] { "Presente", "Ausente", "Retardo" }
                    };
                    asistencias.Columns.Add(comboCol);
                }
                // Botón Guardar
                Button btnGuardar = new Button
                {
                    Text = "Guardar Asistencias",
                    Height = 40,
                    Dock = DockStyle.Bottom,
                    BackColor = Color.MediumSeaGreen,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnGuardar.Click += (s, ev) => GuardarAsistencias(asistencias);
                contenedor.Controls.Add(btnGuardar);
                // Agregar contenedor al FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(contenedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asistencias: " + ex.Message);
            }
        }

        private void GuardarAsistencias(DataGridView dgv)
        {
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                    ConectarBD();

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells["EstadoAsistencia"].Value == null) continue;

                    int idAlumno = Convert.ToInt32(row.Cells["id_Alumno"].Value);
                    string estado = row.Cells["EstadoAsistencia"].Value.ToString();

                    // Procedimiento
                    ConexionBD.EjecutarProcedimiento("proRegistrarAsistencia",
                        ("p_idAlumno", idAlumno),
                        ("p_estado", estado)
                    );
                }

                MessageBox.Show("Asistencias guardadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar asistencias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitle_MouseDown);
            btnSalir.BackColor = Color.LightCyan;
        }
        private void CrearMateria(string nombre, int idMateria)
        {
            // Panel contenedor
            Panel tarjeta = new Panel();
            tarjeta.Width = 250;
            tarjeta.Height = 140;
            tarjeta.Margin = new Padding(10);
            tarjeta.BackColor = Color.FromArgb(20, 25, 60);
            tarjeta.BorderStyle = BorderStyle.FixedSingle;
            tarjeta.Padding = new Padding(5);

            // Etiqueta con el nombre
            Label lbl = new Label
            {
                Text = nombre,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            tarjeta.Controls.Add(lbl);

            // Botón Abrir
            Button btnAbrir = new Button
            {
                Text = "Abrir",
                Dock = DockStyle.Bottom,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 60, 120),
                ForeColor = Color.White
            };
            btnAbrir.Click += (s, e) =>
            {
                AlumnosMateria alumnosMateria = new AlumnosMateria(idMateria, nombre);
                alumnosMateria.Show();

            };
            tarjeta.Controls.Add(btnAbrir);
            // Botón Editar
            Button btnEditar = new Button
            {
                Text = "Editar",
                Dock = DockStyle.Bottom,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White
            };
            btnEditar.Click += (s, e) => EditarMateria(idMateria, nombre);
            tarjeta.Controls.Add(btnEditar);

            // Botón Borrar
            Button btnBorrar = new Button
            {
                Text = "Borrar",
                Dock = DockStyle.Bottom,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.IndianRed,
                ForeColor = Color.White
            };
            btnBorrar.Click += (s, e) => BorrarMateria(idMateria);
            tarjeta.Controls.Add(btnBorrar);

            // Estadísticas: promedio materia y total materias del usuario
            Button btnStats = new Button
            {
                Text = "Estadísticas",
                Dock = DockStyle.Bottom,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White
            };
            btnStats.Click += (s, e) =>
            {//Aquí uso la conexion del archivo conexion.cs, ya que ahí tengo los métodos para ejecutar funciones y procedimientos, y separar las cosas de la BD.
                try
                {
                    ConexionBD.Conectar();
                    decimal prom = ConexionBD.EjecutarFuncionDecimal("SELECT funPromedioMateria(@p_idMateria)", ("@p_idMateria", idMateria));
                    int total = ConexionBD.EjecutarFuncionInt("SELECT funTotalMateriasUsuario(@p_idUsuario)", ("@p_idUsuario", idUsuarioActual));
                    MessageBox.Show($"Promedio materia: {prom:F2}\nTotal materias (usuario): {total}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener estadísticas: " + ex.Message);
                }
            };
            tarjeta.Controls.Add(btnStats);

            // Agregar a la lista de tarjetas
            listaTarjetas.Add(tarjeta);
            // Agregar tarjetas al FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(tarjeta);
        }
        private int contador = 1;

        private void EditarMateria(int idMateria, string nombreActual)
        {
            int cantidadActual = 0;
            string queryCantidad = "SELECT Cantidad_Alumnos FROM Materias WHERE id_Materia=@id";
            using (MySqlCommand cmd = new MySqlCommand(queryCantidad, conexion))
            {
                cmd.Parameters.AddWithValue("@id", idMateria);
                object result = cmd.ExecuteScalar();
                if (result != null) cantidadActual = Convert.ToInt32(result);
            }

            using (var formEditar = new NuevaMateria(nombreActual, cantidadActual))
            {
                var resultado = formEditar.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    string nuevoNombre = formEditar.NombreMateria;
                    int nuevaCantidad = formEditar.CantidadAlumnos;

                    string queryUpdate = "UPDATE Materias SET Nombre_Materia=@nombre, Cantidad_Alumnos=@cantidad WHERE id_Materia=@id";
                    using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@cantidad", nuevaCantidad);
                        cmd.Parameters.AddWithValue("@id", idMateria);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Materia editada correctamente.");
                    CargarMaterias();
                }
            }
        }
        private void BorrarMateria(int idMateria)
        {
            var confirm = MessageBox.Show("¿Seguro que quieres borrar esta materia?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                string query = "DELETE FROM Materias WHERE id_Materia=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idMateria);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Materia eliminada correctamente.");
                CargarMaterias();
            }
        }

        private void agregar_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.BackColor= Color.LightBlue;
            flowLayoutPanel1.Controls.Clear();
            listaTarjetas.Clear();

            using (var nuevaMateria = new NuevaMateria())
            {
                var resultado = nuevaMateria.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    string nombreMateria = nuevaMateria.NombreMateria;
                    int cantidadAlumnos = nuevaMateria.CantidadAlumnos;

                    int idUsuario = idUsuarioActual == 0 ? 1 : idUsuarioActual;

                    string insertQuery = "INSERT INTO Materias (Nombre_Materia, Cantidad_Alumnos, id_Usuario) VALUES (@nombre, @cantidad, @idUsuario)";
                    try
                    {
                        // Llamada al procedimiento proAgregarMateria (cantidad inicial 0; trigger o SP puede ajustarla si se desea)
                        ConexionBD.EjecutarProcedimiento("proAgregarMateria",
                            ("p_nombre", nombreMateria),
                            ("p_idUsuario", idUsuario)
                        );

                        MessageBox.Show("Materia creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Recaraga las materias
                        CargarMaterias();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al crear materia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Usuario canceló, no hace nada
                    CargarMaterias();
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DesconectarBD();
            Application.Exit();
        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void lblCorreo_Click(object sender, EventArgs e)
        {

        }
        private int idUsuarioActual;

        private void CargarDatosUsuario()
        {
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                    ConectarBD();

                string query = "SELECT Nombre, Correo FROM Usuarios WHERE id_Usuario = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuarioActual);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblNombre.Text = reader.GetString("Nombre");
                            lblCorreo.Text = reader.GetString("Correo");
                        }
                        else
                        {
                            lblNombre.Text = "Desconocido";
                            lblCorreo.Text = "-";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del usuario: " + ex.Message);
            }
        }

    }
}
