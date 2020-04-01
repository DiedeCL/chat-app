using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SecurityPe.Domain
{
    public class PublicKeyStore
    {
        public string PublicKey { get; set; } // in xml format      

        public int Id { get; set; }

        public string Email { get; set; }
      
    }
}
