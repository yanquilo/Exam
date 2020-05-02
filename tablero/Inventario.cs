using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tablero
{
    public class Inventario
    {
        public List<Item> items;
        public int itemIndex;

        public Inventario()
        {
            itemIndex = -1;
            items = new List<Item>();
          
        }

        public void Display()
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (i != itemIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.SetCursorPosition(42, 6+i);
                items[i].Display();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(42, 6 + items.Count);
            Console.Write("               ");

        }

        public bool TryAdd(Item objeto)
        {
            if (items.Count < 15)
            {
                items.Add(objeto);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SelectItem(int numeroItem)
        {

            itemIndex = numeroItem;
        }

        public bool UseItem()
        {
            if (itemIndex!=-1)
            {
                Item objeto = items[itemIndex];

                if (objeto is Moneda)
                {
                    Jugador.money += 10;
                    this.Borra();
                    return true;
                }
                else if (objeto is Pocion)
                {
                    if (Jugador.live < 150)
                    {
                        Jugador.live += 50;
                        this.Borra();
                        return true;
                    }
                    else
                    {

                    }
                   
                   
                    return true;
                }
                else if (objeto is Llave)
                {
                    Jugador.llave += 1;
                    this.Borra();
                    return true;
                }
                else
                {
                    itemIndex = -1;
                    return false;
                }
               
            }
            else
            {
                return false;
            }

        }

        public bool Borra()
        {

            items.RemoveAt(itemIndex);
            itemIndex = -1;
            return true;
        }
    }
}