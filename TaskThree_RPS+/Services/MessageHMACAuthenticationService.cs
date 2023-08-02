using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree_RPS_.Services.Interfaces;

namespace TaskThree_RPS_.Services
{
    public class MessageHMACAuthenticationService : IMessageAuthService
    {
        public string GetHMAC(int move, out string secretKey)
        {
            secretKey="TEST_KEY";
            return "TEST_HMAC";
        }
    }
}
