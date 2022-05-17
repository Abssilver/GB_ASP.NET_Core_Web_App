using System.Collections.Generic;

namespace CrossTask
{
    internal sealed class Game
    {
        private readonly Board _board;
        private readonly WinChecker _winChecker;
        private readonly IDrawService _drawService;
        private readonly IList<IPlayer> _players;

        private bool _isGameWon;
        private int _turnCounter;
        
        public bool IsAbleToPlay => !_isGameWon && _winChecker.IsAbleToMove();
        
        public Game(IList<IPlayer> players, ISign none, Board board, int winStreak, IDrawService drawService)
        {
            _board = board;
            _drawService = drawService;
            _players = players;
            _winChecker = new WinChecker(_board, none, winStreak);
        }

        public void NewGame()
        {
            _board.ResetBoard();
            _turnCounter = 0;
            _isGameWon = false;
        }

        public void ProcessGame()
        {
            var playerIndex = _turnCounter % _players.Count;
            var turnMaker = _players[playerIndex];
            _drawService.DrawNextTurn(_board.GameBoard, turnMaker.Name, _turnCounter);
            
            var position = turnMaker.ImplementTurn();
            
            _isGameWon = _winChecker.IsWin(turnMaker.Sign.Value);
            
            if (!_isGameWon)
                _turnCounter++;
        }

        public void ShowResults()
        {
            if (_isGameWon)
            {
                var playerIndex = _turnCounter % _players.Count;
                var turnMaker = _players[playerIndex];
                _drawService.DrawWinner(_board.GameBoard, turnMaker.Name);
            }
            else
            {
                _drawService.DrawNoWinner(_board.GameBoard);
            }
        }
    }
}