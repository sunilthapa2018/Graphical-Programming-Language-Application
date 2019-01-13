using System;
using System.Drawing;
namespace Paint
{
    [Serializable]
    class Circle : Shape
    {
        public int x { get; set; }
        public int y { get; set; }
        public int radius { get; set; }        
        public Color color { get; set; }


        /// <summary>
        /// to draw circle 
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {            
            Pen pen = new Pen(color, 1);
            g.DrawEllipse(pen, new Rectangle(x, y, radius, radius));
            pen.Dispose();            
        }

        /// <summary>
        /// parameter to draw circle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>        
        
        public void SetParam(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.radius = width;
            this.color = color;
            
        }
    }
}
