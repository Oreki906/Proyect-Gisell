using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Login.fomrs
{
    public partial class AlumnosMateria : Form
    {
        private int idMateria;
        private string nombreMateria;

        public AlumnosMateria(int idMateria, string nombre)
        {
            InitializeComponent();
            this.idMateria = idMateria;
            this.nombreMateria = nombre;
            AbrirMateriaEnPanel(idMateria, nombre);
        }

        private void AbrirMateriaEnPanel(int idMateria, string nombreMateria)
        {
            flowLayoutPanel1.Controls.Clear();

            // Título
            Label lblMateria = new Label
            {
                Text = "Materia: " + nombreMateria,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            flowLayoutPanel1.Controls.Add(lblMateria);

            // DataGridView
            DataGridView dgvAlumnos = new DataGridView
            {
                Size = new Size(flowLayoutPanel1.Width - 20, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            flowLayoutPanel1.Controls.Add(dgvAlumnos);

            // Cargar alumnos
            CargarAlumnosMateria(idMateria, dgvAlumnos);

            // Botones
            Button btnAgregarAlumno = new Button
            {
                Text = "Agregar Alumno",
                Width = 150,
                Height = 35,
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAgregarAlumno.Click += (s, e) => AgregarAlumnoEnPanel(idMateria, dgvAlumnos);
            flowLayoutPanel1.Controls.Add(btnAgregarAlumno);

            Button btnRegistrarCal = new Button
            {
                Text = "Registrar Calificación",
                Width = 170,
                Height = 35,
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRegistrarCal.Click += (s, e) => RegistrarCalificacion(dgvAlumnos);
            flowLayoutPanel1.Controls.Add(btnRegistrarCal);

            Button btnActualizarCal = new Button
            {
                Text = "Actualizar Calificación",
                Width = 170,
                Height = 35,
                BackColor = Color.CadetBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnActualizarCal.Click += (s, e) => ActualizarCalificacion(dgvAlumnos);
            flowLayoutPanel1.Controls.Add(btnActualizarCal);

            Button btnPromedio = new Button
            {
                Text = "Promedio Alumno",
                Width = 150,
                Height = 35,
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPromedio.Click += (s, e) => MostrarPromedioAlumno(dgvAlumnos);
            flowLayoutPanel1.Controls.Add(btnPromedio);

            Button btnPorcAsis = new Button
            {
                Text = "Porcentaje Asistencia",
                Width = 180,
                Height = 35,
                BackColor = Color.DarkOliveGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPorcAsis.Click += (s, e) => MostrarPorcentajeAsistencia(dgvAlumnos);
            flowLayoutPanel1.Controls.Add(btnPorcAsis);

            Button btnEliminarAlumno = new Button
            {
                Text = "Eliminar Alumno",
                Width = 150,
                Height = 35,
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnEliminarAlumno.Click += (s, e) => EliminarAlumnoSeleccionado(dgvAlumnos);
            flowLayoutPanel1.Controls.Add(btnEliminarAlumno);
        }

        private void CargarAlumnosMateria(int idMateria, DataGridView dgv)
        {
            try
            {
                ConexionBD.Conectar();
                string query = @"
                SELECT a.id_Alumno, a.Nombre, a.Apellido, a.Matricula, a.Grado, IFNULL(a.Promedio,0) AS Promedio
                FROM Alumnos a
                JOIN Alumno_Materia am ON a.id_Alumno = am.id_Alumno
                WHERE am.id_Materia = @idMateria";

                using (MySqlCommand cmd = new MySqlCommand(query, ConexionBD.conexion))
                {
                    cmd.Parameters.AddWithValue("@idMateria", idMateria);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message);
            }
        }

        private void AgregarAlumnoEnPanel(int idMateria, DataGridView dgv)
        {
            Form formAlumno = new Form
            {
                Size = new Size(320, 280),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Nuevo Alumno"
            };

            Label lblNombre = new Label { Text = "Nombre:", Location = new Point(10, 20), AutoSize = true };
            TextBox txtNombre = new TextBox { Location = new Point(120, 18), Width = 170 };

            Label lblApellido = new Label { Text = "Apellido:", Location = new Point(10, 60), AutoSize = true };
            TextBox txtApellido = new TextBox { Location = new Point(120, 58), Width = 170 };

            Label lblMatricula = new Label { Text = "Matrícula:", Location = new Point(10, 100), AutoSize = true };
            NumericUpDown numMatricula = new NumericUpDown { Location = new Point(120, 98), Width = 170, Maximum = 100000 };

            Label lblGrado = new Label { Text = "Grado:", Location = new Point(10, 140), AutoSize = true };
            NumericUpDown numGrado = new NumericUpDown { Location = new Point(120, 138), Width = 170, Minimum = 1, Maximum = 12 };

            Button btnAceptar = new Button { Text = "Aceptar", Location = new Point(60, 190), DialogResult = DialogResult.OK };
            Button btnCancelar = new Button { Text = "Cancelar", Location = new Point(180, 190), DialogResult = DialogResult.Cancel };

            formAlumno.Controls.AddRange(new Control[] { lblNombre, txtNombre, lblApellido, txtApellido,
                lblMatricula, numMatricula, lblGrado, numGrado, btnAceptar, btnCancelar });

            if (formAlumno.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ConexionBD.Conectar();

                    // Llamada al procedimiento proAgregarAlumno
                    long idAlumno = ConexionBD.EjecutarProcedimiento("proAgregarAlumno", cmd =>
                    {
                        cmd.Parameters.AddWithValue("@p_nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@p_apellido", txtApellido.Text.Trim());
                        cmd.Parameters.AddWithValue("@p_matricula", (int)numMatricula.Value);
                        cmd.Parameters.AddWithValue("@p_grado", (int)numGrado.Value);
                    });

                    // Si LastInsertedId no devolvió el id, intenta buscar por matrícula como respaldo
                    if (idAlumno == 0)
                    {
                        object o = ConexionBD.EjecutarScalar("SELECT id_Alumno FROM Alumnos WHERE Matricula = @mat LIMIT 1",
                            ("@mat", (int)numMatricula.Value));
                        if (o != null && o != DBNull.Value)
                            idAlumno = Convert.ToInt64(o);
                    }

                    if (idAlumno == 0)
                        throw new Exception("No se pudo obtener el id del alumno insertado.");

                    // Relacionar con materia
                    ConexionBD.EjecutarNonQuery("INSERT INTO Alumno_Materia (id_Alumno, id_Materia) VALUES (@idAlumno, @idMateria)",
                        ("@idAlumno", idAlumno), ("@idMateria", idMateria));

                    // Trigger, aumentará Cantidad_Alumnos automáticamente
                    CargarAlumnosMateria(idMateria, dgv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar alumno: " + ex.Message);
                }
            }
        }

        // Registrar calificación (proRegistrarCalificacion)
        private void RegistrarCalificacion(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un alumno.");
                return;
            }

            int idAlumno = Convert.ToInt32(dgv.SelectedRows[0].Cells["id_Alumno"].Value);

            Form f = new Form
            {
                Size = new Size(300, 170),
                StartPosition = FormStartPosition.CenterParent,
                Text = "Registrar Calificación",
                FormBorderStyle = FormBorderStyle.FixedDialog
            };
            NumericUpDown numCal = new NumericUpDown { Minimum = 0, Maximum = 100, Location = new Point(20, 20), Width = 240 };
            Button btnOk = new Button { Text = "Aceptar", Location = new Point(40, 70), DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Cancelar", Location = new Point(150, 70), DialogResult = DialogResult.Cancel };
            f.Controls.AddRange(new Control[] { numCal, btnOk, btnCancel });

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ConexionBD.EjecutarProcedimiento("proRegistrarCalificacion",
                        ("p_idAlumno", idAlumno),
                        ("p_idMateria", idMateria),
                        ("p_calificacion", (int)numCal.Value)
                    );

                    MessageBox.Show("Calificación registrada.");
                    CargarAlumnosMateria(idMateria, dgv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar calificación: " + ex.Message);
                }
            }
        }

        // Actualizar calificación (proActualizarCalificacion)
        private void ActualizarCalificacion(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un alumno primero.");
                return;
            }

            int idAlumno = Convert.ToInt32(dgv.SelectedRows[0].Cells["id_Alumno"].Value);

            // Carga la calificación existente 
            using (MySqlCommand cmdCheck = new MySqlCommand("SELECT id_Calificacion, Calificacion FROM Calificacion WHERE id_Alumno=@id AND id_Materia=@m LIMIT 1", ConexionBD.conexion))
            {
                cmdCheck.Parameters.AddWithValue("@id", idAlumno);
                cmdCheck.Parameters.AddWithValue("@m", idMateria);
                using (var r = cmdCheck.ExecuteReader())
                {
                    if (!r.Read())
                    {
                        MessageBox.Show("No existe una calificación previa para este alumno en esta materia.");
                        return;
                    }
                }
            }

            int idCal = 0;
            int calActual = 0;
            using (MySqlCommand cmd = new MySqlCommand("SELECT id_Calificacion, Calificacion FROM Calificacion WHERE id_Alumno=@id AND id_Materia=@m LIMIT 1", ConexionBD.conexion))
            {
                cmd.Parameters.AddWithValue("@id", idAlumno);
                cmd.Parameters.AddWithValue("@m", idMateria);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        idCal = r.GetInt32("id_Calificacion");
                        calActual = r.GetInt32("Calificacion");
                    }
                }
            }

            Form f = new Form
            {
                Size = new Size(300, 170),
                StartPosition = FormStartPosition.CenterParent,
                Text = "Actualizar Calificación",
                FormBorderStyle = FormBorderStyle.FixedDialog
            };
            NumericUpDown numCal = new NumericUpDown { Minimum = 0, Maximum = 100, Location = new Point(20, 20), Width = 240, Value = calActual };
            Button btnOk = new Button { Text = "Aceptar", Location = new Point(40, 70), DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Cancelar", Location = new Point(150, 70), DialogResult = DialogResult.Cancel };
            f.Controls.AddRange(new Control[] { numCal, btnOk, btnCancel });

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ConexionBD.EjecutarProcedimiento("proActualizarCalificacion",
                        ("p_idCalificacion", idCal),
                        ("p_nuevaCalificacion", (int)numCal.Value)
                    );

                    // Recalcula el promedio del alumno.
                    decimal nuevoProm = ConexionBD.EjecutarFuncionDecimal("SELECT funPromedioAlumno(@p_idAlumno)", ("@p_idAlumno", idAlumno));
                    ConexionBD.EjecutarNonQuery("UPDATE Alumnos SET Promedio=@p WHERE id_Alumno=@id", ("@p", nuevoProm), ("@id", idAlumno));

                    // Recarga la vista
                    CargarAlumnosMateria(idMateria, dgv);

                    MessageBox.Show("Calificación actualizada.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar calificación: " + ex.Message);
                }
            }
        }

        // Mostrar promedio del alumno (funPromedioAlumno)
        private void MostrarPromedioAlumno(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un alumno primero.");
                return;
            }

            int idAlumno = Convert.ToInt32(dgv.SelectedRows[0].Cells["id_Alumno"].Value);

            try
            {
                decimal promedio = ConexionBD.EjecutarFuncionDecimal("SELECT funPromedioAlumno(@p_idAlumno)",
                    ("@p_idAlumno", idAlumno)
                );

                MessageBox.Show($"Promedio del alumno: {promedio:F2}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener promedio: " + ex.Message);
            }
        }

        // Mostrar porcentaje de asistencia (funPorcentajeAsistencia)
        private void MostrarPorcentajeAsistencia(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un alumno primero.");
                return;
            }

            int idAlumno = Convert.ToInt32(dgv.SelectedRows[0].Cells["id_Alumno"].Value);

            try
            {
                decimal porcentaje = ConexionBD.EjecutarFuncionDecimal("SELECT funPorcentajeAsistencia(@p_idAlumno)",
                    ("@p_idAlumno", idAlumno)
                );

                MessageBox.Show($"Porcentaje de asistencia: {porcentaje:F2}%");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener porcentaje de asistencia: " + ex.Message);
            }
        }

        // Eliminar alumno seleccionado (desvincula primero de materia, luego elimina alumno)
        private void EliminarAlumnoSeleccionado(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un alumno.");
                return;
            }

            int idAlumno = Convert.ToInt32(dgv.SelectedRows[0].Cells["id_Alumno"].Value);

            var conf = MessageBox.Show("¿Seguro que quieres eliminar este alumno? (Se quitará de la materia y se borrará el registro del alumno)", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (conf != DialogResult.Yes) return;

            try
            {
                // Borrar relación (esto dispara el trigger trigAlumnoMateriaDelete)
                ConexionBD.EjecutarNonQuery("DELETE FROM Alumno_Materia WHERE id_Alumno=@id AND id_Materia=@m",
                    ("@id", idAlumno), ("@m", idMateria));

                MessageBox.Show("Alumno eliminado.");
                CargarAlumnosMateria(idMateria, dgv);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar alumno: " + ex.Message);
            }
        }
    }
}
