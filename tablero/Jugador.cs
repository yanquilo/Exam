using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace tablero
{
    public class Jugador
    {
        public int x;
        public int y;
        public static int live = 150;
        public static int money = 0;
        public static int llave = 0;
        
        Tablero mapa;
        Inventario mochila;

        public Jugador(int x, int y, Tablero mapa)
        {
            this.x = x;
            this.y = y;
            this.mapa = mapa;

            mochila = new Inventario();

        }
        public void kill() {
            if (mapa.celdas[x, y].matar != null) {
                if (mapa.celdas[x, y].matar is Fuego) {
                    live -= mapa.celdas[x, y].matar.muere();
                    mapa.celdas[x, y].matar = null;
                }
                else if (mapa.celdas[x, y].matar is Veneno)
                {
                    live -= mapa.celdas[x, y].matar.muere();
                    mapa.celdas[x, y].matar = null;
                }
            }
        }
        public void UseItem()
        {

            mochila.UseItem();

        }

        public void CogeItem()
        {
            if (mapa.celdas[x, y].objeto != null)
            {

                bool puedecoger;

                // Meto en la mochila.

                puedecoger = mochila.TryAdd(mapa.celdas[x, y].objeto);

                if (puedecoger==true)
                {

                    // Quito del suelo.

                    mapa.celdas[x, y].objeto = null; 
                }
            }
        }

        // Verificamos que no atraviese los muros.
        public void Move(int incX, int incY)  
        {
            int nuevax, nuevay;

            nuevax = x + incX;
            nuevay = y + incY;

            if (mapa.isSafe(nuevax,nuevay)) 
            {
                if (mapa.celdas[nuevax, nuevay].IsWalkable())
                {
                    x = nuevax;
                    y = nuevay;
                }
            }
        }
        //Para mover Arriba
        public void MoveUp()
        {
            Move(0, -1);
            live--;
        }
        //Para mover Abajo
        public void MoveDown()
        {
            Move(0, 1);
            live--;
        }
        //Para mover a la izquierda
        public void MoveLeft()
        {
            Move(-1, 0);
            live--;
        }
        //Para mover a la Derecha
        public void MoveRight()
        {
            Move(1, 0);
            live--;
        }


        public void Display()
        {
            mochila.Display();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(20, 7);
            Console.Write('º');


        }

        internal void selectPreviousItem()
        {
            
            mochila.itemIndex = Math.Max(mochila.itemIndex-1,-1);
        }

        internal void selectNextItem()
        {
            mochila.itemIndex = Math.Min(mochila.items.Count - 1, mochila.itemIndex+1);  
        }

      
    }
}