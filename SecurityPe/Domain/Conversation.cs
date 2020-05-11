using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace SecurityPe.Domain
{
    public class Conversation
    {
        public Conversation()
        {
            Messages = new List<Message>();
            Files = new List<StoredFile>();
        }
        public int Id { get; set; }

        public IList<Message> Messages { get; set; }

        public List<UserConversation> UserConversations { get; set; }

        public List<StoredFile> Files { get; set; }

    }
}