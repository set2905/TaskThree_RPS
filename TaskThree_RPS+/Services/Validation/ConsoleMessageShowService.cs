using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services.Validation
{
    public class ConsoleMessageShowService : IShowMessageService
    {
        public void ShowError(string message)
        {
            Console.WriteLine("-----------ERROR-----------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
