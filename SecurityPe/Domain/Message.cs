using System;

namespace SecurityPe.Domain
{
    public class Message
    {
        public string ContentOfMessage { get; set; }
        public int IdOfSender { get; set; }
        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }
        public int Id { get; set; }
    }
}