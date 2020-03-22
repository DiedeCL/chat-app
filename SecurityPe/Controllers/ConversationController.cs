using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using SecurityPe.Data;
using SecurityPe.Domain;
using SecurityPe.Models;
using SecurityPe.Services;

namespace SecurityPe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConversationController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SqlUserKeyData _keyData;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ServerData _serverData;

        public ConversationController(UserManager<User> userManager, SqlUserKeyData keyData,
            IPasswordHasher<User> passwordHasher, ServerData serverData)
        {
            _userManager = userManager;
            _keyData = keyData;
            _passwordHasher = passwordHasher;
            _serverData = serverData;
        }

        [Route("{action}")]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel model)
        {
            User sender = await _userManager.FindByEmailAsync(model.SenderEmail);
            var publicRsaKey = _keyData.GetPublicKey((await _userManager.FindByEmailAsync(model.SenderEmail)).Id);
            if (sender == null)
            {
                return BadRequest("Sender doesn't exits");
            }

            if (publicRsaKey == null) return BadRequest("Receiver doesn't exist");

            if (model.Message == string.Empty)
            {
                return BadRequest("Message is empty");
            }

            return Ok();



        }

        [Route("{action")]
        public IActionResult CreatedConversation()
        {
            return Ok();
        }
    }
}