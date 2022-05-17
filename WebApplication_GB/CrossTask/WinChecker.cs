namespace CrossTask
{
    internal sealed class WinChecker
    {
        private readonly int[,] _board;
        private readonly int _winStreak;
        private readonly ISign _noneSign;

        public WinChecker(Board board, ISign noneSign, int winStreak)
        {
            _board = board.GameBoard;
            _noneSign = noneSign;
            _winStreak = winStreak;
        }

        public bool IsWin(int side)
        {
            var rowSize = _board.GetLength(0);
            var columnSize = _board.GetLength(1);
            
            if (IsLineWin(side, columnSize, rowSize))
                return true;

            if (IsLineWin(side, rowSize, columnSize))
                return true;
            
            if (IsDiagonalWinTopLeftToBotRight(side, rowSize, columnSize))
                return true;
            
            if (IsDiagonalWinBotLeftToTopRight(side, rowSize, columnSize))
                return true;
            
            return false;
        }
        
        private bool IsLineWin(int sideValue, int searchThrough, int searchIn)
        {
            for (var i = 0; i < searchThrough; i++)
            {
                var currentStreak = 0;
                for (var j = 0; j < searchIn; j++)
                {
                    currentStreak = _board[i, j] == sideValue ? currentStreak + 1 : 0;
                    if (currentStreak == _winStreak)
                        return true;
                    
                    if (searchIn - j < _winStreak - currentStreak) break;
                }
            }
            
            return false;
        }
        
        
        private bool IsDiagonalWinTopLeftToBotRight(int sideValue, int rowSize, int columnSize)
        {
            for (var i = 0 ; i < rowSize - _winStreak + 1; i++)
            {
                var currentStreak = 0;
                var range = columnSize - i;
                for (var j = 0; j < range; j++)
                {
                    currentStreak = _board[j + i, j] == sideValue ? currentStreak + 1 : 0;
                    if (currentStreak == _winStreak)
                        return true;
                    
                    if (range - j < _winStreak - currentStreak) break;
                }
            }
            
            for (var i = 1 ; i < columnSize - _winStreak + 1; i++)
            {
                var currentStreak = 0;
                var range = rowSize - i;
                for (var j = 0; j < range; j++)
                {
                    currentStreak = _board[j, j + i] == sideValue ? currentStreak + 1 : 0;
                    if (currentStreak == _winStreak)
                        return true;
                    
                    if (range - j < _winStreak - currentStreak) break;
                }
            }

            return false;
        }
        
        private bool IsDiagonalWinBotLeftToTopRight(int sideValue, int rowSize, int columnSize)
        {
            for (var i = 0 ; i < rowSize - _winStreak + 1; i++)
            {
                var currentStreak = 0;
                var range = columnSize - i;
                for (var j = 0; j < range; j++)
                {
                    currentStreak = _board[columnSize - j - 1, j + i] == sideValue ? currentStreak + 1 : 0;
                    if (currentStreak == _winStreak)
                        return true;
                    
                    if (range - j < _winStreak - currentStreak) break;
                }
            }
            
            for (var i = _winStreak - 1; i < columnSize - 1; i++)
            {
                var currentStreak = 0;
                var range = i;
                for (var j = 0; j < range; j++)
                {
                    currentStreak = _board[j, i - j] == sideValue ? currentStreak + 1 : 0;
                    if (currentStreak == _winStreak)
                        return true;
                    
                    if (range - j < _winStreak - currentStreak) break;
                }
            }

            return false;
        }
        
        public bool IsAbleToMove()
        {
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    if (_board[i, j] == _noneSign.Value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}