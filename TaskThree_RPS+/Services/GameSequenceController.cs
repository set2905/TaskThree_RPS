using System.Text;
using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services
{
    public class GameSequenceController
    {
        private readonly IShowMessageService messageOutputService;
        private readonly IMessageAuthService messageAuthService;
        private readonly IGameOutcomeService gameOutcomeService;
        private readonly ITableService tableService;

        public GameSequenceController(IShowMessageService messageOutputService,
                                      IMessageAuthService messageAuthService,
                                      IGameOutcomeService gameOutcomeService,
                                      ITableService tableService)
        {
            this.messageOutputService=messageOutputService;
            this.messageAuthService=messageAuthService;
            this.gameOutcomeService=gameOutcomeService;
            this.tableService=tableService;
        }

        public bool PerformGameSequence()
        {
            int computerMoveChoice = gameOutcomeService.GetRandomMove();
            string exitKey = (gameOutcomeService.Moves.First().Key-1).ToString();
            string hmac = messageAuthService.GetHMAC(gameOutcomeService.Moves[computerMoveChoice], out string secretKey);

            ShowGamePrerequisiteInfo(hmac, exitKey);

            Console.WriteLine("Enter your move: ");
            string? playerInput = Console.ReadLine();
            if (playerInput==null) return false;
            if (playerInput == exitKey) Environment.Exit(0);
            if (playerInput == "?")
            {
                tableService.ShowTable();
                return true;
            }

            GameOutcomeEnum outcome = gameOutcomeService.GetOutcome(playerInput,
                                                    computerMoveChoice,
                                                    out string playerMoveString,
                                                    out string computerMoveString);

            if (outcome==GameOutcomeEnum.UNDEFINED) return false;
            Console.WriteLine($"Your move: {playerMoveString}");
            Console.WriteLine($"Computer move: {computerMoveString}");
            switch (outcome)
            {
                case GameOutcomeEnum.WIN:
                    messageOutputService.ShowSuccess($"You {outcome}!");
                    break;
                case GameOutcomeEnum.LOSE:
                    messageOutputService.ShowDanger($"You {outcome}!");
                    break;
                case GameOutcomeEnum.DRAW:
                    messageOutputService.ShowPrimary(outcome.ToString());
                    break;
                default:
                    messageOutputService.ShowError(outcome.ToString());
                    break;
            }

            Console.WriteLine($"HMAC key:\n{secretKey}");
            messageOutputService.ShowPrimary("PLAY ANOTHER ONE?");
            return true;
        }

        private void ShowGamePrerequisiteInfo(string hmac, string exitKey)
        {
            StringBuilder moveOptionsBuilder = new();
            moveOptionsBuilder.AppendLine("HMAC:").AppendLine(hmac).AppendLine("Available Moves: ");
            foreach (KeyValuePair<int, string> entry in gameOutcomeService.Moves)
            {
                moveOptionsBuilder.AppendLine($"{entry.Key.ToString()} - {entry.Value}");
            }
            moveOptionsBuilder.AppendLine($"{exitKey} - exit").AppendLine("? - help");
            Console.WriteLine(moveOptionsBuilder.ToString());
        }
    }
}
