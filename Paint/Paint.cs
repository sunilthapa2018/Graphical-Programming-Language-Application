using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
        OpenFileDialog openFile = new OpenFileDialog();
        String line = "";
        Validation validate;

        public int raduis = 0;
        public int width = 0;
        public int height = 0;
        public int counter = 0;
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK) {
                txtCommand.Clear();
                line = "";
                StreamReader sr = new StreamReader(openFile.FileName);
                while (line != null) {
                    line = sr.ReadLine();
                    if (line != null) {                        
                        txtCommand.Text += line;
                        txtCommand.Text += "\r\n";
                    }
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtCommand.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File | *.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                using (Stream s = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s)) {
                    sw.Write(txtCommand.Text);
                }
                MessageBox.Show("Your File has been saved Sucessfully");
            }            
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (txtCommand.Text != null && txtCommand.Text != "") {
                validate = new Validation(txtCommand);
                if (!validate.isSomethingInvalid)
                {
                    MessageBox.Show("Everything is working fine");
                    RunCommand();
                }

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
        private void RunCommand() {
            int numberOfLines = txtCommand.Lines.Length;
            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    Boolean hasPlus = oneLineCommand.Contains('+');
                    Boolean hasEquals = oneLineCommand.Contains("=");
                    if (hasEquals) {
                        string[] words2 = oneLineCommand.Split('=');
                        for (int j = 0; j < words2.Length; j++)
                        {
                            words2[j] = words2[j].Trim();
                        }
                        if (words2[0].ToLower().Equals("radius")) {
                            raduis = int.Parse(words2[1]);
                        } else if (words2[0].ToLower().Equals("width")) {
                            width = int.Parse(words2[1]);
                        } else if (words2[0].ToLower().Equals("height")) {
                            height = int.Parse(words2[1]);
                        }
                    } else if (hasPlus) {
                        string[] words2 = oneLineCommand.Split('+');
                        for (int j = 0; j < words2.Length; j++)
                        {
                            words2[j] = words2[j].Trim();
                        }
                        if (words2[0].ToLower().Equals("radius"))
                        {
                            raduis += int.Parse(words2[1]);
                        }
                        else if (words2[0].ToLower().Equals("width"))
                        {
                            width += int.Parse(words2[1]);
                        }
                        else if (words2[0].ToLower().Equals("height"))
                        {
                            height += int.Parse(words2[1]);
                        }
                    }
                    else {
                        sendDrawCommand(oneLineCommand);
                    }
                    
                }

            }
        }
        private void sendDrawCommand(string lineOfCommand)
        {
            String[] shapes = { "circle", "rectangle", "triangle", "polygon" };
            String[] variable = { "radius", "width", "height", "counter" };

            lineOfCommand = System.Text.RegularExpressions.Regex.Replace(lineOfCommand, @"\s+", " ");
            string[] words = lineOfCommand.Split(' ');
            //removing white spaces in between words
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }
            String firstWord = words[0].ToLower();
            Boolean firstWordShape = shapes.Contains(firstWord);
            if (firstWordShape) {
                
                if (firstWord.Equals("circle")) {
                    Boolean secondWordIsVariable = variable.Contains(words[1].ToLower());
                    if (secondWordIsVariable) {
                        if (words[1].ToLower().Equals("radius")) {
                            DrawCircle(raduis);
                        }
                    }
                    else {
                        DrawCircle(Int32.Parse(words[1]));
                    }
                    
                } else if (firstWord.Equals("rectangle")) {
                    String args = lineOfCommand.Substring(9, (lineOfCommand.Length - 9));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    Boolean secondWordIsVariable = variable.Contains(parms[0].ToLower());
                    Boolean thirdWordIsVariable = variable.Contains(parms[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(width, height);
                        }
                        else {
                            DrawRectangle(width, Int32.Parse(parms[1]));
                        }
                        
                    }
                    else
                    {
                        if (thirdWordIsVariable) {
                            DrawRectangle(Int32.Parse(parms[0]), height);
                        }
                    }                    
                }
                else if (firstWord.Equals("triangle"))
                {
                    String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    DrawTriangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]), Int32.Parse(parms[2]));
                }
                else if (firstWord.Equals("polygon"))
                {
                    String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    if (parms.Length == 8){
                        DrawPolygon(Int32.Parse(parms[0]), Int32.Parse(parms[1]), Int32.Parse(parms[2]), Int32.Parse(parms[3]),
                            Int32.Parse(parms[4]), Int32.Parse(parms[5]), Int32.Parse(parms[6]), Int32.Parse(parms[7]));
                    } else if (parms.Length == 10) {
                        DrawPolygon(Int32.Parse(parms[0]), Int32.Parse(parms[1]), Int32.Parse(parms[2]), Int32.Parse(parms[3]),
                            Int32.Parse(parms[4]), Int32.Parse(parms[5]), Int32.Parse(parms[6]), Int32.Parse(parms[7]),
                            Int32.Parse(parms[8]), Int32.Parse(parms[9]));
                    }
                    
                }
            }             
        }        
        private void DrawPolygon(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8)
        {
            Pen myPen = new Pen(mainColor);
            Point[] pnt = new Point[5];

            pnt[0].X = mouseX;
            pnt[0].Y = mouseY;

            pnt[1].X = mouseX - v1;
            pnt[1].Y = mouseY - v2;

            pnt[2].X = mouseX - v3;
            pnt[2].Y = mouseY - v4;

            pnt[3].X = mouseX - v5;
            pnt[3].Y = mouseY - v6;

            pnt[4].X = mouseX - v7;
            pnt[4].Y = mouseY - v8;

            g.DrawPolygon(myPen, pnt);
        }
        private void DrawPolygon(int v1, int v2, int v3, int v4, int v5,int v6, int v7, int v8, int v9, int v10)
        {
            Pen myPen = new Pen(mainColor);
            Point[] pnt = new Point[6];

            pnt[0].X = mouseX;
            pnt[0].Y = mouseY;

            pnt[1].X = mouseX - v1;
            pnt[1].Y = mouseY - v2;

            pnt[2].X = mouseX - v3;
            pnt[2].Y = mouseY - v4;

            pnt[3].X = mouseX - v5;
            pnt[3].Y = mouseY - v6;

            pnt[4].X = mouseX - v7;
            pnt[4].Y = mouseY - v8;

            pnt[5].X = mouseX - v9;
            pnt[5].Y = mouseY - v10;
            g.DrawPolygon(myPen, pnt);            
        }

        private void DrawTriangle(int rBase, int adj, int hyp)
        {
            Pen myPen = new Pen(mainColor);
            Point[] pnt = new Point[3];

            pnt[0].X = mouseX;
            pnt[0].Y = mouseY;

            pnt[1].X = mouseX - rBase;
            pnt[1].Y = mouseY ;

            pnt[2].X = mouseX;
            pnt[2].Y = mouseY - adj;
            g.DrawPolygon(myPen, pnt);
        }

        private void DrawRectangle(int width, int height)
        {            
            Pen myPen = new Pen(mainColor);
            g.DrawRectangle(myPen, mouseX - width / 2, mouseY - height / 2, width, height);
        }

        private void DrawCircle(int radius)
        {
            Pen myPen = new Pen(mainColor);
            g.DrawEllipse(myPen, mouseX - radius , mouseY - radius , radius*2, radius*2);
        }
    }
}
