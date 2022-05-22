using System.Collections.Generic;
using System.Linq;

namespace CrossTask
{
    internal sealed class MoveNode
    {
        public int Side { get; }
        public Position Position { get; }
        public Core Core { get; }
        
        public MoveNode(int side, Position position, IList<MoveNode> moves)
        {
            Side = side;
            Position = position;
            Core = new Core(this, moves);
        }
    }

    internal sealed class Core
    {
        public Core(MoveNode current, IList<MoveNode> moves)
        {
            if (moves is null || moves.Count == 0)
                return;

            var pos = current.Position;
            TopLeft = moves.FirstOrDefault(x => 
                pos.Row - 1 == x.Position.Row && pos.Column - 1 == x.Position.Column);
            Top = moves.FirstOrDefault(x => 
                pos.Row - 1 == x.Position.Row && pos.Column == x.Position.Column);
            TopRight = moves.FirstOrDefault(x => 
                pos.Row - 1 == x.Position.Row && pos.Column + 1 == x.Position.Column);
            MidLeft = moves.FirstOrDefault(x => 
                pos.Row == x.Position.Row && pos.Column - 1 == x.Position.Column);
            MidRight = moves.FirstOrDefault(x => 
                pos.Row == x.Position.Row && pos.Column + 1 == x.Position.Column);
            BotLeft = moves.FirstOrDefault(x => 
                pos.Row + 1 == x.Position.Row && pos.Column - 1 == x.Position.Column);
            Bot = moves.FirstOrDefault(x => 
                pos.Row + 1 == x.Position.Row && pos.Column == x.Position.Column);
            BotRight = moves.FirstOrDefault(x => 
                pos.Row + 1 == x.Position.Row && pos.Column + 1 == x.Position.Column);
            
            if (TopLeft != null)
                TopLeft.Core.BotRight = current;
            if (Top != null)
                Top.Core.Bot = current;
            if (TopRight != null)
                TopRight.Core.BotLeft = current;
            if (MidLeft != null)
                MidLeft.Core.MidRight = current;
            if (MidRight != null)
                MidRight.Core.MidLeft = current;
            if (BotLeft != null)
                BotLeft.Core.TopRight = current;
            if (Bot != null)
                Bot.Core.Top = current;
            if (BotRight != null)
                BotRight.Core.TopLeft = current;
        }

        public MoveNode TopLeft { get; set; }
        public MoveNode Top { get; set; }
        public MoveNode TopRight { get; set; }
        public MoveNode MidLeft { get; set; }
        public MoveNode MidRight { get; set; }
        public MoveNode BotLeft { get; set; }
        public MoveNode Bot { get; set; }
        public MoveNode BotRight { get; set; }
    }
}