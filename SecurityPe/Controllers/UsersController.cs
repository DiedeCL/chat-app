using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityPe.Data;

namespace SecurityPe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsersController : ControllerBase
    {
        private ChatAppContext _context;

        public UsersController(ChatAppContext context)
        {
            _context = context;
        }
        [HttpGet("{action}")]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.PublicKeyStores.ToList());

        }
    }
}