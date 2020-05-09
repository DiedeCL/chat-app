using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorld.Controller
{
    public class PlaystationController : ApiController
    {
        [HttpGet]
        public string LaunchBomb(int id)
        {
            return "Bomb A launched!";
        }

        [HttpGet]
        // attribute routing with 2 routes
        [Route("api/ar/Playstation/LaunchBombB")]
        [Route("api/ar/Playstation2/LaunchBombB")]
        public string AnotherBomb()
        {
            return "Bomb B launched!";
        }

        [HttpGet]
        public string BombV1()
        {
            return "Bomb v1 launched!";
        }

        [HttpGet]
        public string BombV2()
        {
            return "Bomb v2 launched!";
        }

        [HttpGet]
        public string LaunchRocket()
        {
            return "Rocket launched!";
        }

        [HttpGet]
        public string LaunchKimJongUnBomb()
        {
            return "Kim Jong-Un bomb launched!";
        }

        // attribute routing
        // collide with Playstation2 controller
        [HttpGet] 
        [Route("api/ar/duplicatedInControllers")]
        public string Duplicated()
        {
            return "Bomb launched from playstation!";
        }


        // collision between actions in same controller
        [HttpGet]
        [Route("api/ar/duplicatedInSameController")]
        public string DuplicatedV2A()
        {
            return "Bomb launched from duplicatedV2A";
        }
        [HttpGet]
        [Route("api/ar/duplicatedInSameController")]
        public string DuplicatedV2B()
        {
            return "Bomb launched from duplicatedV2B";
        }


        // collide with Convention Based Routing - PlaystationAPIv1
        [HttpGet] 
        [Route("api/v1/")]
        public string TheBomb()
        {
            return "The bomb was launched!!!!!!!";
        }
    }
}
