using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossTask
{
    public interface IDrawService
    {
        public void DrawNextTurn(int [,] board, string turnMaker, int turnNumber);
        public void DrawWinner(int[,] board, string winner);
        public void DrawNoWinner(int[,] board);
    }

    internal sealed class ConsoleDrawer : IDrawService
    {
        private readonly IList<ISign> _signs = new List<ISign>();

        public ConsoleDrawer(ISign noneSign, IEnumerable<ISign> players)
        {
            _signs.Add(noneSign);
            foreach (var player in players)
            {
                _signs.Add(player);
            }
        }
        
        public void DrawNextTurn(int[,] board, string turnMaker, int turnNumber)
        {
            Console.WriteLine($"----{turnMaker} Turn----");
            Console.WriteLine($"Game Turn: {turnNumber}");
            DrawBoard(board);
        }

        public void DrawWinner(int[,] board, string winner)
        {
            var rowSize = board.GetLength(0);
            var separator = new string('-', rowSize);
            Console.WriteLine(separator);
            Console.WriteLine($"{winner} is Won");
            DrawBoard(board);
            Console.WriteLine(separator);
        }
        
        public void DrawNoWinner(int[,] board)
        {
            var rowSize = board.GetLength(0);
            var separator = new string('-', rowSize);
            Console.WriteLine(separator);
            Console.WriteLine("It's а Draw! Amazing!");
            DrawBoard(board);
            Console.WriteLine(separator);
        }

        private void DrawBoard(int[,] board)
        {
            Console.WriteLine();
            for (var i = 0; i < board.GetLength(1); i++)
            {
                Console.Write("|");
                for (var j = 0; j < board.GetLength(0); j++)
                {
                    Console.Write(_signs.FirstOrDefault(x => x.Value == board[i, j])?.Visual + "|");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}