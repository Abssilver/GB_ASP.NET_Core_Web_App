using System;
using System.Collections.Generic;

namespace CrossTask
{
    internal class MoveSequence
    {
        private readonly Board _board;
        private readonly List<MoveNode> _sequence;
        private readonly List<Position> _availablePositions;
        private readonly Random _rand;
        
        public IReadOnlyCollection<MoveNode> Sequence => _sequence;
        public IReadOnlyCollection<Position> AvailablePositions => _availablePositions;

        public MoveSequence(Board board, Random random)
        {
            _board = board;
            _sequence = new List<MoveNode>();
            _availablePositions = new List<Position>();
            _rand = random;
        }

        public void Clear()
        {
            _sequence.Clear();
            _availablePositions.Clear();
        }

        public void AddMoveToSequence(MoveNode move)
        {
            _sequence.Add(move);
            if (_sequence.Count == 1)
            {
                if (move.Core.TopLeft is null)
                    HandleEmptyPosition(new Position { Row = move.Position.Row - 1, Column = move.Position.Column - 1 });
                if (move.Core.Top is null)
                    HandleEmptyPosition(new Position { Row = move.Position.Row - 1, Column = move.Position.Column });
                if (move.Core.TopRight is null)
                    HandleEmptyPosition(new Position { Row = move.Position.Row - 1, Column = move.Position.Column + 1 });
                if (move.Core.MidLeft is null)
                    HandleEmptyPosition(new Position { Row = move.Position.Row, Column = move.Position.Column - 1 });
                if (move.Core.MidRight is null)
                    HandleEmptyPosition(new Position { Row = move.Position.Row, Column = move.Position.Column + 1 });
                if (move.Core.BotLeft is null)
                    HandleEmptyPosition(new Position { Row = move.Position.Row + 1, Column = move.Position.Column - 1 });
                if (move.Core.Bot is null)
                    HandleEmptyPosition(new Position { Row = move.Position.Row + 1, Column = move.Position.Column});
                if (move.Core.BotRight is null)
                    HandleEmptyPosition(new Position { Row = move.Position.Row + 1, Column = move.Position.Column + 1});
            }
            else
            {
                _availablePositions.Clear();
                var firstPos = _sequence[0].Position;
                var secondPos = _sequence[1].Position;
                var directionRow = secondPos.Row - firstPos.Row;
                var directionColumn = secondPos.Column - firstPos.Column;
                HandleEmptyPosition(new Position
                    { Row = firstPos.Row - directionRow, Column = firstPos.Column - directionColumn });
                HandleEmptyPosition(new Position
                    { Row = move.Position.Row + directionRow, Column = move.Position.Column + directionColumn });
            }
        }

        public void HandleEnemyMove(Position pos)
        {
            var availablePos = _availablePositions.Find(x => x.Row == pos.Row && x.Column == pos.Column);
            if (availablePos != null)
            {
                _availablePositions.Remove(availablePos);
            }
        }

        public Position GetRandomAvailablePosition()
        {
            var posCount = _availablePositions.Count;
            if (_availablePositions.Count > 0)
            {
                var randomIndex = _rand.Next(0, posCount);
                return _availablePositions[randomIndex];
            }

            return null;
        }

        private void HandleEmptyPosition(Position pos)
        {
            if (_board.IsInBoard(pos) && _board.IsEmpty(pos))
            {
                _availablePositions.Add(pos);
            }
        }
    }
}