using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services
{
    public class LaunchArgsValidationService : IArgsValidator<string[]>
    {
        private const string EXAMPLE_ARGS = "rock Spock paper lizard scissors";
        private IShowMessageService messageOutputService;

        public LaunchArgsValidationService(IShowMessageService messageOutputService)
        {
            this.messageOutputService = messageOutputService;
        }

        public bool IsValid(string[] args)
        {
            bool result = IsArgsLengthCorrect(args, 3) & IsArgsLengthOdd(args) & IsArgsRepeating(args);
            if (result==false)
                Console.WriteLine($"Correct parameters example: {EXAMPLE_ARGS}");
            return result;
        }

        private bool IsArgsLengthCorrect(string[] args, int requiredLength)
        {
            
            if (args.Length < requiredLength)
            {
                messageOutputService.ShowError($"You've passed {args.Length} parameters. Please, pass more than {requiredLength} parameters!");
                return false;
            }
            return true;
        }

        private bool IsArgsLengthOdd(string[] args)
        {
            if (args.Length % 2 == 0)
            {
                messageOutputService.ShowError($"Please, pass odd number of parameters!\nYou've passed {args.Length} parameters, which is even.");
                return false;
            }
            return true;
        }

        private bool IsArgsRepeating(string[] args)
        {
            if (args.Distinct().Count() != args.Length)
            {
                IEnumerable<string> repeated = args.GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .Select(y => y.Key);
                StringBuilder repeatedParamMsgBuilder = new();
                repeatedParamMsgBuilder.AppendLine("Parameters should not repeat.")
                    .AppendLine()
                    .AppendLine("Found repeated parameters: ");
                foreach (string repeatedParameter in repeated)
                {
                    repeatedParamMsgBuilder.AppendLine($"- {repeatedParameter}");
                }
                repeatedParamMsgBuilder.AppendLine().AppendLine("Try again with non-repeating parameters!");
                messageOutputService.ShowError(repeatedParamMsgBuilder.ToString());
                return false;
            }
            return true;
        }
    }



}
