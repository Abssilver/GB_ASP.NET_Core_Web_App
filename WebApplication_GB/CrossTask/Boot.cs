using System.Collections.Generic;
using System.Linq;

namespace CrossTask
{
    public static class Boot
    {
        static void Main(string[] args)
        {           
            var size = 10;
            var winSteak = 5;

            var noneSign = new NoneSign();
            var board = new Board(size, size, noneSign);
            var logger = new ConsoleLogger();
            var inputValidator = new GameValidator(board, noneSign);
            var botFactory = new BotFactory(board, logger, winSteak);
            var players = new List<IPlayer>
            {
                //TODO: Disable player, if you want to see how ai plays
                new HumanPlayer(new Cross(), "Mr Cross", inputValidator, logger),
                botFactory.CreateDummyAi(new Circle(), "Mr Bun"),
                botFactory.CreateDefenderAi(new ASign(), "Shield Ace"),
                botFactory.CreateDefenderAi(new ESign(), "Attentive Kitty"),
                //botFactory.CreateDummyAi(new Cross(), "Mr Fluffy"),
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