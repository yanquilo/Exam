using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero
{
    public class Celda
    {
        public int valor;
        public int vecinos;
        public Boolean visible;
        public Item objeto;
        public ITrampas matar;

        public Celda()
        {
            valor = TipoCelda.EMPTY;
            vecinos = 0;
            visible = false;
            objeto = null;
            matar = null;
        }

        public void PutMine()
        {
            valor = TipoCelda.MINE;
        }

        public void RemoveMine()
        {
            valor = TipoCelda.EMPTY;
        }

        public bool IsMine()
        {
            return (valor == TipoCelda.MINE);
        }

        public bool IsWalkable()
        {
            if (valor == TipoCelda.WALL)
            {
                return false;
            }else
            {
                return true;
            }
        }

        public void Display()
        {
            if (visible==true)
            {
                if (this.objeto != null)
                {
                    if(this.objeto is Moneda)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("O");
                    }
                    else if (this.objeto is Pocion)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("P");
                    }
                    else if(this.objeto is Llave){
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Y");
                    }
                }
                else if (this.matar!=null) {
                    if (this.matar is Fuego)
                    {
                        matar.display();
                    }
                    else if (this.matar is Veneno)
                    {
                        matar.display();
                    }
                }
                else 
                {
                    switch (valor)
                    {
                        case TipoCelda.WALL:
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write(" ");
                            break;
                        case TipoCelda.FLOOR:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write(" ");
                            break;
                        case TipoCelda.EXIT:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("[]");
                            break;
                    }
                }
            }
            else
            {
                // oculto
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("~");
            }

        }
    }
}
