using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace SecurityPe.Domain
{
    public class Conversation
    {
        public int Id { get; set; }
        public IList<Message> Messages { get; set; }

        public int UserOneId { get; set; }
        public int UserTwoId { get; set; }

    }
}