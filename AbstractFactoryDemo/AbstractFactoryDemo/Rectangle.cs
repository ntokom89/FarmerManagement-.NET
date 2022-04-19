using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
    class Rectangle : Shape
    {
        public void draw()
        {
            Console.WriteLine("This shape is rectangle.");
        }
    }
}
