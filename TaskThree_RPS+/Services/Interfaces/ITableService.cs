namespace TaskThree_RPS_.Services.Interfaces
{
    public interface ITableService
    {
        public IGameOutcomeService GameOutcomeService { get; }
        public void ShowTable();
    }
}
