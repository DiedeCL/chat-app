using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorld.Controller
{
    public class Playstation2Controller : ApiController
    {
        [HttpGet]
        public string BombV2()
        {
            return "Bomb v2 launched from playstation2!";
        }

        [HttpGet]
        [Route("api/ar/duplicatedInControllers")]
        public string Duplicated()
        {
            return "Bomb launched from playstation2!";
        }


        // with placeholders
        [HttpGet]
        [Route("api/RequestBomb/{name}/{size}")]
        public string RequestBomb(int size, string name)
        {
            return "Launch bomb from RequestBomb.";
        }

        // again - collision with actions in same controller
        [HttpGet]
        [Route("api/RequestBombV2/{name}/{size}")]
        public string RequestBombV2a(int size, string name)
        {
            return "Launch bomb from RequestBombV2-A.";
        }
        [HttpGet]
        [Route("api/RequestBombV2/{name}/{size}")]
        public string RequestBombV2b(int size, string name)
        {
            return "Launch bomb from RequestBombV2-B.";
        }

        // with minimum value for size as 100
        [HttpGet]
        [Route("api/RequestBombV3/{name}/{size:int:min(100)}")]
        public string RequestBombV3(int size, string name)
        {
            return "Launch bomb from RequestBombV3.";
        }

        // with default value for size as 5000
        [HttpGet]
        [Route("api/RequestBombV4/{name}/{size:int=5000}", Order = 2)]
        public string RequestBombV4a(int size, string name)
        {
            return "Launch bomb from RequestBombV4a.";
        }
        [HttpGet]
        [Route("api/RequestBombV4/{name}/{size}", Order = 1)]
        public string aRequestBombV4b(int size, string name)
        {
            return "Launch bomb from RequestBombV4b.";
        }
    }
}
