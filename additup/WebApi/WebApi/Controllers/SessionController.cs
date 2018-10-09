using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Logic;

namespace WebApi.Controllers
{
    [Route("api/session")]
    public class SessionController : Controller
    {
        private ISessionLogic _sessionLogic;
        public SessionController(ISessionLogic sessionLogic)
        {
            _sessionLogic = sessionLogic;

        }

        [HttpPost]
        public IActionResult CreateSession()
        {
            var session = _sessionLogic.CreateSession();
            return Ok(session);
        }
    }
}