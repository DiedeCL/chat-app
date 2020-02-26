using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using SecurityPe.Domain;

namespace SecurityPe.Data
{
    public class TempData
    {
        private List<User> _Users = new List<User>();
        private List<Conversation> _Conversations =new List<Conversation>();

        
        public TempData()
        {
            
            var messages = new List<String>();
            messages.Add("Hello Bogaert");
            messages.Add("Hello Boris");
            messages.Add("Alles ok?");
            var userIds = new List<int>();
            userIds.Add(1);
            userIds.Add(2);
            var conversation = new Conversation
            {
                Id = 0,
                Messages = messages,
                UserIds = userIds
                
            };
            var conversationIds = new List<int>();
            conversationIds.Add(conversation.Id);
            _Conversations.Add(conversation);
            var bogaert = new User
            {
                Id = 1,
                Name = "Bogaert",
                Email = "bogaert@gmail.com",
                Password = "12345",
                ConversationIds =  conversationIds
                
            };
            var boris = new User
            {
                Id = 2,
                Name = "Boris",
                Email = "boris@gmail.com",
                Password = "12345",
                ConversationIds = conversationIds
            };
            _Users.Add(bogaert);
            _Users.Add(boris);
           
        }

        public void AddUser(User user)
        {
            _Users.Add(user);
        }

        public void AddMessagesToConversation(int idConversation, string message)
        {
            _Conversations[idConversation].Messages.Add(message);
        }

        public void AddConversation(Conversation conversation)
        {
            conversation.Id = _Conversations.Count;
        }

        public List<User> GetUsers()
        {
            return _Users;
        }

        public List<Conversation> GetConversations()
        {
            return _Conversations;
        }

        public List<string> GetMessagesOfConversation(int idConversation)
        {
            return _Conversations[idConversation].Messages;
        }
        

    }
}