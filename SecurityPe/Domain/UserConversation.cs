using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Domain
{
    public class UserConversation
    {
        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }


    }
}
