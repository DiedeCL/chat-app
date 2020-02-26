using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SecurityPe.Data;

namespace SecurityPe.Controllers
{
    public class ConversationController : Controller
    {
        readonly TempData _data = new TempData();
        [Route("")]
        public IActionResult Users()
        {
            var json = JsonSerializer.Serialize(_data.GetUsers());
            return Json(json);

        }/*
        [Route("Address")] //of [Route("{action}")]
        public IActionResult Address()
        {
            return Content("An address");
        }*/
    }
}
    