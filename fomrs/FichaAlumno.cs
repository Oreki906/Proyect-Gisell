using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using WinFont = System.Drawing.Font;
using WinFontStyle = System.Drawing.FontStyle;

namespace Login.fomrs
{
    public partial class FichaAlumno : Form
    {
        private int idAlumno;
        private string nombreAlumno;
        private MySqlConnection conexion;

        private List<decimal> listaPromedios = new List<decimal>();

        public FichaAlumno(int idAlumno, string nombreAlumno, MySqlConnection conexion)
        {
            InitializeComponent();
            this.idAlumno = idAlumno;
            this.nombreAlumno = nombreAlumno;
            this.conexion = conexion;

            lblTitulo.Text = "Boleta de " + nombreAlumno;
            lblTitulo.Font = new WinFont("Segoe UI Semibold", 20, FontStyle.Bold);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
           

            CargarMateriasYCalificaciones();
            MostrarPromedioGeneral();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse
        );


        private void CargarMateriasYCalificaciones()
        {
            string query = @"
        SELECT 
            m.id_Materia,
            m.Nombre_Materia,
            m.Unidades
        FROM Alumno_Materia am
        INNER JOIN Materias m ON am.id_Materia = m.id_Materia
        WHERE am.id_Alumno = @alumno;
    ";

            List<(int idMateria, string materia, int unidades)> materias =
    new List<(int idMateria, string materia, int unidades)>();

            using (MySqlCommand cmd = new MySqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@alumno", idAlumno);

                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        materias.Add((
                            rd.GetInt32("id_Materia"),
                            rd.GetString("Nombre_Materia"),
                            rd.GetInt32("Unidades")
                        ));
                    }
                }
            }

