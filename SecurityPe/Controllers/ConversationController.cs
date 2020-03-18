using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using SecurityPe.Data;
using SecurityPe.Domain;

namespace SecurityPe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConversationController : Controller
    {
        [Route("{action}")]
        public IActionResult Conversations(int id)
        {
            return Json(JsonSerializer.Serialize(2));

        }
    
        
    }
}
    