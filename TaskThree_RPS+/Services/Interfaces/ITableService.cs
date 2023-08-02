using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskThree_RPS_.Services.Interfaces
{
    public interface ITableService
    {
        public IGameOutcomeService GameOutcomeService { get; }
        public void ShowTable();
    }
}
