using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
   public  class RoundedSquare: Shape
    {
        public void draw()
        {
            Console.WriteLine("This shape is rounded square.");
        }
    }
}
