using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Login.fomrs
{
    public partial class Agenda : Form
    {
        private MySqlConnection conexion;
        private DateTime semanaActual;
        private Form ventanaPrincipal;

        public Agenda(Form ventanaPrincipal, MySqlConnection cnx)
        {
            InitializeComponent();
            conexion = cnx;
            this.ventanaPrincipal = ventanaPrincipal;

            semanaActual = ObtenerLunes(DateTime.Today);
            InicializarTabla();
            CargarSemana();
            ActualizarTituloSemana();

          //  tblAgenda.BackColor = Color.FromArgb(240, 230, 210);
        }

        private DateTime ObtenerLunes(DateTime fecha)
        {
            while (fecha.DayOfWeek != DayOfWeek.Monday)
                fecha = fecha.AddDays(-1);
            return fecha;
        }

        private void InicializarTabla()
        {
            tblAgenda.ColumnCount = 8; // 1 columna para proyecto + 7 días
            tblAgenda.RowCount = 0;
            tblAgenda.Controls.Clear();
            tblAgenda.RowStyles.Clear();
            tblAgenda.ColumnStyles.Clear();

            tblAgenda.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // Columna Proyecto

            // Lunes a Domingo
            for (int i = 1; i <= 7; i++)
                tblAgenda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7f));
        }


        private void CargarSemana()
        {
            tblAgenda.Controls.Clear();
            tblAgenda.RowStyles.Clear();

            // Siempre empezamos con una fila (encabezados)
            tblAgenda.RowCount = 1;

            // Encabezados
            tblAgenda.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tblAgenda.Controls.Add(new Label()
            {
                Text = "Proyecto",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            }, 0, 0);

            for (int i = 0; i < 7; i++)
            {
                tblAgenda.Controls.Add(new Label()
                {
                    Text = semanaActual.AddDays(i).ToString("ddd dd", new System.Globalization.CultureInfo("es-MX")),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }, i + 1, 0);
            }

            // Dibujar planeaciones
            int filasAntes = tblAgenda.RowCount;
            CargarPlaneacionesBD();

            // Si no se agregó ninguna planeación, agregar una fila vacía
            if (tblAgenda.RowCount == filasAntes)
            {
                tblAgenda.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                tblAgenda.RowCount++;

                Label lblVacio = new Label()
                {
                    Text = "No hay planeaciones esta semana",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(10),
                    ForeColor = Color.Gray
                };

                tblAgenda.Controls.Add(lblVacio, 0, tblAgenda.RowCount - 1);
            }

            // Actualiza el label superior (ya existe en tu form)
            lblSemana.Text = $"{semanaActual:MMMM yyyy}".ToUpper();
        }





        private void CargarPlaneacionesBD()
        {
            string query = @"SELECT Titulo_Proyecto, Fecha_Creacion, Fecha_Entrega FROM planeaciones";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string titulo = reader["Titulo_Proyecto"].ToString();
                DateTime inicio = Convert.ToDateTime(reader["Fecha_Creacion"]);
                DateTime fin = Convert.ToDateTime(reader["Fecha_Entrega"]);

                DibujarPlaneacion(titulo, inicio, fin);
            }

            reader.Close();
        }

        private void DibujarPlaneacion(string titulo, DateTime inicio, DateTime fin)
        {
            // Definir el rango de la semana mostrada (lunes → domingo)
            DateTime inicioSemana = semanaActual;
            DateTime finSemana = semanaActual.AddDays(6);

            // Si la planeación NO tiene cruce con la semana, no se dibuja
            if (fin < inicioSemana || inicio > finSemana)
                return;

            int fila = tblAgenda.RowCount;
            tblAgenda.RowCount++;

            tblAgenda.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // altura fija para filas

            // Primera columna: nombre del proyecto
            tblAgenda.Controls.Add(new Label()
            {
                Text = titulo,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            }, 0, fila);

            // Pinta los días correspondientes en la semana
            for (int d = 0; d < 7; d++)
            {
                DateTime dia = semanaActual.AddDays(d);

                Panel panel = new Panel
                {
                    Margin = new Padding(2),
                    Dock = DockStyle.Fill,
                    BackColor = (dia >= inicio && dia <= fin) ? Color.LightBlue : Color.Transparent,
                    BorderStyle = (dia >= inicio && dia <= fin) ? BorderStyle.FixedSingle : BorderStyle.None
                };

                tblAgenda.Controls.Add(panel, d + 1, fila);
            }
        }



        private void ActualizarTituloSemana()
        {
            // Muestra MES + AÑO
            lblSemana.Text = semanaActual.ToString("MMMM yyyy").ToUpper();
        }





        private void btnAnterior_Click(object sender, EventArgs e)
        {
            semanaActual = semanaActual.AddDays(-7);
            CargarSemana();
            ActualizarTituloSemana();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            semanaActual = semanaActual.AddDays(7);
            CargarSemana();
            ActualizarTituloSemana();
        }
    }
}
