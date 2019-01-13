namespace Paint
{
    class ShapeFactory
    {
        public Shape getShape(string shapeType)
        {
            Shape shape = null;
            switch (shapeType)
            {
                case "CIRCLE":
                    shape = new Circle();
                    break;
                case "RECTANGLE":
                    shape = new Rectangles();
                    break;                
            }
            return shape;
        }
    }
}
