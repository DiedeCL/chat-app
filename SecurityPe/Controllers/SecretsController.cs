using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecurityPe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SecretsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllSecrets()
        {
            var userInfoBuilder = new StringBuilder();
            foreach (var userClaim in User.Claims)
            {
                userInfoBuilder.Append($"{userClaim.Type}: {userClaim.Value} * ");
            }
            return Ok(new[] { "secret1", "secret2", userInfoBuilder.ToString() });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            return Ok($"secret with id {id}");
        }
    }
}