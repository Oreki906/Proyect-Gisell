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
    public partial class Planeacion : Form
    {
        private int idMateria;
        private string nombreMateria;
        private MySqlConnection conexion;
        
        private int? idPlaneacion = null;
        public Planeacion(int idMateria, string nombreMateria, MySqlConnection conexion, int? idPlaneacion = null)
        {
            InitializeComponent();
            this.idMateria = idMateria;
            this.nombreMateria = nombreMateria;
            this.conexion = conexion;
            this.idPlaneacion = idPlaneacion;

        }

        private void ritface_TextChanged(object sender, EventArgs e)
        {


        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conexion == null || conexion.State != System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("No hay conexión con la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //  Inserta una nueva planeación si idPlaneacion es null
                if (idPlaneacion == null)
                {
                    string query = @"INSERT INTO Planeaciones
                (Fecha_Creacion, Fecha_Entrega, id_Usuario, id_Materia, 
                 Titulo_Proyecto, Campo_Formativo, Contenido, Proceso_Aprendizaje,
                 Metodologia, Momento, Evaluacion, Materiales, Actividades,
                 Face, fecha_aplicacion, tiempo, eje, escenario, producto)
                VALUES
                (@creacion, @entrega, @usuario, @materia, @titulo, @campo, @contenido,
                 @proceso, @metodo, @momento, @eval, @mat, @actividades,
                 @face, @fecha_ap, @tiempo, @eje, @escenario, @producto)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@creacion", DateTime.Now);
                        cmd.Parameters.AddWithValue("@entrega", DateTime.Now);
                        cmd.Parameters.AddWithValue("@usuario", 1); // 🔸 Reemplaza con tu variable de usuario logueado
                        cmd.Parameters.AddWithValue("@materia", idMateria);
                        cmd.Parameters.AddWithValue("@titulo", titulo.Text);
                        cmd.Parameters.AddWithValue("@campo", campo.Text);
                        cmd.Parameters.AddWithValue("@contenido", contenido.Text);
                        cmd.Parameters.AddWithValue("@proceso", pda.Text);
                        cmd.Parameters.AddWithValue("@metodo", metodologia.Text);
                        cmd.Parameters.AddWithValue("@momento", momento.Text);
                        cmd.Parameters.AddWithValue("@eval", evaluacion.Text);
                        cmd.Parameters.AddWithValue("@mat", materiales.Text);
                        cmd.Parameters.AddWithValue("@actividades", actividades.Text);
                        cmd.Parameters.AddWithValue("@face", face.Text);
                        cmd.Parameters.AddWithValue("@fecha_ap", fecha.Text);
                        cmd.Parameters.AddWithValue("@tiempo", tiempo.Text);
                        cmd.Parameters.AddWithValue("@eje", ejes.Text);
                        cmd.Parameters.AddWithValue("@escenario", ecenario.Text);
                        cmd.Parameters.AddWithValue("@producto", producto.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(" Planeación registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //  Si existe, actualiza la planeación
                    string query = @"UPDATE Planeaciones SET
                Fecha_Entrega = @entrega,
                Titulo_Proyecto = @titulo,
                Campo_Formativo = @campo,
                Contenido = @contenido,
                Proceso_Aprendizaje = @proceso,
                Metodologia = @metodo,
                Momento = @momento,
                Evaluacion = @eval,
                Materiales = @mat,
                Actividades = @actividades,
                Face = @face,
                fecha_aplicacion = @fecha_ap,
                tiempo = @tiempo,
                eje = @eje,
                escenario = @escenario,
                producto = @producto
                WHERE id_Planeacion = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        //.Parameters.AddWithValue("@entrega", fecha.Text);
                        cmd.Parameters.AddWithValue("@titulo", titulo.Text);
                        cmd.Parameters.AddWithValue("@campo", campo.Text);
                        cmd.Parameters.AddWithValue("@contenido", contenido.Text);
                        cmd.Parameters.AddWithValue("@proceso",pda.Text);
                        cmd.Parameters.AddWithValue("@metodo", metodologia.Text);
                        cmd.Parameters.AddWithValue("@momento", momento.Text);
                        cmd.Parameters.AddWithValue("@eval", evaluacion.Text);
                        cmd.Parameters.AddWithValue("@mat", materiales.Text);
                        cmd.Parameters.AddWithValue("@actividades", actividades.Text);
                        cmd.Parameters.AddWithValue("@face", face.Text);
                        cmd.Parameters.AddWithValue("@fecha_ap",fecha.Text);
                        cmd.Parameters.AddWithValue("@tiempo", tiempo.Text);
                        cmd.Parameters.AddWithValue("@eje", ejes.Text);
                        cmd.Parameters.AddWithValue("@escenario", ecenario.Text);
                        cmd.Parameters.AddWithValue("@producto", producto.Text);
                        cmd.Parameters.AddWithValue("@id", idPlaneacion);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("✏️ Planeación actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close(); // Cierra el formulario tras guardar
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar la planeación:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Planeacion_Load(object sender, EventArgs e)
        {
            if (idPlaneacion.HasValue)
            {
                CargarPlaneacion(idPlaneacion.Value);
            }
        }
        private void CargarPlaneacion(int id)
        {
            string query = "SELECT * FROM Planeaciones WHERE id_Planeacion = @id";
            using (MySqlCommand cmd = new MySqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        titulo.Text = reader["Titulo_Proyecto"].ToString();
                        campo.Text = reader["Campo_Formativo"].ToString();
                        contenido.Text = reader["Contenido"].ToString();
                        pda.Text = reader["Proceso_Aprendizaje"].ToString();
                        metodologia.Text = reader["Metodologia"].ToString();
                        momento.Text = reader["Momento"].ToString();
                        actividades.Text = reader["Actividades"].ToString();
                        evaluacion.Text = reader["Evaluacion"].ToString();
                        materiales.Text = reader["Materiales"].ToString();
                        fecha.Text = reader["Fecha_Entrega"].ToString();
                        tiempo.Text = reader["tiempo"].ToString();
                        ejes.Text = reader["eje"].ToString();
                        ecenario.Text = reader["escenario"].ToString();
                        producto.Text = reader["producto"].ToString();
                    }
                }
            }
        }
    }
}
