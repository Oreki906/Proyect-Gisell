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
    }
}
