using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Logic
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        Random _rnd;
        const int HIGHEST_RANDOM_NUMBER = 20;
        public RandomNumberGenerator()
        {
            _rnd = new Random();

        }
        public Tuple<int, int> GenerateNumbers()
        {
            return new Tuple<int, int>(_rnd.Next(1, HIGHEST_RANDOM_NUMBER), _rnd.Next(1, HIGHEST_RANDOM_NUMBER));

        }
    }
}
