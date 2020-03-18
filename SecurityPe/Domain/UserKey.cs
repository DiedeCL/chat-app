using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SecurityPe.Domain
{
    public class UserKey
    {
        public UserKey()
        {
            var rsa = RSA.Create();
            PublicKey = rsa.ExportRSAPublicKey();
            PrivateKey = rsa.ExportRSAPrivateKey();
        }

        public byte[] PublicKey { get; }
        public byte[] PrivateKey { get; }

        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
