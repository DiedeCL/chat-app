using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using SecurityPe.Data;
using SecurityPe.Domain;

namespace SecurityPe.Controllers
{
    public class ConversationController : Controller
    {
        /*//ChatAppContext _context = new ChatAppContext();
        [Route("{action}")]
        public IActionResult Users()
        {
            
            // var json = JsonSerializer.Serialize(_context.Users);
            // return Json(json);

        }
        
        
        [Route("{action}")]
        public IActionResult Conversations(int id)
        {
            // return Json(JsonSerializer.Serialize(_context.Conversations));

        }*/
        /*[Route("{action}/{id}/{msg}")]
        public IActionResult AddMessage(int id, string msg)
        {
            
            _data.AddMessagesToConversation(id, msg);
            return Json(JsonSerializer.Serialize(_data.GetMessagesOfConversation(id)));

        }
        [Route("{action}/{id}")]
        public IActionResult GetMessage(int id)
        {
            return Json(JsonSerializer.Serialize(_data.GetMessagesOfConversation(id)));

        }*/
        
    }
}
    