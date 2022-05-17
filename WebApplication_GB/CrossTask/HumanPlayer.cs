using System;

namespace CrossTask
{
    internal sealed class HumanPlayer : IPlayer
    {
        private readonly IValidator _inputValidator;
        private readonly ILogger _logger;
        public string Name { get; }
        public ISign Sign { get; }

        public HumanPlayer(
            ISign playerSign, 
            string playerName, 
            IValidator inputValidator,
            ILogger logger)
        {
            Sign = playerSign;
            Name = playerName;
            _inputValidator = inputValidator;
            _logger = logger;
        }

        public Position ImplementTurn()
        {
            Console.WriteLine("Enter the coordinates: X Y (example: 1 3)");
            Console.WriteLine("Use Space Key as separator");
            string playerInput;
            ValidationResult<Position> parseResult;
            ValidationResult<bool> pickerResult;
            do
            {
                pickerResult = null;
                
                playerInput = Console.ReadLine();
                parseResult = _inputValidator.IsInputValid(playerInput);
                if (!parseResult.IsSuccess)
                {
                   _logger.Log(parseResult.Error);
                   continue;
                }

                pickerResult = _inputValidator.IsAbleToPickCell(parseResult.Result);
                if (!pickerResult.IsSuccess)
                {
                    _logger.Log(pickerResult.Error);
                }
                
            } while (!parseResult.IsSuccess || pickerResult is null || !pickerResult.IsSuccess);

            return parseResult.Result;
        }
    }
}