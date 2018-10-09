using WebApi.Model;

namespace WebApi.Logic
{
    public interface IExerciseLogic
    {
        Exercise CreateExercise(Session session, int attempt = 1);
        void RemoveExercise(Exercise exercise);
    }
}