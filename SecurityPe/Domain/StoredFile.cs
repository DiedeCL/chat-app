using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Domain
{
    public class StoredFile
    {
        public string FilePath { get; set; }
        public int Id { get; set; }
        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }
    }
}
