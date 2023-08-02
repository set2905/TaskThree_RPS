using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskThree_RPS_.Services.Interfaces
{
    public enum GameOutcomeEnum
    {
        WIN,
        LOSE,
        DRAW,
        UNDEFINED
    }
    public interface IGameOutcomeService
    {
        public Dictionary<int, string> Moves { get; }
        public GameOutcomeEnum GetOutcome(string playerMoveString, int opponentMoveKey, out string playerMove, out string opponentMove);
        public GameOutcomeEnum GetOutcome(int playerMoveKey, int opponentMoveKey, out string playerMoveName, out string opponentMoveName);

        public int GetRandomMove(int seed);
        public int GetRandomMove();
    }
}
