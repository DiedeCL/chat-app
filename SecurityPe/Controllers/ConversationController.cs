using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecurityPe.Data;
using SecurityPe.Domain;
using SecurityPe.Models;
using SecurityPe.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SecurityPe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConversationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SqlUserKeyData _keyData;
        private readonly MessageService _messageService;
        private ChatAppContext _context;

        public ConversationController(UserManager<User> userManager, SqlUserKeyData keyData,
             MessageService messageService, ChatAppContext context)
        {
            _userManager = userManager;
            _keyData = keyData;
            _messageService = messageService;
            _context = context;
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel model)
        {
            User sender = await FindByEmailAsync(model.SenderEmail);

            if (sender == null)
            {
                return BadRequest("Sender doesn't exits");
            }

            if (model.ReceiverEmail == String.Empty) return BadRequest("Receiver doesn't exist");

            if (model.Message == string.Empty)
            {
                return BadRequest("Message is empty");
            }

            var messageSendSucceeded = _messageService.SendMessage(model.Message, model.ReceiverEmail, sender, model.ConversationId);
            if (messageSendSucceeded) return Ok("Message Send");
            else return BadRequest("Failed to send message");
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> CreatedConversation([FromBody] CreateConversationModel model)
        {
            User sender = await FindByEmailAsync(model.SenderEmail);
            User receiver = await FindByEmailAsync(model.ReceiverEmail);
            var succeeded = _messageService.CreateNewConversation(sender, receiver);
            if (succeeded) return Ok();
            else return BadRequest();
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> GetConversationById([FromBody] ConversationModel model)
        {
            var user = await FindByEmailAsync(model.Email);

            var messages = _context.Messages.Where(m => m.ConversationId == model.ConversationID).Select(me =>
                 new
                {
                    me.Id,
                    content = DecryptWithAes(me, user),
                    me.ConversationId,
                    me.EmailOfSender,
                    dataIsTrusted = EncryptionServices.VerifyData
                        (DecryptWithAes(me, user)
                        ,_context.PublicKeyStores.FirstOrDefault(store => store.Email == me.EmailOfSender).PublicKey 
                        , me.SignedData)
                }
            ).ToList();
            return Ok(messages);
        }
        
        private static string DecryptWithAes(Message me, User user)
        {
            var key = EncryptionServices.DecryptWithRsa(me.EncryptedAesKey, user.PrivateKey);
            var iv =Convert.FromBase64String(me.EncryptedAesIV); //EncryptionServices.DecryptWithRsa(me.EncryptedAesIV, user.PrivateKey);
            return EncryptionServices.DecryptWithAes(
                Convert.FromBase64String(me.EncryptedContentOfMessage), key, iv);
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> GetAllConversationsOfAUser([FromBody] UserModel model)
        {
            var user = await FindByEmailAsync(model.Email);
            var userConversations = _context.UserConversations.Where(us => us.UserId == user.Id).Select(us => new
            {
                us.Id,
                us.ConversationId,
                us.UserId
            }) .ToList();

            return Ok(userConversations);
        }

        private async Task<User> FindByEmailAsync(string mail)
        {
            return await _userManager.FindByEmailAsync(mail);
        }
    }
}