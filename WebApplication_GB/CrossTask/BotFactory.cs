namespace CrossTask
{
    internal sealed class BotFactory
    {
        private readonly ISign _noneSign;
        private readonly Board _board;
        private readonly ILogger _logger;

        private IBotBehavior _randomBehavior = null;
        
        public BotFactory(ISign noneSign, Board board, ILogger logger)
        {
            _noneSign = noneSign;
            _board = board;
            _logger = logger;
        }

        public AiPlayer CreateDummyAi(ISign playerSign, string playerName)
        {
            _randomBehavior ??= new RandomAiBehavior(_board, _logger, _noneSign);
            return new AiPlayer(playerSign, _randomBehavior, playerName);
        }
    }
}