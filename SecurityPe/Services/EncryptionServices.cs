using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SecurityPe.Domain;


namespace SecurityPe.Services
{
    public class EncryptionServices
    {
        private Aes _aes;
        private RSA _rsa;

        public EncryptionServices(byte[] aesKey, User user)
        {
            _aes = Aes.Create();
            _rsa = RSA.Create();
        }


        
     
    }
}
