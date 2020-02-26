using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SecurityPe.Data;
using SecurityPe.Domain;

namespace SecurityPe.Controllers
{
    public class ConversationController : Controller
    {
        readonly TempData _data = new TempData();
        [Route("{action}")]
        public IActionResult Users()
        {
            var json = JsonSerializer.Serialize(_data.GetUsers());
            return Json(json);

        }
        
        
        [Route("{action}")]
        public IActionResult Conversations(int id)
        {
            return Json(JsonSerializer.Serialize(_data.GetConversations()));

        }
        [Route("{action}/{id}/{msg}")]
        public IActionResult AddMessage(int id, string msg)
        {
            _data.AddMessagesToConversation(id, msg);
            return Json(JsonSerializer.Serialize(_data.GetMessagesOfConversation(id)));

        }
        [Route("{action}/{id}")]
        public IActionResult GetMessage(int id)
        {
            return Json(JsonSerializer.Serialize(_data.GetMessagesOfConversation(id)));

        }
        
    }
}
    