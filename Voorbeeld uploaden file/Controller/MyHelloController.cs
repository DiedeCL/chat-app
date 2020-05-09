using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorld.Controller
{
    public class MyHelloController : ApiController
    {
        public string Get()
        {
            return "Hello World from Web API";
        }

        [HttpGet]
        public string SayHi()
        {
            return "From say Hi!!!";
        }
    }
}