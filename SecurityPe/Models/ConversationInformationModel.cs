using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Models
{
    public class ConversationInformationModel
    {

        [Required]
        public int ConversationID { get; set; }
        [Required]
        [EmailAddress]
        public string EmailOfCurrentUser { get; set; }
    }
}
