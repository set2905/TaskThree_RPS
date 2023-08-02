using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using TaskThree_RPS_.Services;
using TaskThree_RPS_.Services.Interfaces;

IShowMessageService messageOutputService = new ConsoleMessageShowService();
IArgsValidator<string[]> argValidator = new LaunchArgsValidationService(messageOutputService);

if (!argValidator.Validate(args)) return;

IGameOutcomeService gameOutcomeService = new GameOutcomeService(args, messageOutputService);
IMessageAuthService messageAuthService = new MessageHMACAuthenticationService(new HMac(new Sha3Digest(256)));
ITableService tableService = new HelpTableService(gameOutcomeService);
GameSequenceController gameSequence = new(messageOutputService, messageAuthService, gameOutcomeService, tableService);
while (true)
{
    gameSequence.PerformGameSequence();
}











