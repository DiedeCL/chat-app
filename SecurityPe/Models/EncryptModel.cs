using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Models
{
    public class EncryptModel
    {
        [Required]
        
        public string PrivateKey { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
