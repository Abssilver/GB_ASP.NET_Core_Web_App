using System;

namespace CrossTask
{
    internal class CrossGame
    {
        // 3. Определяем размеры массива
        static int SIZE_X = 3;
        static int SIZE_Y = 3;
        static int WinStreak = 4;

        // 1. Создаем двумерный массив
        static char[,] field = new char[SIZE_Y, SIZE_X];

        // 2. Обозначаем кто будет ходить какими фишками
        static char PLAYER_DOT = 'X';
        static char AI_DOT = '0';
        static char EMPTY_DOT = '.';

        // 12. Создаем рандом
        private static readonly Random Rand = new Random();

        // 4. Заполняем на массив
        private static void InitField()
        {
            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    field[i, j] = EMPTY_DOT;
                }
            }
        }


        // 5. Выводим на массив на печать
        private static void PrintField()
        {
            //6. украшаем картинку
            Console.WriteLine("-------");
            for (int i = 0; i < SIZE_Y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < SIZE_X; j++)
                {
                    Console.Write(field[i, j] + "|");
                }
                Console.WriteLine();
            }
            //6. украшаем картинку
            Console.WriteLine("-------");
        }

        // 10. Проверяем возможен ли ход
        private static bool IsCellValid(int y, int x)
        {
            // если вываливаемся за пределы возвращаем false
            if (x < 0 || y < 0 || x > SIZE_X - 1 || y > SIZE_Y - 1)
            {
                return false;
            }
            // если не путое поле тоже false
            return (field[y, x] == EMPTY_DOT);
        }

        // 7. Метод который устанавливает символ
        private static void setSym(int y, int x, char sym)
        {
            field[y, x] = sym;
        }

        // 9. Ход игрока
        private static void PlayerStep()
        {
            // 11. с проверкой
            int x;
            int y;
            do
            {
                Console.WriteLine("Введите координаты: X Y (1-3)");
                x = Int32.Parse(Console.ReadLine()) - 1;
                y = Int32.Parse(Console.ReadLine()) - 1;
            } while (!IsCellValid(y, x));
            setSym(y, x, PLAYER_DOT);
        }


        // 13. Ход ПК
        private static void AiStep()
        {
            int x;
            int y;
            do
            {
                x = Rand.Next(0, SIZE_X);
                y = Rand.Next(0, SIZE_Y);
            } while (!IsCellValid(y, x));
            setSym(y, x, AI_DOT);
        }

        public enum Side
        {
            Cross = 0,
            Circle = 1,
        }

        // 14. Проверка победы
       


        // 16. Проверка полное ли поле? возможно ли ходить?
        private static bool IsFieldFull()
        {
            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    if (field[i, j] == EMPTY_DOT)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static void Play()
        {
            // 1 - 1 иницируем и выводим на печать
            InitField();
            PrintField();
            // 1 - 1 иницируем и выводим на печать

            // 15 Основной ход программы

            while (true)
            {
                PlayerStep();
                PrintField();
                /*if (IsLineWin(PLAYER_DOT))
                {
                    Console.WriteLine("Player WIN!");
                    break;
                }
                if (IsFieldFull())
                {
                    Console.WriteLine("DRAW");
                    break;
                }

                AiStep();
                PrintField();
                if (CheckWin(AI_DOT))
                {
                    Console.WriteLine("Win SkyNet!");
                    break;
                }*/
                if (IsFieldFull())
                {
                    Console.WriteLine("DRAW");
                    break;
                }
            }
        }
    }
}