using System;
using System.Collections.Generic;

namespace CrossTask
{
    internal sealed class DefenderAiPlayer: DummyAiPlayer, IDisposable
    {
        private readonly Random _rand;
        private readonly ILogger _logger;
        private readonly Dictionary<int, List<Position>> _playersRecords;

        public DefenderAiPlayer(ISign playerSign, ISign noneSign, Board board, string playerName, ILogger logger) 
            : base(playerSign, noneSign, board, playerName, logger)
        {
            _rand = new Random();
            _logger = logger;
            _playersRecords = new Dictionary<int, List<Position>>();
            _board.OnTurnMade += TurnHandler;
        }

        public override Position ImplementTurn()
        {
            var board = _board.GameBoard;
            var randomRowIndex = _rand.Next(0, board.GetLength(0));
            var randomColumnIndex = _rand.Next(0, board.GetLength(1));
            var position = GetAvailablePosition(new Position { Row = randomRowIndex, Column = randomColumnIndex });
            return position;
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
        }

        private void TurnHandler(Position position, int side)
        {
            if (_playersRecords.TryGetValue(side, out var positions))
            {
                positions.Add(position);
            }
            else
            {
                _playersRecords[side] = new List<Position> { position };
            }
        }


        public void Dispose()
        {
            _board.OnTurnMade -= TurnHandler;
        }
    }
}