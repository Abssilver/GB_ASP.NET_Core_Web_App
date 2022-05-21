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
        Position MakeTurn();
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
    internal sealed class ASign : ISign
    {
        public int Value { get; } = 3;
        public char Visual { get; } = 'A'; 
    }
    internal sealed class ESign : ISign
    {
        public int Value { get; } = 4;
        public char Visual { get; } = 'E'; 
    }
}