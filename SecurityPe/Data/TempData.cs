using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using SecurityPe.Domain;

namespace SecurityPe.Data
{
    public class TempData
    {
        private List<User> _Users;
        private List<Conversation> _Conversations;

        
        public TempData()
        {
            _Users = new List<User>();
            _Conversations = new List<Conversation>();
            var messages = new List<String>();
            messages.Add("Hello Bogaert");
            messages.Add("Hello Boris");
            messages.Add("Alles ok?");
            var conversation = new Conversation
            {
                Id = 0,
                Messages = messages,
                Users = _Users.GetRange(0,2)
            };
            var conversations = new List<Conversation>();
            conversations.Add(conversation);
            _Conversations.Add(conversation);
            var bogaert = new User
            {
                Id = 1,
                Name = "Bogaert",
                Email = "bogaert@gmail.com",
                Password = "12345",
                Conversations = conversations
                
            };
            var boris = new User
            {
                Id = 2,
                Name = "Boris",
                Email = "boris@gmail.com",
                Password = "12345",
                Conversations = conversations
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