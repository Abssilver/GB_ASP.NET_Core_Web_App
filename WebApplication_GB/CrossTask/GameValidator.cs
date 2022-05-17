using System;

namespace CrossTask
{
    internal interface IValidator
    {
        ValidationResult<Position> IsInputValid (string playerInput);
        ValidationResult<bool> IsAbleToPickCell(Position position);
    }

    public class ValidationResult<TResult>
    {
        public TResult Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }

    internal sealed class GameValidator: IValidator
    {
        private readonly int[,] _board;
        private readonly ISign _noneSign;
        
        public GameValidator(Board board, ISign noneSign)
        {
            _board = board.GameBoard;
            _noneSign = noneSign;
        }
        
        public ValidationResult<Position> IsInputValid(string playerInput)
        {
            var result = new ValidationResult<Position>();
            var values = playerInput.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != 2)
            {
                result.IsSuccess = false;
                result.Error = "Invalid input. The number of parameters is greater than 2";
                return result;
            }

            if (!int.TryParse(values[0], out var columns))
            {
                result.IsSuccess = false;
                result.Error = "Invalid input. X is not a number";
                return result;
            }

            if (!int.TryParse(values[1], out var rows))
            {
                result.IsSuccess = false;
                result.Error = "Invalid input. Y is not a number";
                return result;
            }

            if (rows < 0)
            {
                result.IsSuccess = false;
                result.Error = "Invalid input. Y is negative number";
                return result; 
            }
            
            if (columns < 0)
            {
                result.IsSuccess = false;
                result.Error = "Invalid input. X is negative number";
                return result; 
            }
            
            if (rows >= _board.GetLength(0))
            {
                result.IsSuccess = false;
                result.Error = "Invalid input. Y is greater than field size";
                return result; 
            }
            
            if (columns >= _board.GetLength(1))
            {
                result.IsSuccess = false;
                result.Error = "Invalid input. X is greater than field size";
                return result; 
            }

            result.Result = new Position { Row = rows, Column = columns };
            result.IsSuccess = true;
            return result;
        }

        public ValidationResult<bool> IsAbleToPickCell(Position position)
        {
            var isValid = _board[position.Row, position.Column] == _noneSign.Value;
            var result = new ValidationResult<bool>
            {
                Result = isValid,
                IsSuccess = isValid,
                Error = isValid 
                    ? string.Empty 
                    : $"Unable to pick cell. Cell is not empty ({position.Column}, {position.Row})",
            };
            
            return result;
        }
    }
}