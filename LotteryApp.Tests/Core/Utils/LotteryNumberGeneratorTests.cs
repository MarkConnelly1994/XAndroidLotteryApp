using NUnit.Framework;
using LotteryApp.Core.Utils;
using LotteryApp.Core.Utils.LotteryApp.Core.Utils;
using System.Linq;

namespace LotteryApp.Tests
{
    [TestFixture]
    public class LotteryNumberGeneratorTests
    {
        [Test]
        public void GenerateRandomNumbers_ShouldReturnCorrectCount()
        {
            // Arrange
            int count = 7;
            int min = 1;
            int max = 59;

            // Act
            var result = LotteryNumberGenerator.GenerateRandomNumbers(count, min, max);

            // Assert
            Assert.AreEqual(count, result.Length, "The generated number count is incorrect.");
        }

        [Test]
        public void GenerateRandomNumbers_ShouldReturnUniqueNumbers()
        {
            // Arrange
            int count = 7;
            int min = 1;
            int max = 59;

            // Act
            var result = LotteryNumberGenerator.GenerateRandomNumbers(count, min, max);

            // Assert
            CollectionAssert.AllItemsAreUnique(result, "The generated numbers are not unique.");
        }

        [Test]
        public void GenerateRandomNumbers_ShouldReturnNumbersWithinRange()
        {
            // Arrange
            int count = 7;
            int min = 1;
            int max = 59;

            // Act
            var result = LotteryNumberGenerator.GenerateRandomNumbers(count, min, max);

            // Assert
            Assert.IsTrue(result.All(number => number >= min && number <= max), "Some numbers are out of the specified range.");
        }
    }
}
