using System.Text;
using TaskThree_RPS_.Services;
using TaskThree_RPS_.Services.Interfaces;

IShowMessageService messageOutputService = new ConsoleMessageShowService();
IArgsValidator<string[]> argValidator = new LaunchArgsValidationService(messageOutputService);

if (!argValidator.Validate(args))
{
    return;
}
IGameOutcomeService gameOutcomeService = new GameOutcomeService(args, messageOutputService);
IMessageAuthService messageAuthService = new MessageHMACAuthenticationService();
ITableService tableService = new HelpTableService(gameOutcomeService);
while (true)
{
    PerformGameSequence();
}

bool PerformGameSequence()
{
    int computerMoveChoice = gameOutcomeService.GetRandomMove();
    string secretKey = string.Empty;

    string hmac = messageAuthService.GetHMAC(computerMoveChoice, out secretKey);
    StringBuilder moveOptionsBuilder = new();
    moveOptionsBuilder.AppendLine("HMAC:");
    moveOptionsBuilder.AppendLine(hmac);
    moveOptionsBuilder.AppendLine("Available Moves: ");
    foreach (KeyValuePair<int, string> entry in gameOutcomeService.MovesDictionary)
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
        case GameOutcomeEnum.UNDEFINED:
            messageOutputService.ShowError(outcome.ToString());
            break;
        case GameOutcomeEnum.WIN:
            messageOutputService.ShowSuccess($"You {outcome.ToString()}!");
            break;
        case GameOutcomeEnum.LOSE:
            messageOutputService.ShowDanger($"You {outcome.ToString()}!");
            break;
        case GameOutcomeEnum.DRAW:
            messageOutputService.ShowPrimary(outcome.ToString());
            break;
    }
    if (outcome==GameOutcomeEnum.UNDEFINED) return false;



    Console.WriteLine($"HMAC key:\n{secretKey}");
    messageOutputService.ShowPrimary("------------------ANOTHER ONE?------------------");

    return true;
}









