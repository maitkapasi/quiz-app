using WebApi.Model;

namespace WebApi.Logic
{
    public interface ISessionLogic
    {
        Session CreateSession();
        void EndSession(Session session);
    }
}