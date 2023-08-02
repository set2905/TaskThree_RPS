using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskThree_RPS_.Services.Interfaces
{
    public interface IShowMessageService
    {
        public void ShowError(string message);
        public void ShowSuccess(string message);
        public void ShowDanger(string message);
        public void ShowSecondary(string message);
        public void ShowPrimary(string message);

    }
}
