using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services
{
    public class ConsoleMessageShowService : IShowMessageService
    {
        public void ShowDanger(string message)
        {
            ShowMessage(message, ConsoleColor.Red);

        }

        public void ShowError(string message)
        {
            ShowMessage("ERROR");
            ShowMessage(message, ConsoleColor.Red);
        }

        public void ShowPrimary(string message)
        {
            ShowMessage(message, ConsoleColor.Blue);
        }

        public void ShowSecondary(string message)
        {
            ShowMessage(message, ConsoleColor.Yellow);
        }

        public void ShowSuccess(string message)
        {
            ShowMessage(message, ConsoleColor.Green);
        }
        private void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
