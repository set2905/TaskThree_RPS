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
        public HelpTableService(IGameOutcomeService gameOutcomeService)
        {
            GameOutcomeService=gameOutcomeService;
        }

        public IGameOutcomeService GameOutcomeService { get; }

        public void ShowTable()
        {
            List<string> headerHorizontal = GameOutcomeService.MovesDictionary.Select(x => x.Value).ToList();
            headerHorizontal.Insert(0, "v PC\\User >");
            ConsoleTable table = new(headerHorizontal.ToArray());
            foreach (KeyValuePair<int, string> move in GameOutcomeService.MovesDictionary)
            {
                List<string> row = new() { move.Value };
                foreach (KeyValuePair<int, string> keyValuePair in GameOutcomeService.MovesDictionary)
                {
                    row.Add(GameOutcomeService.GetOutcome(keyValuePair.Key, move.Key, out _, out _).ToString());
                }
                table.AddRow(row.ToArray());
            }
            table.Write();
        }
    }
}
