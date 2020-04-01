using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Models
{
    public class CreateConversationModel
    {
        [Required]
        
        public string ReceiverEmail { get; set; }
        [Required]
        public string SenderEmail { get; set; }
    }
}
