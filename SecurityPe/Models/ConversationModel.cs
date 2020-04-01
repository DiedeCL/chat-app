using System.ComponentModel.DataAnnotations;

namespace SecurityPe.Models
{
    public class ConversationModel
    {
        [Required]
        public int ConversationID { get; set; }
        [Required] 
        [EmailAddress]
        public string Email { get; set; }
    }
}