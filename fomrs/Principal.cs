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
            ConectarBD();


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
            label1.Text = "Alumnos";
            agregar.Visible = false;

            flowLayoutPanel1.Controls.Clear();

            // Panel contenedor principal
            Panel contenedor = new Panel();
            contenedor.Width = flowLayoutPanel1.Width - 20;
            contenedor.Height = flowLayoutPanel1.Height - 20;
            contenedor.Margin = new Padding(10);
            contenedor.BackColor = Color.Transparent;

            // DataGridView
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                EnableHeadersVisualStyles = false
            };

            contenedor.Controls.Add(dgv);
            flowLayoutPanel1.Controls.Add(contenedor);

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    conexion = new MySqlConnection(
                        $"Server={mySqlServer};Database={mySqlDatabase};Uid={mySqlUserId};Pwd={mySqlPassword};"
                    );
                    conexion.Open();
                }

                // Consulta AGRUPADA (un alumno, muchas materias)
                string query = @"
            SELECT
                a.id_Alumno,
                CONCAT(a.Nombre, ' ', a.Apellido) AS Alumno,
                IFNULL(GROUP_CONCAT(m.Nombre_Materia SEPARATOR ', '), 'Sin materias') AS Materias
            FROM Alumnos a
            LEFT JOIN Alumno_Materia am ON a.id_Alumno = am.id_Alumno
            LEFT JOIN Materias m ON am.id_Materia = m.id_Materia
            GROUP BY a.id_Alumno, Alumno;
        ";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }

                // Ocultar ID Alumno
                if (dgv.Columns.Contains("id_Alumno"))
                    dgv.Columns["id_Alumno"].Visible = false;

                // Botón Ver Boleta
                if (!dgv.Columns.Contains("Boleta"))
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "Boleta";
                    btn.HeaderText = "";
                    btn.Text = "Ver boleta";
                    btn.UseColumnTextForButtonValue = true;
                    dgv.Columns.Add(btn);
                }

                dgv.CellClick -= Dgv_CellClick_Boleta;  // evitar duplicación de eventos
                dgv.CellClick += Dgv_CellClick_Boleta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message);
            }
        }
        private void Dgv_CellClick_Boleta(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;

            if (e.RowIndex >= 0 && e.ColumnIndex == dgv.Columns["Boleta"].Index)
            {
                int idAlumno = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id_Alumno"].Value);
                string nombreAlumno = dgv.Rows[e.RowIndex].Cells["Alumno"].Value.ToString();

                // Abrir boleta de TODAS las materias del alumno
                FichaAlumno ficha = new FichaAlumno(idAlumno, nombreAlumno, conexion);
                ficha.ShowDialog();
            }
        }


        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (e.ColumnIndex == dgv.Columns["Detalle"].Index && e.RowIndex >= 0)
            {
                int idAlumno = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id_Alumno"].Value);
                string nombre = dgv.Rows[e.RowIndex].Cells["Alumno"].Value.ToString();

                FichaAlumno ficha = new FichaAlumno(idAlumno, nombre, conexion);
                ficha.ShowDialog();
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
            // Panel de la tarjeta
            Panel tarjeta = new Panel();
            tarjeta.Width = 250;
            tarjeta.Height = 120;
            tarjeta.Margin = new Padding(15);
            tarjeta.BackColor = Color.FromArgb(20, 25, 60);
            tarjeta.BorderStyle = BorderStyle.FixedSingle;
            tarjeta.Padding = new Padding(5);


           
            Button btnEliminar = new Button();
            btnEliminar.Size = new Size(28, 28);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.BackColor = Color.Transparent;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Text = "🗑";   // Ícono Unicode
            btnEliminar.Font = new Font("Segoe UI Emoji", 12, FontStyle.Regular);
            btnEliminar.Cursor = Cursors.Hand;

            // Ubicamos arriba a la derecha
            btnEliminar.Location = new Point(tarjeta.Width - 35, 5);

            // Evento eliminar
            btnEliminar.Click += (s, e) =>
            {
                DialogResult confirm = MessageBox.Show(
                    $"¿Eliminar la materia \"{nombre}\"?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        if (conexion.State != ConnectionState.Open)
                            ConectarBD();

                        string deleteQuery = "DELETE FROM Materias WHERE id_Materia = @id";
                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id", idMateria);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Materia eliminada correctamente.", "Eliminado",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarMaterias();
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("Error al eliminar: " + ex2.Message);
                    }
                }
            };


            // ----------- TÍTULO DE LA MATERIA -------------
            Label lbl = new Label();
            lbl.Text = nombre;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lbl.Dock = DockStyle.Top;
            lbl.Height = 40;
            lbl.TextAlign = ContentAlignment.MiddleCenter;


            // ----------- BOTÓN ABRIR -------------
            Button btnAbrir = new Button();
            btnAbrir.Text = "Abrir";
            btnAbrir.Height = 35;
            btnAbrir.Dock = DockStyle.Bottom;
            btnAbrir.FlatStyle = FlatStyle.Flat;
            btnAbrir.FlatAppearance.BorderSize = 0;
            btnAbrir.ForeColor = Color.White;
            btnAbrir.BackColor = Color.FromArgb(45, 65, 130);
            btnAbrir.Cursor = Cursors.Hand;

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


            // Agregar controles en orden
            tarjeta.Controls.Add(btnEliminar); // que quede arriba
            tarjeta.Controls.Add(lbl);
            tarjeta.Controls.Add(btnAbrir);

            listaTarjetas.Add(tarjeta);
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
                    int cunidades = nuevaMateria.CantidadAlumnos;

                    int idUsuario = 1;

                    string insertQuery = "INSERT INTO Materias (Nombre_Materia, Unidades, id_Usuario) VALUES (@nombre, @unidades, @idUsuario)";
                    try
                    {
                        if (conexion == null || conexion.State != ConnectionState.Open)
                            ConectarBD();

                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, conexion))
                        {
                            cmd.Parameters.AddWithValue("@nombre", nombreMateria);
                            cmd.Parameters.AddWithValue("@unidades", cunidades);
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

                // Agregar tarjetas una sola vez
                foreach (var t in listaTarjetas)
                    flowLayoutPanel1.Controls.Add(t);

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
            CargarMaterias();
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
