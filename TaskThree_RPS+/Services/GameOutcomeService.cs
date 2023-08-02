using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services
{
    public class GameOutcomeService : IGameOutcomeService
    {
        public Dictionary<int, string> MovesDictionary { get; }
        private IShowMessageService messageOutputService;

        public GameOutcomeService(string[] args, IShowMessageService messageOutputService)
        {
            MovesDictionary = args.Select((v, i) => new { Key = i+1, Value = v })
                                  .ToDictionary(o => o.Key, o => o.Value);
            this.messageOutputService = messageOutputService;
        }

        public GameOutcomeEnum GetOutcome(string playerMoveString, int opponentMoveKey, out string playerMove, out string opponentMove)
        {
            if (!int.TryParse(playerMoveString, out int playerMoveKey))
            {
                messageOutputService.ShowError("Enter integer value!");
                opponentMove=string.Empty;
                playerMove = string.Empty;
                return GameOutcomeEnum.Undefined;
            }
            if (!MovesDictionary.ContainsKey(playerMoveKey))
            {
                messageOutputService.ShowError($"Could not find key {playerMoveString} in move dictionary.");
                opponentMove=string.Empty;
                playerMove = string.Empty;
                return GameOutcomeEnum.Undefined;
            }

            if (!MovesDictionary.ContainsKey(opponentMoveKey))
            {
                messageOutputService.ShowError($"Could not find opponent key {opponentMoveKey} in move dictionary.");
                opponentMove=string.Empty;
                playerMove = string.Empty;
                return GameOutcomeEnum.Undefined;
            }

            playerMove = MovesDictionary[playerMoveKey];
            opponentMove = MovesDictionary[opponentMoveKey];

            if (playerMoveKey==opponentMoveKey) return GameOutcomeEnum.Draw;


            return GameOutcomeEnum.Undefined;
        }
    }
}
