using System.ComponentModel.DataAnnotations;

namespace SecurityPe.Models
{
    public class GetFilesModel
    {
        [Required]
        public int ConversationId { get; set; }
    }
}