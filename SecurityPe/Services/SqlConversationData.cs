using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityPe.Data;
using SecurityPe.Domain;

namespace SecurityPe.Services
{
    public class SqlConversationData : IConversationData

    {
        private readonly ChatAppContext _context;

        public SqlConversationData(ChatAppContext context)
        {
            _context = context;
        }

        public IEnumerable<Conversation> GetAll()
        {
            return _context.Conversations.ToList();
        }

        public Conversation GetConversationById(int id)
        {
            return _context.Conversations.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Message> GetMessages(int conversationId)
        {
            return GetConversationById(conversationId).Messages;
        }

        public void Add(Conversation newConversation)
        {
            _context.Conversations.Add(newConversation);
            _context.SaveChanges();
        }
    }
}
