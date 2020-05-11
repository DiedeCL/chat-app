using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Virgil.Crypto;

namespace SecurityPe.Domain
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserConversations = new List<UserConversation>();
        }
        public IList<UserConversation> UserConversations { get; set; }
        public string PrivateKey { get; set; } // encrypt with aes en take the password hash as key and IV
        public string AesIv { get; set; }


    }
}