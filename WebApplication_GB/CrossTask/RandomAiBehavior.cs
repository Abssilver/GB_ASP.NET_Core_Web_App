
namespace CrossTask
{
    internal sealed class RandomAiBehavior: IBotBehavior
    {
        private readonly IAiBrain _brain;
        
        public RandomAiBehavior(IAiBrain brain)
        {
            _brain = brain;
        }

        public Position GetPosition()
        {
            return _brain.GetRandom();
        }
    }
}