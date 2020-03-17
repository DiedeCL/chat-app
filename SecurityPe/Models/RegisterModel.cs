using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public string UserName { get; set; }

    }
}
