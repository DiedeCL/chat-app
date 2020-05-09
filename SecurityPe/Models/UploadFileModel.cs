using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecurityPe.Models
{
    public class UploadFileModel
    {
        [Required]
        [EmailAddress]
        public string ReceiverEmail { get; set; }
        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Required]
        public string Message { get; set; }
        [Required]
        public int ConversationId { get; set; }

        public MultipartFormDataContent FormData { get; set; }
    }
}
