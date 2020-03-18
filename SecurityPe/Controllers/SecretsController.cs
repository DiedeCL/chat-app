using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecurityPe.Domain;
using SecurityPe.Models;

namespace SecurityPe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SecretsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public SecretsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("encrypt")]

        public async Task<IActionResult> Encrypt([FromBody] UserModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var publicKey = GetPublicKey(user);
            if (publicKey == null) return Ok("Key does not exist");

            return Ok(publicKey.ToString());
        }

        private byte[] GetPublicKey(User user)
        {
            return user.UserKey.PublicKey;
        }
    }
}