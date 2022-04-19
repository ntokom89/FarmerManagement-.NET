using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
    public abstract class AbstractFactory
    {
        public abstract Shape getShape(String shapeType);
    }
}
