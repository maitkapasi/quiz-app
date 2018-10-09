using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Model;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class SessionControllerTests
    {
        [TestMethod]
        public void CreateSession_200()
        {
            Mock<ISessionLogic> sessionLogic = new Mock<ISessionLogic>();
            var newSession = new Model.Session() { Id = Guid.NewGuid(), Rank = RankEnum.Beginner, Level = 1 };
            sessionLogic.Setup(c => c.CreateSession()).Returns(newSession) ;
            SessionController sc = new SessionController(sessionLogic.Object);

            var result = (ObjectResult)sc.CreateSession();
            Assert.AreEqual(result.Value, newSession);

        }

    }
}
