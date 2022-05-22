using System;

namespace CrossTask
{
    public interface ILogger
    {
        public void Log(string message);
    }

    internal sealed class ConsoleLogger: ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}