using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskThree_RPS_.Services.Interfaces
{
    public enum GameOutcomeEnum
    {
        Win,
        Lose,
        Draw,
        Undefined
    }
    public interface IGameOutcomeService
    {
        public Dictionary<int, string> MovesDictionary { get; }
        public GameOutcomeEnum GetOutcome(string playerMoveString, int opponentMoveKey, out string playerMove, out string opponentMove);
    }
}
