using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero
{
    class Program
    {


        static void Main(string[] args)
        {
            ConsoleKeyInfo tecla;


            Tablero miTablero;
            Jugador player;
            int level = 1;
            int estado;
            int gm;
            const int Titulo = 1;
            const int Juego = 2;
            const int Game_Over = 4;
            const int GG = 3;
            Boolean fin = false;

            String menu = "../data/portada.txt";
            String const1;
            String menu1 = "../data/over.txt";
            String const2;
            String menu2 = "../data/GG.txt";
            String const3;
            StreamReader archivo;
            StreamReader archivo1;
            StreamReader archivo2;

            miTablero = new Tablero(300, 300);
            player = new Jugador(150, 150, miTablero);

            estado = Titulo;
            gm = GG;

            do {


                //Controlador de pantalla del juego
                switch (estado)
                {

                    //Pantalla de titulo
                    case Titulo:


                        //archivo = new StreamReader("..\\data\\portada.txt");

                        // archivo = new StreamReader("../../../data/portada.txt");
                        //Console.WriteLine(archivo.ReadToEnd());

                        archivo = new StreamReader(menu);
                        const1 = archivo.ReadToEnd();

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(const1);


                        Console.SetCursorPosition(15, 20);
                        Console.Write("Presione Enter para jugar 3:)");

                        tecla = Console.ReadKey(true);
                        if (tecla.Key == ConsoleKey.Enter)
                        {
                            estado = Juego;
                        }

                        Console.Clear();

                        break;

                    //Pantalla de juego
                    case Juego:
                        estado = Juego;

                        // Genera mapa
                        miTablero.RandomWalk(5000);
                        miTablero.putItems(150);
                        miTablero.putTrampas(100);

                        do
                        {
                            // Limpia el buffer de teclado
                            while (Console.KeyAvailable == true)
                            {
                                Console.ReadKey(true);
                            }

                            Console.CursorVisible = false;

                            miTablero.Display(player.x, player.y);

                            player.Display();

                            // info del jugador
                            Console.SetCursorPosition(42, 0);
                            Console.Write("Level " + level);
                            Console.SetCursorPosition(42, 1);
                            Console.Write("X:" + player.x + " Y:" + player.y + " ");
                            Console.SetCursorPosition(52, 0);
                            Console.Write("Live " + Jugador.live + " ");
                            Console.SetCursorPosition(52, 2);
                            Console.Write("Money " + Jugador.money);
                            Console.SetCursorPosition(52, 3);
                            Console.Write("Llaves " + Jugador.llave);

                            // Brujula
                            Console.SetCursorPosition(42, 3);
                            if (player.x > miTablero.endX) Console.Write("<  ");
                            else if (player.x < miTablero.endX) Console.Write("  >");
                            else Console.Write("   ");

                            Console.SetCursorPosition(43, 2);
                            if (player.y > miTablero.endY)
                            {
                                Console.Write("^");
                            }
                            else
                            {
                                Console.Write(" ");
                            }


                            Console.SetCursorPosition(43, 4);
                            if (player.y < miTablero.endY)
                            {
                                Console.Write("v");
                            }
                            else
                            {
                                Console.Write(" ");
                            }


                            tecla = Console.ReadKey(true);

                            switch (tecla.Key)
                            {

                                case ConsoleKey.UpArrow:
                                    player.MoveUp();
                                    break;

                                case ConsoleKey.DownArrow:
                                    player.MoveDown();
                                    break;

                                case ConsoleKey.LeftArrow:
                                    player.MoveLeft();
                                    break;

                                case ConsoleKey.RightArrow:
                                    player.MoveRight();
                                    break;
                                case ConsoleKey.Spacebar:
                                    player.UseItem();
                                    break;
                                case ConsoleKey.W:
                                    player.selectPreviousItem();
                                    break;
                                case ConsoleKey.S:
                                    player.selectNextItem();
                                    break;


                            }

                         

                            player.CogeItem();
                            player.kill();

                            // Has llegado?
                            if (player.x == miTablero.endX && player.y == miTablero.endY)
                            {
                                player.x = miTablero.anchura / 2;
                                player.y = miTablero.altura / 2;
                                miTablero.RandomWalk(5000);

                                level++;
                            }
                           
                            if (Jugador.live == 0) //muere
                            {
                                estado = Game_Over;
                                gm = estado;
                            }
                            else if (Jugador.llave == 1) { //gana
                                estado = GG;
                                gm = estado;
                            }

                        } while (estado != gm);

                        Console.Clear();

                        break;

                        //Pantalla de Good Game
                    case GG:
                        estado = GG;

                        archivo2 = new StreamReader(menu2);
                        const3 = archivo2.ReadToEnd();

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(const3);

                        fin = true;
                        break;

                    //Pantalla de game over
                    case Game_Over:
                        estado = Game_Over;

                        archivo1 = new StreamReader(menu1);
                        const2 = archivo1.ReadToEnd();

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(const2);

                        fin = true;
                        break;

                   

                }
            } while (fin == false);

            Console.ReadKey(true);


        }
    }

}
