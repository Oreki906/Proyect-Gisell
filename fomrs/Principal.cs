using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.fomrs
{
    public partial class Principal : Form

    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public Principal()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(40, 40, 40);



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
           
            
        }

        private void btncalificaciones_Click(object sender, EventArgs e)
        {
            
        }

        private void btnalumnos_Click(object sender, EventArgs e)
        {
            
        }

        private void btnasistencias_Click(object sender, EventArgs e)
        {
            btnasistencias.BackColor = Color.FromArgb(25, 42, 86);
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

            // Agregar tarjeta al FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(tarjeta);
        }
        private int contador = 1;
        private void button1_Click_1(object sender, EventArgs e)
        {
            CrearMateria($"Materia {contador}");
            contador++;
        }
    }
}
