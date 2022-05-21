
using System;

namespace CrossTask
{
    internal sealed class Board
    {
        private readonly int[,] _board;
        private readonly ISign _noneSign;
        public int[,] GameBoard => _board;
        
        public event Action<Position, int> OnTurnMade;
        public event Action OnBoardReset;
        public Board(int rowSize, int columnSize, ISign noneSign)
        {
            _board = new int[rowSize, columnSize];
            _noneSign = noneSign;
        }

        public void ResetBoard()
        {
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    _board[i, j] = _noneSign.Value;
                }
            }
            OnBoardReset?.Invoke();
        }

        public void ImplementTurn(Position pos, int side)
        {
            //TODO: implement commands
            _board[pos.Row, pos.Column] = side;
            OnTurnMade?.Invoke(pos, side);
        }

        public bool IsEmpty(Position pos)
        {
            return GameBoard[pos.Row, pos.Column] == _noneSign.Value;
        }
        
        public bool IsInBoard(Position pos)
        {
            var isRowValid = pos.Row > 0 && pos.Row < GameBoard.GetLength(0);
            var isColumnValid = pos.Column > 0 && pos.Column < GameBoard.GetLength(1);
            return isRowValid && isColumnValid;
        }
    }
}