using System;
using Moq;
using WebApi.Logic;
using WebApi.Model;
using WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace WebApi.Tests.Logic
{
    [TestClass]
    public class SessionLogicTests
    {
        [TestMethod]
        public void SessionLogic_CreateSession()
        {
            // Arrange
            State.Sessions.Clear();
            var sessionLogic = new SessionLogic();

            // Act
            var session = sessionLogic.CreateSession();

            // Assert
            Assert.AreEqual(session.Rank, RankEnum.Beginner);
            Assert.AreEqual(session.Level, 1);
            Assert.AreEqual(State.Sessions.Count, 1);

        }

        [TestMethod]
        public void SessionLogic_EndSession()
        {
            // Arrange
            State.Sessions.Clear();
            var sessionLogic = new SessionLogic();
            var session = sessionLogic.CreateSession();
            Assert.AreEqual(State.Sessions.Count, 1);

            // Act
            sessionLogic.EndSession(session);

            // Assert
            Assert.AreEqual(State.Sessions.Count, 0);
        }

    }
}
