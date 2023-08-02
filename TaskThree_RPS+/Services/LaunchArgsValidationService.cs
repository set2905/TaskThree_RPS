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

        public bool Validate(string[] args)
        {
            bool result = true;
            if (args.Length < 3)
            {
                messageOutputService.ShowError("Please, pass more than 3 parameters!");
                result = false;
            }
            if (args.Length % 2 == 0)
            {
                messageOutputService.ShowError($"Please, pass odd number of parameters!\nYou've passed {args.Length} parameters, which is even.");
                result = false;

            }
            if (args.Distinct().Count() != args.Length)
            {
                IEnumerable<string> repeated = args.GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .Select(y => y.Key);
                StringBuilder repeatedParamMsgBuilder = new();
                repeatedParamMsgBuilder.AppendLine("Parameters should not repeat.");
                repeatedParamMsgBuilder.AppendLine();
                repeatedParamMsgBuilder.AppendLine("Found repeated parameters: ");
                foreach (string repeatedParameter in repeated)
                {
                    repeatedParamMsgBuilder.AppendLine($"- {repeatedParameter}");
                }
                repeatedParamMsgBuilder.AppendLine();
                repeatedParamMsgBuilder.AppendLine("Try again with non-repeating parameters!");
                messageOutputService.ShowError(repeatedParamMsgBuilder.ToString());
                result = false;
            }
            return result;
        }

    }



}
