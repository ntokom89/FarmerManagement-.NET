using System;

namespace AbstractFactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            AbstractFactory factory = new ShapeFactory();
            Shape shape1 = new Rectangle();
            shape1.draw();

            Shape
        }
    }
}
