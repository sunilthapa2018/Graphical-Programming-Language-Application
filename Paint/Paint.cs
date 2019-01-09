using System;
using System.Drawing;
using System.Windows.Forms;


namespace Paint
{    
    public partial class MsPaint : Form
    {
        Color btnBorderColor = Color.FromArgb(104, 162, 255);
        Color mainColor = Color.Black;
        int size = 2;
        Graphics g;
        int x,y = -1;
        int mouseX, mouseY = 0;
        Boolean moving = false;
        Pen pen;
        String active = "pen";        
           

        
        public MsPaint()
        {
            InitializeComponent();
            g = panelPaint.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(mainColor, size);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            addPenSizes();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F10:
                    btnRun.PerformClick();
                    return true;                    
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void addPenSizes() {
            cboSize.Items.Clear();
            cboSize.Items.AddRange(new String[] {"1", "2", "4", "6", "8"});
            cboSize.SelectedIndex = 0;
        }

        private void addDrawingSizes()
        {
            cboSize.Items.Clear();
            cboSize.Items.AddRange(new String[] { "10", "20", "40", "50", "100" });
            cboSize.SelectedIndex = 0;
        }

        private void MsPaint_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            setAllBorderColorForButtons();
            btnPen.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = new Cursor(Properties.Resources.pen.GetHicon());
        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.Cancel) {
                btnChooseColor.BackColor = colorDialog1.Color;
                mainColor = colorDialog1.Color;
            }
            pen = new Pen(mainColor, size);
        }

        private void btnPen_Click(object sender, EventArgs e)
        {
            cboSize.Enabled = true;
            active = "pen";
            removeAllBorderFromButtons();
            btnPen.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = new Cursor(Properties.Resources.pen.GetHicon());
            addPenSizes();

        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            active = "eraser";            
            removeAllBorderFromButtons();
            btnEraser.FlatAppearance.BorderSize = 1;            
            panelPaint.Cursor = new Cursor(Properties.Resources.eraser.GetHicon());
            cboSize.Enabled = false;

        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            active = "rectangle";
            removeAllBorderFromButtons();
            btnRectangle.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = Cursors.Default;
            addDrawingSizes();
            cboSize.Enabled = true;
        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            active = "triangle";
            removeAllBorderFromButtons();
            btnTriangle.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = Cursors.Default;
            addDrawingSizes();
            cboSize.Enabled = true;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            active = "circle";
            removeAllBorderFromButtons();
            btnCircle.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = Cursors.Default;
            addDrawingSizes();
            cboSize.Enabled = true;
        }

        private void setAllBorderColorForButtons()
        {
            btnPen.FlatAppearance.BorderColor = btnBorderColor;
            btnEraser.FlatAppearance.BorderColor = btnBorderColor;
            btnRectangle.FlatAppearance.BorderColor = btnBorderColor;
            btnTriangle.FlatAppearance.BorderColor = btnBorderColor;
            btnCircle.FlatAppearance.BorderColor = btnBorderColor;
        }

        private void removeAllBorderFromButtons()
        {
            btnPen.FlatAppearance.BorderSize = 0;
            btnEraser.FlatAppearance.BorderSize = 0;
            btnRectangle.FlatAppearance.BorderSize = 0;
            btnTriangle.FlatAppearance.BorderSize = 0;
            btnCircle.FlatAppearance.BorderSize = 0;
        }
        private void panelPaint_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        private void panelPaint_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;            
        }

        private void cboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = Convert.ToInt32(cboSize.SelectedItem.ToString());
            size = size * 5;
            pen = new Pen(mainColor, size);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            panelPaint.Refresh();
        }

        private void panelPaint_MouseClick(object sender, MouseEventArgs e)
        {
            if (active.Equals("pen"))
            {
                g.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }
            else if (active.Equals("eraser"))
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                g.FillRectangle(myBrush, e.X, e.Y, 24, 24);
            }else if (active.Equals("rectangle"))
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(mainColor);
                g.FillRectangle(myBrush, e.X - size/2, e.Y - size / 2, size, size);
            }
            else if (active.Equals("triangle"))
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(mainColor);
                Point[] pnt = new Point[3];

                pnt[0].X = e.X;
                pnt[0].Y = e.Y - size;

                pnt[1].X = e.X + size;
                pnt[1].Y = e.Y + size;

                pnt[2].X = e.X - size;
                pnt[2].Y = e.Y + size;
                g.FillPolygon(myBrush, pnt);
            }
            else if (active.Equals("circle"))
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(mainColor);
                g.FillEllipse(myBrush, e.X - size / 2, e.Y - size / 2, size, size);
                
            }
        }       

        

        private void panelPaint_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            if (moving && x != -1 && y != -1) {
                if (active.Equals("pen"))
                {
                    g.DrawLine(pen, new Point(x, y), e.Location);
                    x = e.X;
                    y = e.Y;
                } else if (active.Equals("eraser")) {
                    //g.DrawRectangle(pen, e.X, e.Y, 24, 24);
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                    g.FillRectangle(myBrush, e.X, e.Y, 24, 24);
                }                
            }
        }        
    }
}
