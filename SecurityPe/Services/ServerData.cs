using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Services
{
    public class ServerData
    {
        public ServerData()
        {
            PrivateKey = EncryptionServices.GetPrivateKey();
            PublicKey = EncryptionServices.GetPublicKey();
        }

        public string PrivateKey { get; }

        public string PublicKey { get; }
    }
}
