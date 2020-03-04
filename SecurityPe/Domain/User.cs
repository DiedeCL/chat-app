using System;
using System.Collections.Generic;

namespace SecurityPe.Domain
{
    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public IList<UserConversation> UserConversations { get; set; }

    }
}