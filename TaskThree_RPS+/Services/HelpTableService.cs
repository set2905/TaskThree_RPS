using ConsoleTables;
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
            cachedtable = GenerateHelpTable();
            Console.WriteLine("Table is generated from your point of view. \ne.g. \"WON\" means that you would win.");
            cachedtable.Write();
        }

        private ConsoleTable GenerateHelpTable()
        {
            List<string> headerHorizontal = GameOutcomeService.Moves.Select(x => x.Value).ToList();
            headerHorizontal.Insert(0, "v PC\\User >");
            ConsoleTable generatedTable = new(headerHorizontal.ToArray());
            foreach (KeyValuePair<int, string> move in GameOutcomeService.Moves)
            {
                List<string> row = new() { move.Value };
                foreach (KeyValuePair<int, string> keyValuePair in GameOutcomeService.Moves)
                {
                    row.Add(GameOutcomeService.GetOutcome(keyValuePair.Key, move.Key, out _, out _).ToString());
                }
                generatedTable.AddRow(row.ToArray());
            }
            return generatedTable;
        }
    }
}
