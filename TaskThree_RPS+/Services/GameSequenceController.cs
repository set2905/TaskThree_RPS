﻿using System.Text;
using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services
{
    public class GameSequenceController
    {
        private readonly IShowMessageService messageOutputService;
        private readonly IMessageAuthService messageAuthService;
        private readonly IGameOutcomeService gameOutcomeService;
        private readonly ITableService tableService;

        public GameSequenceController(IShowMessageService messageOutputService, IMessageAuthService messageAuthService, IGameOutcomeService gameOutcomeService, ITableService tableService)
        {
            this.messageOutputService=messageOutputService;
            this.messageAuthService=messageAuthService;
            this.gameOutcomeService=gameOutcomeService;
            this.tableService=tableService;
        }

        public bool PerformGameSequence()
        {
            int computerMoveChoice = gameOutcomeService.GetRandomMove();
            string secretKey = string.Empty;

            string hmac = messageAuthService.GetHMAC(computerMoveChoice, out secretKey);
            StringBuilder moveOptionsBuilder = new();
            moveOptionsBuilder.AppendLine("HMAC:");
            moveOptionsBuilder.AppendLine(hmac);
            moveOptionsBuilder.AppendLine("Available Moves: ");
            foreach (KeyValuePair<int, string> entry in gameOutcomeService.Moves)
            {
                moveOptionsBuilder.AppendLine($"{entry.Key.ToString()} - {entry.Value}");
            }
            moveOptionsBuilder.AppendLine("0 - exit");
            moveOptionsBuilder.AppendLine("? - help");
            Console.WriteLine(moveOptionsBuilder.ToString());

            Console.WriteLine("Enter your move: ");
            string? playerInput = Console.ReadLine();
            if (playerInput==null) return false;
            if (playerInput == "0") Environment.Exit(0);
            if (playerInput == "?")
            {
                tableService.ShowTable();
                return true;
            }

            GameOutcomeEnum outcome = GameOutcomeEnum.UNDEFINED;

            outcome = gameOutcomeService.GetOutcome(playerInput, computerMoveChoice, out string playerMoveString, out string computerMoveString);
            Console.WriteLine($"Your move: {playerMoveString}");
            Console.WriteLine($"Computer move: {computerMoveString}");
            switch (outcome)
            {
                case GameOutcomeEnum.WIN:
                    messageOutputService.ShowSuccess($"You {outcome.ToString()}!");
                    break;
                case GameOutcomeEnum.LOSE:
                    messageOutputService.ShowDanger($"You {outcome.ToString()}!");
                    break;
                case GameOutcomeEnum.DRAW:
                    messageOutputService.ShowPrimary(outcome.ToString());
                    break;
                default:
                    messageOutputService.ShowError(outcome.ToString());
                    break;
            }
            if (outcome==GameOutcomeEnum.UNDEFINED) return false;



            Console.WriteLine($"HMAC key:\n{secretKey}");
            messageOutputService.ShowPrimary("------------------ANOTHER ONE?------------------");

            return true;
        }
    }
}