            // Mostrar una tarjeta por cada materia
            foreach (var item in materias)
            {
                MostrarMateriaEnTarjeta(item.idMateria, item.materia, item.unidades);
            }
        }



        private void MostrarMateriaEnTarjeta(int idMateria, string nombreMateria, int unidades)
        {
            Panel tarjeta = new Panel();
            tarjeta.BackColor = Color.White;
            tarjeta.Padding = new Padding(20);
            tarjeta.Margin = new Padding(15, 10, 15, 10);
            tarjeta.Width = panelContenido.Width - 50;
            tarjeta.AutoSize = true;
            tarjeta.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Borde redondeado
            tarjeta.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, tarjeta.Width, 300, 20, 20)
            );

            // --- Título ---
            Label lblMateria = new Label();
            lblMateria.Text = $"Materia: {nombreMateria}";
            lblMateria.Font = new WinFont("Segoe UI Semibold", 14, FontStyle.Bold);
            lblMateria.ForeColor = Color.FromArgb(40, 40, 70);
            lblMateria.AutoSize = true;

            tarjeta.Controls.Add(lblMateria);

            int offsetY = 50;
            decimal suma = 0;
            int count = 0;

            // --- Unidades ---
            for (int u = 1; u <= unidades; u++)
            {
                string cal = ObtenerCalificacion(idMateria, u);
                if (cal == "Sin registro") cal = "-";

                Label lblUnidad = new Label();
                lblUnidad.Text = $"Unidad {u}: {cal}";
                lblUnidad.Font = new WinFont("Segoe UI", 11F);
                lblUnidad.ForeColor = Color.Black;
                lblUnidad.AutoSize = true;
                lblUnidad.Top = offsetY;
                lblUnidad.Left = 30;

                tarjeta.Controls.Add(lblUnidad);
                offsetY += 28;

                if (decimal.TryParse(cal, out decimal val))
                {
                    suma += val;
                    count++;
                }
            }

            decimal promedio = (count > 0 ? suma / count : 0);

            // --- Promedio ---
            Label lblPromedio = new Label();
            lblPromedio.Text = $"Promedio: {(count > 0 ? promedio.ToString("0.00") : "-")}";
            lblPromedio.Font = new WinFont("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblPromedio.ForeColor = Color.FromArgb(20, 80, 20);
            lblPromedio.AutoSize = true;
            lblPromedio.Top = offsetY + 10;
            lblPromedio.Left = 30;

            tarjeta.Controls.Add(lblPromedio);

            panelContenido.Controls.Add(tarjeta);

            if (count > 0)
                listaPromedios.Add(promedio);
        }

     

        private string ObtenerCalificacion(int idMateria, int unidad)
        {
            string sql = @"
                SELECT Calificacion
                FROM UnidadCalificacion
                WHERE id_Alumno = @al
                AND id_Materia = @mat
                AND Unidad = @u;";

            using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
            {
                cmd.Parameters.AddWithValue("@al", idAlumno);
                cmd.Parameters.AddWithValue("@mat", idMateria);
                cmd.Parameters.AddWithValue("@u", unidad);

                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return "Sin registro";

                return Convert.ToDecimal(result).ToString("0.00");
            }
        }
        private void ExportarPDF()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = $"Boleta_{nombreAlumno}.pdf";
            save.Filter = "PDF (*.pdf)|*.pdf";

            if (save.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));
                doc.Open();

                // Título
                Paragraph titulo = new Paragraph($"Boleta de {nombreAlumno}",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.SpacingAfter = 20;

                doc.Add(titulo);

                // Recorrer tarjetas para exportar
                foreach (Control ctrl in panelContenido.Controls)
                {
                    if (ctrl is Panel tarjeta)
                    {
                        // Título de materia
                        Label lblMateria = tarjeta.Controls.OfType<Label>().First();
                        Paragraph materia = new Paragraph(lblMateria.Text,
                            FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
                        materia.SpacingBefore = 10;
                        materia.SpacingAfter = 10;
                        doc.Add(materia);

                        // Unidades
                        foreach (Label lbl in tarjeta.Controls.OfType<Label>())
                        {
                            if (lbl.Text.StartsWith("Unidad"))
                            {
                                Paragraph unidad = new Paragraph(lbl.Text,
                                    FontFactory.GetFont(FontFactory.HELVETICA, 12));
                                unidad.IndentationLeft = 20;
                                unidad.SpacingAfter = 5;
                                doc.Add(unidad);
                            }
                        }

                        // Promedio
                        Label lblProm = tarjeta.Controls
                                               .OfType<Label>()
                                               .FirstOrDefault(l => l.Text.StartsWith("Promedio"));

                        if (lblProm != null)
                        {
                            Paragraph promedio = new Paragraph(lblProm.Text,
                                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
                            promedio.IndentationLeft = 20;
                            promedio.SpacingAfter = 15;
                            doc.Add(promedio);
                        }

                        // Línea separadora
                        doc.Add(new Paragraph(" "));
                    }
                    else if (ctrl is Label general && general.Text.StartsWith("Promedio general"))
                    {
                        Paragraph promedioGeneral = new Paragraph(general.Text,
                            FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                        promedioGeneral.SpacingBefore = 20;
                        promedioGeneral.Alignment = Element.ALIGN_CENTER;

                        doc.Add(promedioGeneral);
                    }
                }

                doc.Close();
                MessageBox.Show("PDF generado correctamente.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message);
            }
        }

    

        private void MostrarPromedioGeneral()
        {
            if (listaPromedios.Count == 0) return;

            decimal general = listaPromedios.Average();

            Label lblGeneral = new Label();
            lblGeneral.Text = $"Promedio general: {general:F2}";
            lblGeneral.Font = new WinFont("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblGeneral.ForeColor = Color.FromArgb(30, 100, 30);
            lblGeneral.Margin = new Padding(20);
            lblGeneral.AutoSize = true;

            panelContenido.Controls.Add(lblGeneral);
        }
        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            ExportarPDF();
        }
    }
}
