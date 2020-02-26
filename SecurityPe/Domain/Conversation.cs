using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace SecurityPe.Domain
{
    public class Conversation
    {
        public int Id { get; set; }
        public List<string> Messages { get; set; }
        public List<User> Users { get; set; }
    }
}