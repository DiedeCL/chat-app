using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace SecurityPe.Domain
{
    public class Role : IdentityRole<int>
    {
         
        public const string User = "User";
        
    }
}
