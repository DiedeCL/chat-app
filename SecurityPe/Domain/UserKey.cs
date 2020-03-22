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
           
        }

        public string PublicKey { get; set; } // in xml format      
        public string PrivateKey { get; set; } // in xml format

        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }

      
    }
}
