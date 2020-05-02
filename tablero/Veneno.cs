using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tablero
{
    public class Veneno:ITrampas
    {
        public void display()
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("v");

        }

        public int muere()
        {
            return 5;
        }
    }
}