using LotteryApp.Core.Models;
using System.Linq;

namespace LotteryApp.Core.Utils
{
    public static class LotteryNumberComparer
    {
        public static bool AreNumbersMatching(int[] generatedNumbers, LotteryDrawModel draw)
        {
            var drawNumbers = new[]
            {
                draw.Number1,
                draw.Number2,
                draw.Number3,
                draw.Number4,
                draw.Number5,
                draw.Number6,
                draw.BonusBall
            };

            return generatedNumbers.OrderBy(n => n).SequenceEqual(drawNumbers.OrderBy(n => n));
        }
    }
}
