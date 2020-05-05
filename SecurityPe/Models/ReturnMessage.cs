using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Models
{
    public class ReturnMessage
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public int ConversationId { get; set; }
        public string EmailOfSender { get; set; }
        public bool DataIsTrusted { get; set; }
    }
}
