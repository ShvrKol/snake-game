using System;
using System.Threading;

namespace Project_X_bergerak
{
    class Program
    {
        static void Main(string[] args)
        {
            #region variable
            int makananX = 10;
            int makananY = 10;
            int[] X = new int[1624];
            X[0] = 30;
            int[] Y = new int[1624];
            Y[0] = 15;
            int kecepatan = 150;
            int jumlahkemakan = 0;
            bool mulai = true;
            bool nabrak = false;
            bool makanankemakan = false;
            Random random = new Random();
            Console.CursorVisible = false;
            #endregion

            //membuat welcome screen

            #region Setup Game
            //munculin makanan
            setposisimakanandilayar(random, out makananX, out makananY);
            munculmakanan(makananX, makananY);

            //munculin ular

            munculinular(jumlahkemakan, X, Y, out X, out Y);

            //munculin arena
            Arena();
            ConsoleKey command = Console.ReadKey().Key;
            ConsoleKey commandSebelum = command;
            #endregion

            //control Ular
            do
            {
                #region Control Arah
                switch (command)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(X[0], Y[0]);
                        Console.Write(" ");
                        X[0]--;
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(X[0], Y[0]);
                        Console.Write(" ");
                        X[0]++;
                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(X[0], Y[0]);
                        Console.Write(" ");
                        Y[0]--;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(X[0], Y[0]);
                        Console.Write(" ");
                        Y[0]++;
                        break;
                }
                #endregion
                #region Game Playing
                // munculkan badan ular
                munculinular(jumlahkemakan, X, Y, out X, out Y);

                //detect nabrak atau gak
                nabrak = benerannabrak(X[0], Y[0]);
                if (nabrak == true)
                {
                    mulai = false;
                    Console.Clear();
                    Console.SetCursorPosition(30, 15);
                    Console.Write("Cie nabrak nie X_X");
                }
                //check saat makanan kemakan
                makanankemakan = menentukanmakanandimakan(X[0], Y[0], makananX, makananY);

                //naruh makanan di arena (random)
                if (makanankemakan == true)
                {
                    setposisimakanandilayar(random, out makananX, out makananY);
                    munculmakanan(makananX, makananY);
                    //menghitung jumlah score
                    jumlahkemakan++;
                }
                if (Console.KeyAvailable)
                {
                    commandSebelum = command;
                    command = Console.ReadKey().Key;
                    if (command == ConsoleKey.LeftArrow && commandSebelum == ConsoleKey.RightArrow) command = commandSebelum;
                    if (command == ConsoleKey.RightArrow && commandSebelum == ConsoleKey.LeftArrow) command = commandSebelum;
                    if (command == ConsoleKey.UpArrow && commandSebelum == ConsoleKey.DownArrow) command = commandSebelum;
                    if (command == ConsoleKey.DownArrow && commandSebelum == ConsoleKey.UpArrow) command = commandSebelum;
                }
                System.Threading.Thread.Sleep(kecepatan);
                #endregion 
            } while (mulai);
        }
        #region Methods
        public static void Arena()
        {
            for (int A = 0; A < 30; A++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, A);
                Console.Write("█");
                Console.SetCursorPosition(59, A);
                Console.Write("█");
            }
            for (int B = 0; B < 60; B++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(B, 0);
                Console.Write("█");
                Console.SetCursorPosition(B, 29);
                Console.Write("█");
            }

        }//set arena map
        public static void munculinular(int jumlahkemakan, int[] xmasuk, int[] ymasuk, out int[] xkeluar, out int[] ykeluar)
        {
            //mewarnai kepala
            Console.SetCursorPosition(xmasuk[0], ymasuk[0]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("X");

            //mewarnai badan
            for (int i = 1; i < jumlahkemakan + 1; i++)
            {
                Console.SetCursorPosition(xmasuk[i], ymasuk[i]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("#");
            }

            //menghapus bagian terakhir ular
            Console.SetCursorPosition(xmasuk[jumlahkemakan + 1], ymasuk[jumlahkemakan + 1]);
            Console.Write(" ");

            //rekam semua lokasi dari setiap bagian ular
            for (int i = jumlahkemakan + 1; i > 0; i--)
            {
                xmasuk[i] = xmasuk[i - 1];
                ymasuk[i] = ymasuk[i - 1];
            }
            //mengembalikan array yang baru
            xkeluar = xmasuk;
            ykeluar = ymasuk;

        }//spawn ular
        public static void setposisimakanandilayar(Random random, out int makananX, out int makananY)
        {
            makananX = random.Next(1, 58);
            makananY = random.Next(1, 28);
        }//spawn makanan random
        public static bool menentukanmakanandimakan(int x, int y, int makananX, int makananY)
        {
            if (x == makananX && y == makananY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }//apakah makanan dimakan ular
        public static bool benerannabrak(int X, int Y)
        {
            if (X == 0 || X == 59 || Y == 0 || Y == 29) return true; return false;
        }//apatabrakan dengan tembok
        public static void munculmakanan(int makananX, int makananY)
        {
            Console.SetCursorPosition(makananX, makananY);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("O");
        }//spawn makanan awal
        #endregion 
    }
}