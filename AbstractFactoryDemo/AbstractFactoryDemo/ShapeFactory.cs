using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
    public class ShapeFactory : AbstractFactory
    {
        public override Shape getShape(String shapeType)
        {
            if (shapeType.Equals("rectangle", StringComparison.OrdinalIgnoreCase))
            {
                return new Rectangle();
            }
            else if (shapeType.Equals("square", StringComparison.OrdinalIgnoreCase))
            {
                return new Square();
            }
            else
            {
                return null;
            }

        }
    }
}
