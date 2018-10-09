using System;
using Moq;
using WebApi.Logic;
using WebApi.Model;
using WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using WebApi.Exceptions;

namespace WebApi.Tests.Logic
{
    [TestClass]
    public class ExerciseLogicTests
    {
        Mock<IRandomNumberGenerator> _randomNumberGenerator = new Mock<IRandomNumberGenerator>();

        [TestMethod]
        public void ExerciseLogic_CreateExercise_Set_TimeLimit_As_Per_Session_Rank()
        {
            // Arrange
            State.Exercises.Clear();
            _randomNumberGenerator.Setup(r => r.GenerateNumbers()).Returns(new Tuple<int, int>(2, 3));
            var session = new Session() { Id = Guid.NewGuid(), Rank = RankEnum.Expert, Level = 1 };
            ExerciseLogic exerciseLogic = new ExerciseLogic(_randomNumberGenerator.Object);

            // Act
            var exercise = exerciseLogic.CreateExercise(session);

            // Assert
            Assert.IsNotNull(exercise);
            Assert.AreEqual(exercise.Expression1, 2);
            Assert.AreEqual(exercise.Expression2, 3);
            Assert.AreEqual(exercise.TimeLimit, State.RankTimeLimit[RankEnum.Expert]);
            Assert.IsTrue(exercise.CreatedDateTime < DateTime.Now && exercise.CreatedDateTime > DateTime.Now.AddSeconds(-5));

        }

        [TestMethod]
        [ExpectedException(typeof(UniqueExerciseNotAvailableException))]
        public void ExerciseLogic_CreateExercise_Should_Not_Reuse_Active_Excercise()
        {
            // Arrange
            State.Exercises.Clear();
            _randomNumberGenerator.Setup(r => r.GenerateNumbers()).Returns(new Tuple<int, int>(2, 3));
            var session = new Session() { Id = Guid.NewGuid(), Rank = RankEnum.Beginner, Level = 1 };
            ExerciseLogic exerciseLogic = new ExerciseLogic(_randomNumberGenerator.Object);

            // Act
            var exercise = exerciseLogic.CreateExercise(session);

            // Assert
            Assert.IsNotNull(exercise);
            
            // Act
            var exercise2 = exerciseLogic.CreateExercise(session);

        }

        [TestMethod]
        public void ExerciseLogic_RemoveExercise_Success()
        {
            // Arrange
            State.Exercises.Clear();
            _randomNumberGenerator.Setup(r => r.GenerateNumbers()).Returns(new Tuple<int, int>(2, 3));
            var session = new Session() { Id = Guid.NewGuid(), Rank = RankEnum.Beginner, Level = 1 };
            ExerciseLogic exerciseLogic = new ExerciseLogic(_randomNumberGenerator.Object);
            var exercise = exerciseLogic.CreateExercise(session);
            Assert.IsNotNull(exercise);
            Assert.IsTrue(State.Exercises.Count == 1);
            
            // Act
            State.Exercises.Remove(exercise);


            // Assert
            Assert.IsTrue(State.Exercises.Count == 0);
        }



    }
}
