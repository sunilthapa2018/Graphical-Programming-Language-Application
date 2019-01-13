using System.Drawing;
namespace Paint
{
    interface Shape
    {
        void Draw(Graphics g);
        void SetParam(int x, int y, int width, int height, Color color);
    }
}
