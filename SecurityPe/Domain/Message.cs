using System;

namespace SecurityPe.Domain
{
    public class Message
    {
        public string ContentOfMessage { get; set; }
        public int IdOfSender { get; set; }
        public int IdOfConversations { get; set; }
        public int Id { get; set; }
    }
}