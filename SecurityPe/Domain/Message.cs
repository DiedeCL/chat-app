using System;

namespace SecurityPe.Domain
{
    public class Message
    {
        public string EncryptedContentOfMessage { get; set; }
        public int IdOfFile { get; set; }
        public string EmailOfSender { get; set; }
        public string SignedData { get; set; }
        public string EncryptedAesKey { get; set; }

        public string EncryptedAesIV { get; set; }
        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }
        public int Id { get; set; }
    }
}