using System;
using System.Collections.Generic;

namespace CrossTask
{
    internal sealed class DefenderAiBehavior: IBotBehavior, IDisposable
    {
        private readonly Board _board;
        private readonly ISign _noneSign;
        private readonly ISign _aiSign;
        private readonly Random _rand;
        private readonly ILogger _logger;

        private readonly Dictionary<int, List<MoveNode>> _playersRecords;
        private readonly int _winStreak;

        public DefenderAiBehavior(
            ISign playerSign, 
            ISign noneSign, 
            Board board,
            int winStreak,
            ILogger logger) 
        {
            _board = board;
            _rand = new Random();
            _logger = logger;
            _noneSign = noneSign;
            _aiSign = playerSign;
            _winStreak = winStreak;
            _playersRecords = new Dictionary<int, List<MoveNode>>();
            _board.OnTurnMade += TurnHandler;
            _board.OnBoardReset += ResetBoardHandler;
        }
        
        public void Dispose()
        {
            _board.OnTurnMade -= TurnHandler;
            _board.OnBoardReset -= ResetBoardHandler;
        }
        
        public Position GetPosition()
        {
            return _playersRecords.Count == 0
                ? GetRandom()
                : FindPosition();
        }
        
        private void TurnHandler(Position position, int side)
        {
            if (_playersRecords.TryGetValue(side, out var moves))
            {
                var move = new MoveNode(side, position, moves);
                moves.Add(move);
            }
            else
            {
                _playersRecords[side] = new List<Position> { position };
            }
        }

        private void ResetBoardHandler()
        {
            _playersRecords.Clear();  
        }



        private Position FindPosition()
        {
            var result = new Position
            {
                Row = anchorPosition.Row,
                Column = anchorPosition.Column,
            };

            if (IsEmpty(result))
                return result;
            
            
            

            _logger.Log("Ai can't find the position to make a turn. Search limit is exceeded");
            throw new Exception("Some logic error. Ai can't find the position to make a turn");
        }

        private Position TryToFindWinPosition()
        {
            if (_playersRecords.TryGetValue(_aiSign.Value, out var moves))
            {
                if (moves.Count >= _winStreak - 1)
                {
                    return GetPositionOnSelfMovesAnalysis(moves);
                }
            }

            return null;
        }

        private Position GetPositionOnSelfMovesAnalysis(List<Position> moves)
        {
            
            
        }

        private bool IsInBorder(int row, int column)
        {
            var board = _board.GameBoard;
            var isRowValid = row >= 0 && row < board.GetLength(0);
            var isColumnValid = column >= 0 && column < board.GetLength(1);
            return isRowValid && isColumnValid;
        }

        private Position GetRandom()
        {
            var result = new Position();
            var board = _board.GameBoard;
            var randomRowIndex = _rand.Next(0, board.GetLength(0));
            var randomColumnIndex = _rand.Next(0, board.GetLength(1));
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