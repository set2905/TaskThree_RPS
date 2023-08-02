namespace TaskThree_RPS_.Services.Interfaces
{
    public interface IMessageAuthService
    {
        public string GetHMAC(int move, out string secretKey);

    }
}
