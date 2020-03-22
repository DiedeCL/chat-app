using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Models
{
    public class MessageModel
    {
        [Required]
        [EmailAddress]
        public string ReceiverEmail { get; set; }
        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Required] 
        public string Message { get; set; }

    }
}
