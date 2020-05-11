using System.ComponentModel.DataAnnotations;

namespace SecurityPe.Models
{
    public class StoreFileModel
    {
        
        public int ConversationId { get; set; }
        
        public string filePath { get; set; }
    }
}