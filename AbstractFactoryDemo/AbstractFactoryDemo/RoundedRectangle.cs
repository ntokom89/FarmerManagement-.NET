using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
   public  class RoundedRectangle: Shape
    {
        public void draw()
        {
            Console.WriteLine("This shape is rounded Rectangle.");
        }
    }
}
