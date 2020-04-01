using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityPe.Models;
using SecurityPe.Services;

namespace SecurityPe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class EncryptionController : ControllerBase
    {
        [HttpPost("EncryptWithRsa")]
        public IActionResult ServerPublicKey([FromBody] EncryptModel model)
        {
            EncryptionServices.EncryptWithRsa(model.Data, model.PrivateKey);

            return Ok();
        }
    }
}