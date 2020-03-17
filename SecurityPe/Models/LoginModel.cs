using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityPe.Models
{
    public class LoginModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
