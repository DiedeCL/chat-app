using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SecurityPe.Domain
{
    public class User : IdentityUser<int>
    {
        public IList<UserConversation> UserConversations { get; set; }

    }
}