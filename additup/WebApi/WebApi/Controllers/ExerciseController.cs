using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Exceptions;
using WebApi.Logic;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/exercise")]
    public class ExerciseController : Controller
    {
        private IExerciseLogic _exerciseLogic;
        private ISessionLogic _sessionLogic;
        public ExerciseController(IExerciseLogic exerciseLogic, ISessionLogic sessionLogic)
        {
            _exerciseLogic = exerciseLogic;
            _sessionLogic = sessionLogic;

        }
        [HttpGet]
        public IActionResult GetExercise([FromQuery]Guid sessionId)
        {
            var session = State.Sessions.FirstOrDefault(u => u.Id == sessionId);

            if (session == null)
                return NotFound();

            try
            {
                var exercise = _exerciseLogic.CreateExercise(session);
                return Ok(exercise);

            }
            catch (UniqueExerciseNotAvailableException)
            {
                HttpContext.Response.Headers.Add(HttpResponseHeader.RetryAfter.ToString(), "30");
                return StatusCode(503);
            }
            
        }

        [HttpPost("{exerciseId}")]
        public IActionResult PostAnswer([FromRoute]Guid exerciseId, [FromBody] Answer answer)
        {
            var result = new Result();

            if (exerciseId != answer.ExerciseId)
                return BadRequest();

            var session = State.Sessions.FirstOrDefault(s => s.Id == answer.SessionId);

            if (session == null)
                return BadRequest();

            var exercise = State.Exercises.FirstOrDefault(e => e.Id == answer.ExerciseId.Value && e.AssignedSession == answer.SessionId);

            if(exercise == null)
                return NotFound();

            result.AnswerCorrect = CheckAnswer(exercise, answer.SubmittedAnswer);

            if (result.AnswerCorrect.Value)
            {
                bool completedAllStages = MoveToNextLevel(session);
                result.AllLevelCompleted = completedAllStages;
            } else
            {
                result.AllLevelCompleted = false;
            } 
            
            result.Rank = session.Rank;
            result.Level = session.Level;

            _exerciseLogic.RemoveExercise(exercise);

            if(!result.AnswerCorrect.Value || result.AllLevelCompleted.Value)
                _sessionLogic.EndSession(session);
            
            return Ok(result);
        }

        private bool CheckAnswer(Exercise exercise, string submittedAnswer)
        {
            var correctAnswer = exercise.Expression1 + exercise.Expression2;

            int answer;
            if (int.TryParse(submittedAnswer, out answer))
            {
                var isCorrect = (correctAnswer == answer);
                return isCorrect;
            }

            return false;
        }

        public bool MoveToNextLevel(Session session)
        {
            bool completedAllStages = (session.Rank == RankEnum.Expert && session.Level >= 3);

            if (completedAllStages)
                return true;

            session.Level++;
            if (session.Level > 3)
            {
                session.Rank = GetNextRank(session.Rank);
                session.Level = 1;
            }

            return false;

        }

        private RankEnum GetNextRank(RankEnum currentRank)
        {

            switch (currentRank)
            {
                case RankEnum.Beginner:
                    return RankEnum.Talented;
                case RankEnum.Talented:
                    return RankEnum.Intermediate;
                case RankEnum.Intermediate:
                    return RankEnum.Advanced;
                case RankEnum.Advanced:
                    return RankEnum.Expert;
                case RankEnum.Expert:
                    return RankEnum.Expert;
                default:
                    throw new NotSupportedException("Unexpected Rank");
            }
        }
    }
}
