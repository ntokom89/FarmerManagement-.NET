using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
    public class FactoryProducer
    {
       public static AbstractFactory getFactory(bool rounded)
        {
            if (rounded)
            {
                return new RoundedShapeFactory();
            }
            else
            {
                return new ShapeFactory();
            }
        }
    }
}
