using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Virgil.Crypto;

namespace SecurityPe.Domain
{
    public class User : IdentityUser<int>
    {
        
        public IList<UserConversation> UserConversations { get; set; }
        public UserKey UserKey { get; set; }


    }
}