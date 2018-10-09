using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Exceptions;
using WebApi.Model;

namespace WebApi.Logic
{
    public class ExerciseLogic : IExerciseLogic
    {
        IRandomNumberGenerator _randomNumberGenerator;
        public ExerciseLogic(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;

        }
        /// <summary>
        /// Creates an excercise for the supplied session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Exercise CreateExercise(Session session, int attempt = 1)
        {
            if(attempt > 100) // 100 can be changed to required value to prevent looping and StackOverflowException.
                throw new UniqueExerciseNotAvailableException("Maximum number of attempts to create an exercises have reached. Please retry after sometime.");

            var randomNumbers = _randomNumberGenerator.GenerateNumbers();
            var expression1 = randomNumbers.Item1;
            var expression2 = randomNumbers.Item2;

            var currentExerciseWithTheSameExpressions = State.Exercises.FirstOrDefault(e =>
                                                                            ((e.Expression1 == expression1 && e.Expression2 == expression2) ||
                                                                            (e.Expression1 == expression2 && e.Expression2 == expression1)) &&
                                                                            e.CreatedDateTime >= DateTime.Now.AddSeconds(-30));
            if(currentExerciseWithTheSameExpressions != null)
            {
                return CreateExercise(session, ++attempt);
            }
            else
            {
                var exercise = new Exercise();
                exercise.Id = Guid.NewGuid();
                exercise.Expression1 = expression1;
                exercise.Expression2 = expression2;
                exercise.TimeLimit = State.RankTimeLimit[session.Rank];
                exercise.AssignedSession = session.Id;
                exercise.CreatedDateTime = DateTime.Now;
                
                State.Exercises.Add(exercise);

                return exercise;
            }
            
        }

        public void RemoveExercise(Exercise exercise)
        {
            State.Exercises.Remove(exercise);
        }

    }
}
