using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi
{
    public static class State
    {
        static State()
        {
            Exercises = new List<Exercise>();
            Sessions = new List<Session>();
            RankTimeLimit = new Dictionary<RankEnum, int>();
            RankTimeLimit.Add(RankEnum.Beginner, 30);
            RankTimeLimit.Add(RankEnum.Talented, 29);
            RankTimeLimit.Add(RankEnum.Intermediate, 28);
            RankTimeLimit.Add(RankEnum.Advanced, 27);
            RankTimeLimit.Add(RankEnum.Expert, 26);
        }
        public static IList<Exercise> Exercises { get; set; }

        public static IList<Session> Sessions { get; set; }

        public static IDictionary<RankEnum, int> RankTimeLimit { get;  private set; }
    }
}
