using System;
using System.Linq;

namespace LotteryApp.Core.Utils
{
    namespace LotteryApp.Core.Utils
    {
        public static class LotteryNumberGenerator
        {
            public static int[] GenerateRandomNumbers(int count, int min, int max)
            {
                var random = new Random();
                return Enumerable.Range(min, max - min + 1)
                                 .OrderBy(x => random.Next())
                                 .Take(count)
                                 .ToArray();
            }
        }
    }

}

