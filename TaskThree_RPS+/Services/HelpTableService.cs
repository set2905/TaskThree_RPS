using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services
{
    public class HelpTableService : ITableService
    {
        ConsoleTable? cachedtable;
        public IGameOutcomeService GameOutcomeService { get; }
        public HelpTableService(IGameOutcomeService gameOutcomeService)
        {
            GameOutcomeService=gameOutcomeService;
        }

        public void ShowTable()
        {
            if (cachedtable!=null)
            {
                cachedtable.Write();
                return;
            }

            List<string> headerHorizontal = GameOutcomeService.Moves.Select(x => x.Value).ToList();
            headerHorizontal.Insert(0, "v PC\\User >");
            cachedtable = new(headerHorizontal.ToArray());
            foreach (KeyValuePair<int, string> move in GameOutcomeService.Moves)
            {
                List<string> row = new() { move.Value };
                foreach (KeyValuePair<int, string> keyValuePair in GameOutcomeService.Moves)
                {
                    row.Add(GameOutcomeService.GetOutcome(keyValuePair.Key, move.Key, out _, out _).ToString());
                }
                cachedtable.AddRow(row.ToArray());
            }
            cachedtable.Write();
        }
    }
}
