using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityPe.Domain;

namespace SecurityPe.Services
{
    public interface IConversationData
    {
        IEnumerable<Conversation> GetAll();
        Conversation GetConversationById(int id);

        IEnumerable<Message> GetMessages(int conversationId);
        void Add(Conversation newConversation);
    }
}
