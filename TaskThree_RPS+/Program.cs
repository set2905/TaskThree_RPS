using TaskThree_RPS_.Services;
using TaskThree_RPS_.Services.Interfaces;

IShowMessageService messageOutputService = new ConsoleMessageShowService();
IArgsValidator<string[]> argValidator = new LaunchArgsValidationService(messageOutputService);

if (!argValidator.Validate(args)) return;

IGameOutcomeService gameOutcomeService = new GameOutcomeService(args, messageOutputService);
IMessageAuthService messageAuthService = new MessageHMACAuthenticationService();
ITableService tableService = new HelpTableService(gameOutcomeService);
GameSequenceController gameSequence = new(messageOutputService, messageAuthService, gameOutcomeService, tableService);
while (true)
{
    gameSequence.PerformGameSequence();
}











