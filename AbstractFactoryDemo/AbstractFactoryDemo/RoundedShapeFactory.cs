using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
    class RoundedShapeFactory: AbstractFactory
    {
        public override Shape getShape(String shapeType) {
            if (shapeType.Equals("rounded square", StringComparison.OrdinalIgnoreCase))
            {
                return new RoundedSquare();
            }
            else if (shapeType.Equals("rounded rectangle", StringComparison.OrdinalIgnoreCase))
            {
                return new RoundedRectangle();
            }
            else
            {
                return null;
            }
        }
    }
}
