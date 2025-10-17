using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Login
{
    public class RoundedPictureBox : PictureBox
    {
        private int borderSize = 3;
        private Color borderColor = Color.Black;

        [Category("Apariencia")]
        public int BorderSize
        {
            get { return borderSize; }
            set { borderSize = Math.Max(0, value); Invalidate(); }
        }

        [Category("Apariencia")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        public RoundedPictureBox()
        {
            SizeMode = PictureBoxSizeMode.Zoom;
            BackColor = Color.Transparent;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Mantener forma perfectamente circular
            int side = Math.Min(Width, Height);
            this.Size = new Size(side, side);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, Width - 1, Height - 1);
                this.Region = new Region(path);
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Dibuja la imagen dentro del círculo
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(ClientRectangle);
                pe.Graphics.SetClip(path);
                base.OnPaint(pe);
            }

            // Dibuja borde circular
            if (borderSize > 0)
            {
                Rectangle rect = new Rectangle(borderSize / 2, borderSize / 2,
                                               Width - borderSize, Height - borderSize);
                using (Pen pen = new Pen(borderColor, borderSize))
                {
                    pe.Graphics.DrawEllipse(pen, rect);
                }
            }
        }
    }
}
