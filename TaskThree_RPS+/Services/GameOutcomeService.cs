using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services
{
    public class GameOutcomeService :IGameOutcomeService
    {
        public Dictionary<int, string> Moves { get; }
        private IShowMessageService messageOutputService;

        public GameOutcomeService(string[] moves, IShowMessageService messageOutputService)
        {
            Moves = moves.Select((v, i) => new { Key = i+1, Value = v })
                                  .ToDictionary(o => o.Key, o => o.Value);

            this.messageOutputService = messageOutputService;
        }
        public int GetRandomMove(int seed)
        {
            System.Random rng = new System.Random(seed);
            return rng.Next(1, Moves.Count);
        }
        public int GetRandomMove()
        {
            System.Random rng = new System.Random();
            return rng.Next(1, Moves.Count);
        }

        public GameOutcomeEnum GetOutcome(string playerMoveKeyInput, int opponentMoveKey, out string playerMoveName, out string opponentMoveName)
        {
            if (!int.TryParse(playerMoveKeyInput, out int playerMoveKey))
            {
                messageOutputService.ShowError("Enter integer value!");
                opponentMoveName=string.Empty;
                playerMoveName = string.Empty;
                return GameOutcomeEnum.UNDEFINED;
            }
            return GetOutcome(playerMoveKey, opponentMoveKey, out playerMoveName, out opponentMoveName);
        }

        public GameOutcomeEnum GetOutcome(int playerMoveKey, int opponentMoveKey, out string playerMoveName, out string opponentMoveName)
        {

            if (!Moves.ContainsKey(playerMoveKey))
            {
                messageOutputService.ShowError($"Could not find key {playerMoveKey} in move dictionary.");
                opponentMoveName=string.Empty;
                playerMoveName = string.Empty;
                return GameOutcomeEnum.UNDEFINED;
            }

            if (!Moves.ContainsKey(opponentMoveKey))
            {
                messageOutputService.ShowError($"Could not find opponent key {opponentMoveKey} in move dictionary.");
                opponentMoveName=string.Empty;
                playerMoveName = string.Empty;
                return GameOutcomeEnum.UNDEFINED;
            }

            playerMoveName = Moves[playerMoveKey];
            opponentMoveName = Moves[opponentMoveKey];

            if (playerMoveKey==opponentMoveKey) return GameOutcomeEnum.DRAW;

            int half = (Moves.Count-1)/2;
            for (int i = 1; i<=half; i++)
            {
                int circular = GetCircularMoveIndex(playerMoveKey+i);
                if (circular==opponentMoveKey)
                {
                    return GameOutcomeEnum.LOSE;
                }
            }

            return GameOutcomeEnum.WIN;
        }

        int GetCircularMoveIndex(int index)
        {
            if (index>Moves.Count)
            {
                index %= Moves.Count;
                return index;
            }
            if (index<0)
            {
                index = Moves.Count - 1;
                return index;
            }
            return index;
        }


    }
}
