
namespace CrossTask
{
    internal sealed class DefenderAiBehavior: IBotBehavior
    {
        private readonly ISign _aiSign;
        private readonly IAiBrain _brain;
        
        public DefenderAiBehavior(
            ISign playerSign,
            IAiBrain brain) 
        {
            _aiSign = playerSign;
            _brain = brain;

        }

        public Position GetPosition()
        {
            var side = _aiSign.Value;
            var position = _brain.GetWinPosition(side);
            if (position != null)
            {
                return position;
            }

            if (_brain.IsNeedToDefend(side, out var moveSequence))
            {
                return _brain.Defend(moveSequence);
            }

            position = _brain.BuildSequence(side);
            return position ?? _brain.GetRandom();
        }
    }
}