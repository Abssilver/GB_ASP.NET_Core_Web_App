using System.Collections.Generic;
using System.Linq;

namespace CrossTask
{
    public static class Boot
    {
        static void Main(string[] args)
        {           
            var size = 8;
            var winSteak = 4;

            var noneSign = new NoneSign();
            var board = new Board(size, size, noneSign);
            var logger = new ConsoleLogger();
            var inputValidator = new GameValidator(board, noneSign);
            var players = new List<IPlayer>
            {
                new HumanPlayer(new Cross(), "Mr Cross", inputValidator, logger),
                new DummyAiPlayer(new Circle(), noneSign, board,"Mr Fluffy", logger),
            };
            
            var drawService = new ConsoleDrawer(noneSign, players.Select(x => x.Sign));
            var game = new Game(players, noneSign, board, winSteak, drawService);
            
            game.NewGame();
            GameLoop(game);
            game.ShowResults();
        }

        private static void GameLoop(Game game)
        {
            while (game.IsAbleToPlay)
            {
                game.ProcessGame();
            }
        }
    }
}