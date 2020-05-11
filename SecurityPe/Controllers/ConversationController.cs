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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading;
using System.Web;
using System.Web.Http;
using Microsoft.AspNetCore.Http;

//using Microsoft.AspNetCore.Http;

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
        private readonly ILogger<ConversationController> _logger;
        private FileService _fileService;

        public ConversationController(UserManager<User> userManager, SqlUserKeyData keyData,
             MessageService messageService, ChatAppContext context, ILogger<ConversationController> logger, FileService fileService)
        {
            _userManager = userManager;
            _keyData = keyData;
            _messageService = messageService;
            _context = context;
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel model)
        {
            User sender = await FindByEmailAsync(model.SenderEmail);

            if (sender == null)
            {
                _logger.LogCritical($"SendMessage -  Sender with {model.SenderEmail} doesn't exist");
                return BadRequest("Sender doesn't exits");
            }

            if (model.ReceiverEmail == String.Empty)
            {
                _logger.LogCritical($"SendMessage - {sender.Email} Failed to send a messages to {model.ReceiverEmail} receiver doesn't exist");
                return BadRequest("Receiver doesn't exist");
            }

            if (model.Message == string.Empty)
            {
                _logger.LogCritical($"SendMessage - {sender.Email} Failed to send a messages to {model.ReceiverEmail} Message was empty");
                return BadRequest("Message is empty");
            }

            var messageSendSucceeded = _messageService.SendMessage(model.Message, model.ReceiverEmail, sender, model.ConversationId);
            if (messageSendSucceeded)
            {
                _logger.LogInformation($"SendMessage - {sender.Email} send a messages to {model.ReceiverEmail}");
                return Ok("Message Send");
            }
            _logger.LogCritical($"SendMessage - {sender.Email} Failed to send a messages to {model.ReceiverEmail}");
            return BadRequest("Failed to send message");
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> CreatedConversation([FromBody] CreateConversationModel model)
        {
            User sender = await FindByEmailAsync(model.SenderEmail);
            User receiver = await FindByEmailAsync(model.ReceiverEmail);
            var succeeded = _messageService.CreateNewConversation(sender, receiver);
            if (succeeded)
            {
                _logger.LogInformation($"CreatedConversation - Conversation started between {model.SenderEmail} and {model.ReceiverEmail}");
                return Ok();
            }
            _logger.LogError($"CreatedConversation - Conversation failed to start between {model.SenderEmail} and {model.ReceiverEmail}");
            return BadRequest();
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> GetConversationInformationById([FromBody] ConversationInformationModel model)
        {
            var userConversationsWithThisId = _context.UserConversations
                .Where(uc => uc.ConversationId == model.ConversationID).ToList();
            var currentUser = await FindByEmailAsync(model.EmailOfCurrentUser);
            List<User> users = new List<User>();
            List<int> userIds = new List<int>();
            userConversationsWithThisId.ForEach(uc => userIds.Add(uc.UserId));
            for (int i = 0; i < userIds.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(userIds[i].ToString());
                users.Add(user);
            }
            var otherUserOfConversation = users.FirstOrDefault(u => u.Email.ToUpper() != model.EmailOfCurrentUser.ToUpper());
            if (otherUserOfConversation == null)
            {
                return BadRequest("conversation doesn't exist");
            }
            return Ok(new
            {
                Email = otherUserOfConversation.Email,
                ConversationId = model.ConversationID

            });
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> GetConversationById([FromBody] ConversationModel model)
        {
            var receiver = await FindByEmailAsync(model.ReceiversEmail);
            var messages = new List<ReturnMessage>();
            
            _context.Messages.Where(m => m.ConversationId == model.ConversationID).ToList().ForEach(async me =>
                {
                    var content = DecryptWithAes(me, receiver);
                        if (content.Equals(String.Empty))
                        {
                            var sender = await FindByEmailAsync(model.SendersEmail);
                            content = DecryptWithAes(me, sender);
                        }

                        var rMessage = new ReturnMessage
                        {
                            MessageId = me.Id,
                            Content = content,
                            ConversationId = me.ConversationId,
                            EmailOfSender = me.EmailOfSender,
                            DataIsTrusted = EncryptionServices.VerifyData
                            (content,
                                _context.PublicKeyStores.FirstOrDefault(store => store.Email == me.EmailOfSender)
                                    ?.PublicKey, me.SignedData)

                        };

                        messages.Add(rMessage);

                }
            );
           
            return Ok(messages);
            
            
        }
        
        private static string DecryptWithAes(Message me, User user)
        {
            var key = EncryptionServices.DecryptWithRsa(me.EncryptedAesKey, user.PrivateKey);
            var iv =Convert.FromBase64String(me.EncryptedAesIV);
            var decryptWithAes = EncryptionServices.DecryptWithAes(
            Convert.FromBase64String(me.EncryptedContentOfMessage), key, iv);
            return decryptWithAes.Length == 0 ? string.Empty : decryptWithAes;
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
        [HttpPost("{action}")]
        public async Task<IActionResult> StoreFile([FromBody] StoreFileModel model)
        {
            try
            {
                var storedFile = new StoredFile
                {
                    FilePath = model.filePath
                };
                _context.Conversations.Where(c => c.Id == model.ConversationId).FirstOrDefault()?.Files.Add(storedFile);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation($"The File {Path.GetFileName(model.filePath)} of conversation {model.ConversationId} is stored");
                return Ok("FileStored");

            }
            catch (Exception e)
            {
                _logger.LogError($"The File {Path.GetFileName(model.filePath)} of conversation {model.ConversationId} couldn't be stored: {e.Message}");
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
        [HttpPost("{action}")]
        public async Task<IActionResult> SendFile([FromBody] MessageModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.SenderEmail);
                _messageService.SendMessage(model.Message ,model.ReceiverEmail, user, model.ConversationId);
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
           
        }

        [HttpPost("{action}")]
        public async Task<IActionResult> GetAllFilesOfCOnversation([FromBody] GetFilesModel model)
        {
            var files = _context.StoredFiles.Where(f => f.ConversationId == model.ConversationId).ToList();
            var rFiles = new List<ReturnFile>();
            files
                .ForEach(f => rFiles.Add(new ReturnFile
                {
                    FileId = f.Id,
                    FileName = Path.GetFileName(f.FilePath)
                }));

            return Ok(rFiles);
        }

       


        [HttpPost("{action}")]
        public async Task<IActionResult> UploadFile()
        {
            try{
                var fileuploadPath = @"C:\Users\diede\Documents\School\Security\SecurityPE\SecurityPe\Files\";
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(fileuploadPath, fileName);
                   

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    
                    return Ok(fullPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("{action}")]
        public HttpResponseMessage GetFile([FromBody] DownloadFileModel model)
        {
            string filePath = _fileService.GetFilePathById(model.id);
            string downloadedFileName = Path.GetFileName(filePath);

            //converting Pdf file into bytes array  
            var dataBytes = System.IO.File.ReadAllBytes(filePath);
            //adding bytes to memory stream   
            var dataStream = new MemoryStream(dataBytes);
            var bufferedStream = new BufferedStream(dataStream);

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(dataStream);
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = downloadedFileName;
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return httpResponseMessage;

        }


        private async Task<User> FindByEmailAsync(string mail)
        {
            return await _userManager.FindByEmailAsync(mail);
        }


    }
}