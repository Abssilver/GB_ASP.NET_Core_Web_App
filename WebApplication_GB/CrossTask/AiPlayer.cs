namespace CrossTask
{
    internal sealed class AiPlayer : IPlayer
    {
        private readonly IBotBehavior _behaviour;
        public string Name { get; }
        public ISign Sign { get; }

        public AiPlayer(
            ISign playerSign,
            IBotBehavior behaviour,
            string playerName)
        {
            Sign = playerSign;
            Name = playerName;
            _behaviour = behaviour;

        }

        public Position MakeTurn()
        {
            return _behaviour.GetPosition();
        }
    }
}