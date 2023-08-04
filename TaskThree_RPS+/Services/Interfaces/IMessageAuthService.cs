using Org.BouncyCastle.Crypto;
namespace TaskThree_RPS_.Services.Interfaces
{
    public interface IMessageAuthService
    {
        public IMac MAC { get; }
        public string GetHMAC(string move, out string secretKey);
        public string GetHMAC(string move, string secretKey);

    }
}
