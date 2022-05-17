namespace CrossTask
{
    public interface ISign
    {
        int Value { get; }
        char Visual { get; }
    }

    internal interface IPlayer: ITurnMaker
    {
        public string Name { get; }
        public ISign Sign { get; }
    }

    internal interface ITurnMaker
    {
        Position ImplementTurn();
    }

    internal sealed class NoneSign : ISign
    {
        public int Value { get; } = 0;
        public char Visual { get; } = '.';
    }

    internal sealed class Circle : ISign
    {
        public int Value { get; } = 1;
        public char Visual { get; } = 'O';
    }

    internal sealed class Cross : ISign
    {
        public int Value { get; } = 2;
        public char Visual { get; } = 'X'; 
    }
}