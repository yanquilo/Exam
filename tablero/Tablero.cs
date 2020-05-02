using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tablero
{
    public class Tablero
    {
        public Celda[,] celdas;
        public int anchura, altura;

        public int endX, endY;


        public Tablero(int x, int y)
        {
            celdas = new Celda[x, y];
            anchura = celdas.GetLength(0);
            altura = celdas.GetLength(1);

            for (int i = 0; i < celdas.GetLength(0); i++)
            {
                for (int j = 0; j < celdas.GetLength(1); j++)
                {
                    celdas[i, j] = new Celda();
                }
            }
       

        }

        public bool isSafe(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < anchura && y < altura)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void putItems(int quantity)
        {
            Random r = new Random();
            for (int m = 0; m < quantity; m++)
            {
                int x, y;
                do
                {
                    x = r.Next(this.anchura);
                    y = r.Next(this.altura);
                } while (celdas[x, y].valor != TipoCelda.FLOOR || celdas[x,y].objeto!=null );

                if (r.Next(100) < 90)
                {
                    celdas[x, y].objeto = new Moneda();
                }
                else if(r.Next(100)<50)
                {
                    celdas[x, y].objeto = new Pocion();
                }else if(r.Next(100)<20)
                {
                    celdas[x, y].objeto = new Llave();
                }

            }
        }
        public void PutMines(int quantity)
        {
            Random r = new Random();
            for (int m = 0; m < quantity; m++)
            {
                int x, y;
                do
                {
                    x = r.Next(celdas.GetLength(0));
                    y = r.Next(celdas.GetLength(1));

                } while (celdas[x, y].IsMine());

                // no hay mina en x,y
                celdas[x, y].PutMine();
            }

        }

        public void putTrampas(int quantity)
        {
            Random r = new Random();
            for (int m = 0; m < quantity; m++)
            {
                int x, y;
                do
                {
                    x = r.Next(this.anchura);
                    y = r.Next(this.altura);
                } while (celdas[x, y].valor != TipoCelda.FLOOR || celdas[x, y].objeto != null||celdas[x,y].matar!=null);

                if (r.Next(100) < 60)
                {
                    celdas[x, y].matar = new Fuego();
                }
                else if (r.Next(100) <60)
                {
                    celdas[x, y].matar = new Veneno();
                }
             
            }
        }

        public void Display(int centroX, int centroY)
        {
            int ventanaX = 41, ventanaY = 15;



            for (int i = 0; i < ventanaX; i++)
            {
                for (int j = 0; j < ventanaY; j++)
                {

                    Console.SetCursorPosition(i, j);

                    int celdaX, celdaY;

                    celdaX = centroX - ventanaX / 2 + i;
                    celdaY = centroY - ventanaY / 2 + j;

                    if (celdaX >= 0 && celdaX < anchura && celdaY >= 0 && celdaY < altura)
                    {
                        double dist = Math.Sqrt(Math.Pow(celdaX - centroX, 2) + Math.Pow(celdaY - centroY, 2));
                        if (dist < 7)
                        {
                            celdas[celdaX, celdaY].visible = true;
                        }


                        celdas[celdaX, celdaY].Display();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("~");
                    }
                }
            }
        }

        public void RandomWalk(int maxFloors)
        {
            // Random walk
          
            int x, y;
            int floors;
            Random r = new Random();

            // initialize all map cells to walls. 
            for (int i = 0; i < anchura; i++)
            {
                for (int j = 0; j < altura; j++)
                {
                    celdas[i, j].valor = TipoCelda.WALL;
                    celdas[i, j].visible = false;
                }
            }
            // pick a map cell as the starting point. 
            x = anchura / 2;
            y = altura / 2;

            // turn the selected map cell into floor. 
            celdas[x, y].valor = TipoCelda.FLOOR;
            floors = 1;


            // while insufficient cells have been turned into floor,
            while (floors <= maxFloors)
            {

                //take one step in a random direction.
                int direction = r.Next(4);

                switch (direction)
                {
                    case 0:
                        x++;
                        break;
                    case 1:
                        x--;
                        break;
                    case 2:
                        y++;
                        break;
                    case 3:
                        y--;
                        break;
                }

                // comprobar si la x,y salen del mapa
                if (x < 0 || y < 0 || x >= anchura || y >= altura)
                {
                    x = anchura / 2;
                    y = altura / 2;
                }


                // if the new map cell is wall,
                //turn the new map cell into floor and increment the count of floor tiles. 
                if (celdas[x, y].valor == TipoCelda.WALL)
                {
                    celdas[x, y].valor = TipoCelda.FLOOR;
                    floors++;
                }


            } // END WHILE

            // FIN RANDOM WALK

            // Choose a end point
            double dist;
            do
            {
                endX = r.Next(anchura);
                endY = r.Next(altura);
                dist = Math.Sqrt(Math.Pow(endX - anchura / 2, 2) + Math.Pow(endY - altura / 2, 2));

            } while (dist < anchura / 4 || celdas[endX, endY].valor != TipoCelda.FLOOR);
            celdas[endX, endY].valor = TipoCelda.EXIT;
            // CalculaVecinos();


        }


    }
}