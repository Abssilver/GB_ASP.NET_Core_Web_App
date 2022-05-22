using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossTask
{
    internal interface IAiBrain
    {
        Position GetRandom();
        Position GetWinPosition(int side);
        Position BuildSequence(int side);
        bool IsNeedToDefend(int side, out MoveSequence sequenceToDefend);
        Position Defend(MoveSequence sequenceToDefend);
    }

    internal sealed class AiBrain: IAiBrain, IDisposable
    {
        private readonly Board _board;
        private readonly ILogger _logger;
        
        private readonly int _winStreak;
        private readonly Random _rand;

        private readonly Dictionary<int, List<MoveNode>> _playersRecords;
        private readonly Dictionary<int, List<MoveSequence>> _playersSequences;

        public AiBrain(
            Board board,
            ILogger logger,
            int winStreak)
        {
            _board = board;
            _rand = new Random();
            _winStreak = winStreak;
            _logger = logger;
            _playersRecords = new Dictionary<int, List<MoveNode>>();
            _playersSequences = new Dictionary<int, List<MoveSequence>>();
            
            _board.OnTurnMade += TurnHandler;
            _board.OnBoardReset += ResetBoardHandler;
        }

        public void Dispose()
        {
            _board.OnTurnMade -= TurnHandler;
            _board.OnBoardReset -= ResetBoardHandler;
        }

        public Position GetRandom()
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
                    if (_board.IsEmpty(result))
                        return result;
                }
            }

            for (int i = 0; i < randomRowIndex; i++)
            {
                for (int j = 0; j < randomColumnIndex; j++)
                {
                    result.Row = i;
                    result.Column = j;
                    if (_board.IsEmpty(result))
                        return result;
                }
            }
            _logger.Log("Ai can't find the position to make a turn. Search limit is exceeded");
            throw new Exception("Some logic error. Ai can't find the position to make a turn");
        }
        
        public Position GetWinPosition(int side)
        {
            if (!_playersSequences.TryGetValue(side, out var sequences)) 
                return null;
            
            var almostWin =
                sequences.Find(x => x.Sequence.Count == _winStreak - 1 && x.AvailablePositions.Count > 0);
            return almostWin?.GetRandomAvailablePosition();
        }

        public Position BuildSequence(int side)
        {
            if (!_playersSequences.TryGetValue(side, out var sequences)) 
                return null;
            
            var prioritySequences =
                sequences.FindAll(x => x.AvailablePositions.Count > 0).OrderBy(x => x.Sequence.Count).ToArray();
            
            if (prioritySequences.Length <= 0) 
                return null;
            
            var mostPriority = prioritySequences[^1];
            return mostPriority.GetRandomAvailablePosition();
        }

        public bool IsNeedToDefend(int side, out MoveSequence sequenceToDefend)
        {
            sequenceToDefend = null;
            foreach (var key in _playersSequences.Keys)
            {
                if (key == side)
                    continue;

                sequenceToDefend = _playersSequences[key].Find(IsDefendCondition);
                if (sequenceToDefend != null)
                    return true;
            }

            return false;
        }

        public Position Defend(MoveSequence sequenceToDefend)
        {
            return sequenceToDefend.GetRandomAvailablePosition();
        }

        private bool IsDefendCondition(MoveSequence sequence)
        {
            var isTwoTurnToWin = _winStreak - sequence.Sequence.Count < 3 && sequence.AvailablePositions.Count > 1;
            var isOneTurnToWin = _winStreak - sequence.Sequence.Count < 2 && sequence.AvailablePositions.Count != 0;
            return isTwoTurnToWin || isOneTurnToWin;
        }

        private void FillSequenceList(int side, List<MoveSequence> sequences)
        {
            sequences.Clear();

            if (_playersRecords.TryGetValue(side, out var moves))
            {
                var result = new List<MoveNode>();
                var current = new List<MoveNode>();
                moves.Sort(MoveNodeSortComparison.CompareMoveNodesByPosition);
                foreach (var move in moves)
                {
                    HandleMove(move, current, result, sequences, m => m.Core.MidRight != null, m => m.Core.MidRight);
                    HandleMove(move, current, result, sequences, m => m.Core.BotRight != null, m => m.Core.BotRight);
                    HandleMove(move, current, result, sequences, m => m.Core.Bot != null, m => m.Core.Bot);
                }
            }
        }

        private void HandleMove(
            MoveNode move, 
            List<MoveNode> helpList, 
            List<MoveNode> resultList, 
            List<MoveSequence> sequences,
            Predicate<MoveNode> searchPredicate, 
            Func<MoveNode, MoveNode> searchDirection)
        {
            var sequence = new MoveSequence(_board, _rand);
            helpList.Clear();
            helpList.Add(move);
            sequence.AddMoveToSequence(move);
            while (searchPredicate.Invoke(move))
            {
                move = searchDirection.Invoke(move);
                helpList.Add(move);
                sequence.AddMoveToSequence(move);
            }

            if (resultList.Count < helpList.Count)
            {
                resultList.Clear();
                helpList.ForEach(resultList.Add);
            }
            
            sequences.Add(sequence);
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
                _playersRecords[side] = new List<MoveNode> { new (side, position, null) };
            }

            if (!_playersSequences.TryGetValue(side, out var sequences))
            {
                sequences = new List<MoveSequence>();
                _playersSequences[side] = sequences;
            }

            FillSequenceList(side, sequences);

            var enemySequences = _playersSequences
                .Where(x => x.Key != side)
                .SelectMany(x => x.Value)
                .ToArray();
            
            foreach (var sequence in enemySequences)
            {
                sequence.HandleEnemyMove(position);
            }
        }

        private void ResetBoardHandler()
        {
            _playersRecords.Clear();
            _playersSequences.Clear();
        }
    }
}