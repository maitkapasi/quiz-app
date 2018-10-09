using System;

namespace WebApi.Logic
{
    public interface IRandomNumberGenerator
    {
        Tuple<int, int> GenerateNumbers();
    }
}