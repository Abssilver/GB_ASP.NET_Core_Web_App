using System;

namespace SnailTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var snail = new Snail(15, 4);
            snail.PrintSnail();
            Console.ReadLine();
        }


        public class Snail
        {
            private int[,] _snail;
            public Snail(int width, int height)
            {
                if (width <= 0 || height <= 0)
                {
                    throw new ArgumentException("Invalid parameters");
                }
                _snail = BuildSnail(width, height);
            }
            
            private int[,] BuildSnail(int width, int height)
            {
                var snail = new int[width, height];
                var dirCounter = new DirectionCounter();
                var position = new Point();
                var points = width * height;
                
                snail[0, 0] = 1;
                dirCounter.ItemsBeforeChange = width - 1;

                for (var i = 1; i < points; i++)
                {
                    if (dirCounter.ItemsBeforeChange <= 0)
                    {
                        ChangeDirection(dirCounter, width, height); 
                    }
                    ApplyDirection(position, dirCounter);
                    snail[position.X, position.Y] = i + 1;
                    dirCounter.ItemsBeforeChange--;
                }

                return snail;
            }
            
            private void ApplyDirection(Point position, DirectionCounter directionCounter)
            {
                switch (directionCounter.Current %= 4)
                {
                    case 0:
                        position.X += 1;
                        break;
                    case 1:
                        position.Y += 1;
                        break;
                    case 2:
                        position.X -= 1;
                        break;
                    default:
                        position.Y -= 1;
                        break;
                }
            }
            
            private void ChangeDirection(DirectionCounter directionCounter, int width, int height)
            {
                directionCounter.Current++;
                switch (directionCounter.Current %= 4)
                {
                    case 0:
                        directionCounter.Left++;
                        directionCounter.ItemsBeforeChange = width - directionCounter.Left - directionCounter.Right;
                        break;
                    case 1:
                        directionCounter.Top++;
                        directionCounter.ItemsBeforeChange = height - directionCounter.Top - directionCounter.Bottom;
                        break;
                    case 2:
                        directionCounter.Right++;
                        directionCounter.ItemsBeforeChange = width - directionCounter.Left - directionCounter.Right;
                        break;
                    default:
                        directionCounter.Bottom++;
                        directionCounter.ItemsBeforeChange = height - directionCounter.Top - directionCounter.Bottom;
                        break;
                }
            }
        
            public void PrintSnail()
            {
                for (int i = 0; i < _snail.GetLength(1); i++)
                {
                    for (int j = 0; j < _snail.GetLength(0); j++)
                    {
                        Console.Write($"{_snail[j,i], 5}");
                    }
                    Console.WriteLine();
                }
            }
            
            private class DirectionCounter
            {
                public int Right;
                public int Bottom;
                public int Left;
                public int Top;
                public int Current;
                public int ItemsBeforeChange;
            }
            
            private class Point
            {
                public int X { get; set; }
                public int Y { get; set; }
            }
        }
    }
}