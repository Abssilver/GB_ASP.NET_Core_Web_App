namespace CrossTask
{
    internal sealed class BotFactory
    {
        private readonly Board _board;
        private readonly ILogger _logger;
        private readonly IAiBrain _brain;
        private readonly int _winStreak;

        private IBotBehavior _randomBehavior = null;
        private IBotBehavior _defenderBehavior = null;

        public BotFactory(Board board, ILogger logger, int winStreak)
        {
            _board = board;
            _logger = logger;
            _winStreak = winStreak;
            _brain = new AiBrain(_board, _logger, winStreak);
        }

        public AiPlayer CreateDummyAi(ISign playerSign, string playerName)
        {
            _randomBehavior ??= new RandomAiBehavior(_brain);
            return new AiPlayer(playerSign, _randomBehavior, playerName);
        }

        public AiPlayer CreateDefenderAi(ISign playerSign, string playerName)
        {
            _defenderBehavior ??= new DefenderAiBehavior(playerSign, _brain);
            return new AiPlayer(playerSign, _defenderBehavior, playerName);
        }
    }
}