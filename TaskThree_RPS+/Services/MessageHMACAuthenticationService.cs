using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using System.Text;
using TaskThree_RPS_.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TaskThree_RPS_.Services
{
    public class MessageHMACAuthenticationService : IMessageAuthService
    {
        public MessageHMACAuthenticationService(IMac mac)
        {
            MAC=mac;
        }

        public IMac MAC { get; }


        public string GetHMAC(string move, out string secretKey)
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                secretKey = Convert.ToBase64String(randomNumber);
            }
            MAC.Init(new KeyParameter(Encoding.ASCII.GetBytes(secretKey)));

            byte[] result = new byte[MAC.GetMacSize()];
            byte[] bytes = Encoding.ASCII.GetBytes(move);
            MAC.BlockUpdate(bytes, 0, bytes.Length);
            MAC.DoFinal(result, 0);
            return Convert.ToHexString(result);
        }
    }
}
