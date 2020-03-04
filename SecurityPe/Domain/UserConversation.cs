namespace SecurityPe.Domain
{
    public class UserConversation
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Conversation Conversation { get; set; }
    }
}