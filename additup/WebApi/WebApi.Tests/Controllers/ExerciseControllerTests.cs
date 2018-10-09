using System;
using Moq;
using WebApi.Logic;
using WebApi.Model;
using WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class ExerciseControllerTests
    {
        Mock<IExerciseLogic> _exerciseLogic = new Mock<IExerciseLogic>();
        Mock<ISessionLogic> _sessionLogic = new Mock<ISessionLogic>();
        Session session;
        Exercise exercise;


        public ExerciseControllerTests()
        {
            session = new Session() { Id = Guid.NewGuid(), Rank = RankEnum.Beginner, Level = 1 };
            State.Sessions.Add(session);
            exercise = new Exercise() { Id = Guid.NewGuid(), Expression1 = 2, Expression2 = 3, TimeLimit = 30, AssignedSession = session.Id, CreatedDateTime = DateTime.Now };
        }

        [TestMethod]
        public void GetExercise_Valid_Session_200()
        {
            // Arrange
            _exerciseLogic.Setup(c => c.CreateExercise(session, 1)).Returns(exercise);
            ExerciseController ec = new ExerciseController(_exerciseLogic.Object, _sessionLogic.Object);

            // Act 
            var result = (ObjectResult)ec.GetExercise(session.Id.Value);

            // Assert
            Assert.AreEqual(result.Value, exercise);
            Assert.AreEqual(result.StatusCode, 200);

        }

        [TestMethod]
        public void GetExercise_Invalid_Session_404()
        {
            // Arrange
            _exerciseLogic.Setup(c => c.CreateExercise(session, 1)).Returns(exercise);
            ExerciseController ec = new ExerciseController(_exerciseLogic.Object, _sessionLogic.Object);

            // Act 
            var result = (StatusCodeResult)ec.GetExercise(Guid.NewGuid());

            // Assert
            Assert.AreEqual(result.StatusCode, 404);

        }

        [TestMethod]
        public void PostAnswer_Correct_Answer_Moves_To_Next_Level_200()
        {
            // Arrange
            State.Exercises.Add(exercise);
            ExerciseController ec = new ExerciseController(_exerciseLogic.Object, _sessionLogic.Object);

            // Act 
            var correctAnswer = new Answer() { ExerciseId = exercise.Id, SessionId = session.Id, SubmittedAnswer = "5" };
            var result = (ObjectResult)ec.PostAnswer(exercise.Id.Value, correctAnswer);

            // Assert
            Assert.AreEqual(result.StatusCode, 200);
            var expectedResult = new Result() { AllLevelCompleted = false, AnswerCorrect = true, Rank = RankEnum.Beginner, Level = 2 };
            Assert.AreEqual(expectedResult, result.Value);

        }

        [TestMethod]
        public void PostAnswer_Should_Remove_Question_After_Answer_Submission_400()
        {
            // Arrange
            State.Exercises.Clear();
            State.Exercises.Add(exercise);
            ExerciseController ec = new ExerciseController(_exerciseLogic.Object, _sessionLogic.Object);
            _exerciseLogic.Setup(e => e.RemoveExercise(exercise));

            // Act 
            var correctAnswer = new Answer() { ExerciseId = exercise.Id, SessionId = session.Id, SubmittedAnswer = "5" };
            var result = (ObjectResult)ec.PostAnswer(exercise.Id.Value, correctAnswer);

            // Assert
            Assert.AreEqual(result.StatusCode, 200);
            _exerciseLogic.Verify(m => m.RemoveExercise(exercise), Times.Once());

        }

        [TestMethod]
        public void PostAnswer_Correct_Answer_Three_Times_Moves_To_Next_Rank_And_Reset_Level_200()
        {
            // Arrange
            ExerciseController ec = new ExerciseController(_exerciseLogic.Object, _sessionLogic.Object);
            var exercise1 = new Exercise() { Id = Guid.NewGuid(), Expression1 = 2, Expression2 = 3, TimeLimit = 30, AssignedSession = session.Id, CreatedDateTime = DateTime.Now };
            State.Exercises.Add(exercise1);
            

            // Act 
            var correctAnswer1 = new Answer() { ExerciseId = exercise1.Id, SessionId = session.Id, SubmittedAnswer = "5" };
            var result = (ObjectResult)ec.PostAnswer(exercise1.Id.Value, correctAnswer1);

            // Assert
            Assert.AreEqual(result.StatusCode, 200);
            var expectedResult = new Result() { AllLevelCompleted = false, AnswerCorrect = true, Rank = RankEnum.Beginner, Level = 2 };
            Assert.AreEqual(expectedResult, result.Value);

            // Arrange 
            var exercise2 = new Exercise() { Id = Guid.NewGuid(), Expression1 = 23, Expression2 = 3, TimeLimit = 30, AssignedSession = session.Id, CreatedDateTime = DateTime.Now };
            State.Exercises.Add(exercise2);

            // Act
            var correctAnswer2 = new Answer() { ExerciseId = exercise2.Id, SessionId = session.Id, SubmittedAnswer = "26" };
            var result2 = (ObjectResult)ec.PostAnswer(exercise2.Id.Value, correctAnswer2);

            // Assert
            Assert.AreEqual(result2.StatusCode, 200);
            var expectedResult2 = new Result() { AllLevelCompleted = false, AnswerCorrect = true, Rank = RankEnum.Beginner, Level = 3 };
            Assert.AreEqual(expectedResult2, result2.Value);

            // Arrange 
            var exercise3 = new Exercise() { Id = Guid.NewGuid(), Expression1 = 2, Expression2 = 1, TimeLimit = 30, AssignedSession = session.Id, CreatedDateTime = DateTime.Now };
            State.Exercises.Add(exercise3);

            // Act
            var correctAnswer3 = new Answer() { ExerciseId = exercise3.Id, SessionId = session.Id, SubmittedAnswer = "3" };
            var result3 = (ObjectResult)ec.PostAnswer(exercise3.Id.Value, correctAnswer3);

            // Assert
            Assert.AreEqual(result3.StatusCode, 200);
            var expectedResult3 = new Result() { AllLevelCompleted = false, AnswerCorrect = true, Rank = RankEnum.Talented, Level = 1 };
            Assert.AreEqual(expectedResult3, result3.Value);

        }

        [TestMethod]
        public void PostAnswer_Incorrect_Answer_Does_Not_Increase_Level_And_Ends_Session_200()
        {
            // Arrange
            State.Exercises.Add(exercise);
            ExerciseController ec = new ExerciseController(_exerciseLogic.Object, _sessionLogic.Object);
            _sessionLogic.Setup(c => c.EndSession(session));

            // Act 
            var incorrectAnswer = new Answer() { ExerciseId = exercise.Id, SessionId = session.Id, SubmittedAnswer = "50" };
            var result = (ObjectResult)ec.PostAnswer(exercise.Id.Value, incorrectAnswer);

            // Assert
            Assert.AreEqual(result.StatusCode, 200);
            var expectedResult = new Result() { AllLevelCompleted = false, AnswerCorrect = false, Rank = RankEnum.Beginner, Level = 1 };
            Assert.AreEqual(expectedResult, result.Value);
            _sessionLogic.Verify(mock => mock.EndSession(session), Times.Once());

        }

        [TestMethod]
        public void PostAnswer_Last_Correct_Answer_Ends_Session_200()
        {
            // Arrange
            var newSession = new Session() { Id = Guid.NewGuid(), Rank = RankEnum.Expert, Level = 3 };
            State.Sessions.Add(newSession);


            var lastExercise = new Exercise() { Id = Guid.NewGuid(), Expression1 = 2, Expression2 = 3, TimeLimit = 30, AssignedSession = newSession.Id, CreatedDateTime = DateTime.Now };
            State.Exercises.Add(lastExercise);
            ExerciseController ec = new ExerciseController(_exerciseLogic.Object, _sessionLogic.Object);
            _sessionLogic.Setup(c => c.EndSession(newSession));

            // Act 
            var correctAnswer = new Answer() { ExerciseId = lastExercise.Id, SessionId = newSession.Id, SubmittedAnswer = "5" };
            var result = (ObjectResult)ec.PostAnswer(lastExercise.Id.Value, correctAnswer);

            // Assert
            Assert.AreEqual(result.StatusCode, 200);
            var expectedResult = new Result() { AllLevelCompleted = true, AnswerCorrect = true, Rank = RankEnum.Expert, Level = 3 };
            Assert.AreEqual(expectedResult, result.Value);
            _sessionLogic.Verify(mock => mock.EndSession(newSession), Times.Once());

        }

    }
}
