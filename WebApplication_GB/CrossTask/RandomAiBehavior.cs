using System;

namespace CrossTask
{
    internal sealed class RandomAiBehavior: IBotBehavior
    {
        private readonly Board _board;
        private readonly ISign _noneSign;
        private readonly Random _rand;
        private readonly ILogger _logger;
        
        public RandomAiBehavior(Board board, ILogger logger, ISign noneSign)
        {
            _board = board;
            _rand = new Random();
            _logger = logger;
            _noneSign = noneSign;
        }

        public Position GetPosition()
        {
            var board = _board.GameBoard;
            var randomRowIndex = _rand.Next(0, board.GetLength(0));
            var randomColumnIndex = _rand.Next(0, board.GetLength(1));
            
            var result = new Position
            {
                Row = randomRowIndex,
                Column = randomColumnIndex,
            };

            if (IsEmpty(result))
                return result;
            
            for (int i = randomRowIndex; i < board.GetLength(0); i++)
            {
                for (int j = randomColumnIndex; j < board.GetLength(1); j++)
                {
                    result.Row = i;
                    result.Column = j;
                    if (IsEmpty(result))
                        return result;
                }
            }

            for (int i = 0; i < randomRowIndex; i++)
            {
                for (int j = 0; j < randomColumnIndex; j++)
                {
                    result.Row = i;
                    result.Column = j;
                    if (IsEmpty(result))
                        return result;
                }
            }

            _logger.Log("Ai can't find the position to make a turn. Search limit is exceeded");
            throw new Exception("Some logic error. Ai can't find the position to make a turn");
        }
        
        private bool IsEmpty(Position pos)
        {
            return _board.GameBoard[pos.Row, pos.Column] == _noneSign.Value;
        }
    }
}