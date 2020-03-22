using System;

namespace SecurityPe.Domain
{
    public class Message
    {
        public string EncryptedContentOfMessage { get; set; }
        public int IdOfSender { get; set; }
        public string Md5Hash { get; set; }
        public string EncryptedAesKey { get; set; }

        public string EncryptedAesIV { get; set; }
        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }
        public int Id { get; set; }
    }
}