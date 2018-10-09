using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Logic;

namespace WebApi.Controllers
{
    [Route("api/state")]
    public class StateController : Controller
    {
        public StateController()
        {

        }

		/*
        [HttpGet("active")]
        public IActionResult GetActiveState()
        {
            var activeExercises = State.Exercises.Where(e => e.CreatedDateTime >= DateTime.Now.AddSeconds(-30));
            var activeSessions = State.Sessions.Where(s => activeExercises.Any(ae => ae.AssignedSession == s.Id));
            return Ok(new { activeExercises, activeSessions });
        }
		*/
    }
}