using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Logic
{
    public class SessionLogic : ISessionLogic
    {
        public Session CreateSession()
        {
            var session = new Session();
            session.Id = Guid.NewGuid();
            session.Rank = RankEnum.Beginner;
            session.Level = 1;

            State.Sessions.Add(session);

            return session;
        }

        public void EndSession(Session session)
        {
            State.Sessions.Remove(session);
        }

        

       
    }
}
