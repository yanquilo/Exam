using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tablero
{
    public class Fuego: ITrampas
    {
        public void display() {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ñ");

        }

        public int muere() {
            return 10;
        }

    }
}