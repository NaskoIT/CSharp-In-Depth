using NUnit.Framework;

namespace ExposeInternals.Tests
{
    [TestFixture]
    public class SummatorUnitTests
    {
        [Test]
        public void SumTwoPlusTwoShouldEqualFour()
        {
            var summator = new Summator();
            var result = summator.Sum(2, 2);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void SumMinusOneAndMinusOneShouldEqualMinusTwo()
        {
            var summator = new Summator();
            var result = summator.Sum(-1, -1);
            Assert.AreEqual(-2, result);
        }

        [Test]
        public void SumInt32MaxValueAndInt32MaxValueShouldProduceCorrectResult()
        {
            var summator = new Summator();
            var result = summator.Sum(int.MaxValue, int.MaxValue);
            Assert.AreEqual((long)int.MaxValue + int.MaxValue, result);
        }

        [Test]
        public void SumInt32MaxValueAndInt32MinValueShouldEqualMinusOne()
        {
            var summator = new Summator();
            var result = summator.Sum(int.MaxValue, int.MinValue);
            Assert.AreEqual(-1, result);
        }
    }
}