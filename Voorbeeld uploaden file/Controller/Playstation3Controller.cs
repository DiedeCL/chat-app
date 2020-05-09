using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorld.Controller
{
    [RoutePrefix("PS3")]
    public class Playstation3Controller : ApiController
    {
        [HttpGet]
        [Route("Bommmb")]
        public string Bommmb()
        {
            return "Bomb launched from playstation3!";
        }

        // multiple HTTP verbs
        [AcceptVerbs("Get", "Head", "MKCOL", "Connect")]
        public string GenericBomb()
        {
            return "Generic Bomb!";
        }

        // override action name
        [ActionName("MiniBomb")]
        public string TheGreatBomb()
        {
            return "Generic Bomb!";
        }

        // prevent method to behave as an action
        [NonAction]
        public string LaBomba()
        {
            return "This is no action!!!";
        }
    }
}
