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
    public partial class listaplaneaciones : Form
    {

        private int idMateria;
        private string nombreMateria;
        private MySqlConnection conexion;
        public listaplaneaciones(int idMateria, string nombreMateria, MySqlConnection conexion)
        {
            InitializeComponent();
            this.idMateria = idMateria;
            this.nombreMateria = nombreMateria;
            this.conexion = conexion;
            this.WindowState = FormWindowState.Maximized;
        }

        private void listaplaneaciones_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = $"Planeaciones de {nombreMateria}";
            CargarPlaneaciones();
            CargarCalificacionesPorUnidades();
           


        }

        private void CargarPlaneaciones()
        {
            try
            {
                string query = @"SELECT id_Planeacion, Titulo_Proyecto, Fecha_Entrega 
                                 FROM Planeaciones WHERE id_Materia = @materia";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@materia", idMateria);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvPlaneaciones.DataSource = dt;
                    }
                }

                dgvPlaneaciones.Columns["id_Planeacion"].Visible = false;
                dgvPlaneaciones.Columns["Titulo_Proyecto"].HeaderText = "Título";
                dgvPlaneaciones.Columns["Fecha_Entrega"].HeaderText = "Fecha de Aplicación";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar planeaciones: " + ex.Message);
            }
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            Planeacion nueva = new Planeacion(idMateria, nombreMateria, conexion);
            nueva.ShowDialog();
            CargarPlaneaciones();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (dgvPlaneaciones.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una planeación para ver.");
                return;
            }

            int idPlaneacion = Convert.ToInt32(dgvPlaneaciones.CurrentRow.Cells["id_Planeacion"].Value);

            Planeacion ver = new Planeacion(idMateria, nombreMateria, conexion, idPlaneacion);
            ver.ShowDialog();
            CargarPlaneaciones();
        }

        private void btnbtnExporatrPDF_Click(object sender, EventArgs e)
        {
            if (dgvPlaneaciones.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una planeación para exportar.");
                return;
            }

            int idPlaneacion = Convert.ToInt32(dgvPlaneaciones.CurrentRow.Cells["id_Planeacion"].Value);

            PlaneacionModelo p = ObtenerPlaneacionDesdeBD(idPlaneacion);
           // ExportarPDF(p);//ya mero creo el codigo
        }

        private PlaneacionModelo ObtenerPlaneacionDesdeBD(int id)
        {
            PlaneacionModelo p = new PlaneacionModelo();
            string query = "SELECT * FROM Planeaciones WHERE id_Planeacion = @id";
            using (MySqlCommand cmd = new MySqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p.Id = id;
                        p.TituloProyecto = reader["Titulo_Proyecto"].ToString();
                        p.CampoFormativo = reader["Campo_Formativo"].ToString();
                        p.Contenido = reader["Contenido"].ToString();
                        p.ProcesoAprendizaje = reader["Proceso_Aprendizaje"].ToString();
                        p.Metodologia = reader["Metodologia"].ToString();
                        p.Momento = reader["Momento"].ToString();
                        p.ActividadesInicio = reader["Actividades_Inicio"].ToString();
                        p.Evaluacion = reader["Evaluacion"].ToString();
                        p.Materiales = reader["Materiales"].ToString();
                        p.FechaPractica = Convert.ToDateTime(reader["Fecha_Entrega"]);
                    }
                }
            }
            return p;
        }
        private void CargarCalificacionesPorUnidades()
        {
            try
            {
                // 1. Obtener número de unidades de la materia
                int unidades = 1;
                string queryUnidades = "SELECT Unidades FROM Materias WHERE id_Materia = @materia";

                using (MySqlCommand cmd = new MySqlCommand(queryUnidades, conexion))
                {
                    cmd.Parameters.AddWithValue("@materia", idMateria);
                    unidades = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2. Cargar lista de alumnos
                string queryAlumnos = @"SELECT a.id_Alumno, CONCAT(a.Nombre, ' ', a.Apellido) AS Alumno
                                FROM Alumno_Materia am
                                JOIN Alumnos a ON am.id_Alumno = a.id_Alumno
                                WHERE am.id_Materia = @materia";

                DataTable dt = new DataTable();
                dt.Columns.Add("id_Alumno", typeof(int));
                dt.Columns.Add("Alumno", typeof(string));

                using (MySqlCommand cmd = new MySqlCommand(queryAlumnos, conexion))
                {
                    cmd.Parameters.AddWithValue("@materia", idMateria);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dt.Rows.Add(reader.GetInt32("id_Alumno"), reader.GetString("Alumno"));
                        }
                    }
                }

                // 3. Agregar columnas dinámicas por unidad
                for (int u = 1; u <= unidades; u++)
                {
                    dt.Columns.Add($"Unidad {u}", typeof(decimal));
                }

                // 4. Cargar calificaciones ya existentes
                string queryCalif = @"SELECT id_Alumno, Unidad, Calificacion
                              FROM UnidadCalificacion
                              WHERE id_Materia = @materia";

                using (MySqlCommand cmd = new MySqlCommand(queryCalif, conexion))
                {
                    cmd.Parameters.AddWithValue("@materia", idMateria);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idAl = reader.GetInt32("id_Alumno");
                            int unidad = reader.GetInt32("Unidad");
                            decimal calif = reader.IsDBNull(reader.GetOrdinal("Calificacion"))
                                                ? 0 : reader.GetDecimal("Calificacion");

                            // buscar la fila del alumno y poner la calificación
                            foreach (DataRow row in dt.Rows)
                            {
                                if ((int)row["id_Alumno"] == idAl)
                                {
                                    row[$"Unidad {unidad}"] = calif;
                                }
                            }
                        }
                    }
                }

                // 5. Mostrar tabla
                dgvUnidades.DataSource = dt;
                if (!dgvUnidades.Columns.Contains("Eliminar"))
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "Eliminar";
                    btn.HeaderText = "";
                    btn.Text = "🗑";
                    btn.UseColumnTextForButtonValue = true;
                    btn.Width = 40;

                    dgvUnidades.Columns.Add(btn);
                }

                dgvUnidades.Columns["id_Alumno"].Visible = false;
                dgvUnidades.Columns["Alumno"].ReadOnly = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar calificaciones: " + ex.Message);
            }

            
            dgvUnidades.BorderStyle = BorderStyle.None;
            dgvUnidades.BackgroundColor = Color.FromArgb(240, 240, 240);
            dgvUnidades.EnableHeadersVisualStyles = false;

            // Encabezado moderno
            dgvUnidades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 42, 86);
            dgvUnidades.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUnidades.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvUnidades.ColumnHeadersHeight = 40;

            // Celdas
            dgvUnidades.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvUnidades.DefaultCellStyle.BackColor = Color.White;
            dgvUnidades.DefaultCellStyle.ForeColor = Color.Black;
            dgvUnidades.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 85, 160);
            dgvUnidades.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUnidades.RowTemplate.Height = 35;

            // Filas alternadas
            dgvUnidades.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);

            // Bordes suaves
            dgvUnidades.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUnidades.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Hover visual
            dgvUnidades.MouseMove += (s, e) =>
            {
                var hit = dgvUnidades.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dgvUnidades.Rows[hit.RowIndex].DefaultCellStyle.BackColor =
                        Color.FromArgb(225, 232, 255);
                }
            };
        }

        private void dgvUnidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvUnidades.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                int idAlumno = Convert.ToInt32(dgvUnidades.Rows[e.RowIndex].Cells["id_Alumno"].Value);
                string nombreAlumno = dgvUnidades.Rows[e.RowIndex].Cells["Alumno"].Value.ToString();

                DialogResult r = MessageBox.Show(
                    $"¿Eliminar a \"{nombreAlumno}\" de esta materia?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (r == DialogResult.Yes)
                {
                    EliminarAlumnoDeMateria(idAlumno);
                }
            }
        }
        private void EliminarAlumnoDeMateria(int idAlumno)
        {
            try
            {
                // 1. Eliminar relación Alumno-Materia
                string sql = "DELETE FROM Alumno_Materia WHERE id_Alumno = @alumno AND id_Materia = @materia";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@alumno", idAlumno);
                    cmd.Parameters.AddWithValue("@materia", idMateria);
                    cmd.ExecuteNonQuery();
                }

                // 2. Eliminar calificaciones por unidades en esta materia
                string sql2 = "DELETE FROM UnidadCalificacion WHERE id_Alumno = @alumno AND id_Materia = @materia";

                using (MySqlCommand cmd2 = new MySqlCommand(sql2, conexion))
                {
                    cmd2.Parameters.AddWithValue("@alumno", idAlumno);
                    cmd2.Parameters.AddWithValue("@materia", idMateria);
                    cmd2.ExecuteNonQuery();
                }

                MessageBox.Show("Alumno eliminado correctamente.");
                CargarCalificacionesPorUnidades();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar alumno: " + ex.Message);
            }
        }

        private void btnGuardarUnidades_Click(object sender, EventArgs e)
        {
            try
            {
                if (conexion == null)
                {
                    MessageBox.Show("Error: la conexión no fue enviada correctamente al formulario.");
                    return;
                }

                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // Usamos transacción por seguridad
                using (MySqlTransaction tx = conexion.BeginTransaction())
                {
                    string sql = @"
                INSERT INTO UnidadCalificacion (id_Alumno, id_Materia, Unidad, Calificacion)
                VALUES (@alumno, @materia, @unidad, @calif)
                ON DUPLICATE KEY UPDATE Calificacion = @calif;
            ";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conexion, tx))
                    {
                        // Definimos parámetros UNA sola vez
                        cmd.Parameters.Add("@alumno", MySqlDbType.Int32);
                        cmd.Parameters.Add("@materia", MySqlDbType.Int32);
                        cmd.Parameters.Add("@unidad", MySqlDbType.Int32);
                        cmd.Parameters.Add("@calif", MySqlDbType.Decimal);

                        foreach (DataGridViewRow row in dgvUnidades.Rows)
                        {
                            // Saltar fila nueva / vacía
                            if (row.IsNewRow)
                                continue;

                            if (row.Cells["id_Alumno"].Value == null ||
                                row.Cells["id_Alumno"].Value.ToString() == "")
                                continue;

                            int idAlumno = Convert.ToInt32(row.Cells["id_Alumno"].Value);

                            // Recorremos SOLO columnas de unidades
                            foreach (DataGridViewColumn col in dgvUnidades.Columns)
                            {
                                if (!col.HeaderText.StartsWith("Unidad"))
                                    continue;

                                string valor = row.Cells[col.Index].Value?.ToString();
                                if (string.IsNullOrWhiteSpace(valor))
                                    valor = "0";

                                if (!decimal.TryParse(valor, out decimal calif))
                                    calif = 0;

                                int unidad = int.Parse(col.HeaderText.Replace("Unidad ", ""));

                                // Asignar valores a parámetros
                                cmd.Parameters["@alumno"].Value = idAlumno;
                                cmd.Parameters["@materia"].Value = idMateria;   // <- variable de tu form
                                cmd.Parameters["@unidad"].Value = unidad;
                                cmd.Parameters["@calif"].Value = calif;

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tx.Commit();
                }

                MessageBox.Show("Calificaciones guardadas correctamente.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar (MySQL): " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }


        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            var form = new AgregarAlumno(conexion, idMateria);

            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int idAlumno = 0;

                    // 1️⃣ Buscar si el alumno YA existe
                    string buscarAlumno = @"
                SELECT id_Alumno 
                FROM Alumnos 
                WHERE Nombre = @n AND Apellido = @a
                LIMIT 1;
            ";

                    using (MySqlCommand cmd = new MySqlCommand(buscarAlumno, conexion))
                    {
                        cmd.Parameters.AddWithValue("@n", form.NombreAlumno);
                        cmd.Parameters.AddWithValue("@a", form.ApellidoAlumno);

                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            idAlumno = Convert.ToInt32(result);   // alumno ya existe
                        }
                    }

                    // 2️⃣ Si NO existe → insertarlo
                    if (idAlumno == 0)
                    {
                        string insertAlumno = @"
                    INSERT INTO Alumnos (Nombre, Apellido)
                    VALUES (@n, @a);
                ";

                        using (MySqlCommand cmd = new MySqlCommand(insertAlumno, conexion))
                        {
                            cmd.Parameters.AddWithValue("@n", form.NombreAlumno);
                            cmd.Parameters.AddWithValue("@a", form.ApellidoAlumno);
                            cmd.ExecuteNonQuery();
                            idAlumno = (int)cmd.LastInsertedId;
                        }
                    }

                    // 3️⃣ Relacionarlo con la materia (evitar duplicados)
                    string registrar = @"
                INSERT IGNORE INTO Alumno_Materia (id_Alumno, id_Materia)
                VALUES (@al, @mat);
            ";

                    using (MySqlCommand cmd = new MySqlCommand(registrar, conexion))
                    {
                        cmd.Parameters.AddWithValue("@al", idAlumno);
                        cmd.Parameters.AddWithValue("@mat", idMateria);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Alumno agregado y vinculado correctamente.");

                    // 4️⃣ Recargar tabla
                    CargarCalificacionesPorUnidades();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar alumno: " + ex.Message);
                }
            }
        }

    }
}

