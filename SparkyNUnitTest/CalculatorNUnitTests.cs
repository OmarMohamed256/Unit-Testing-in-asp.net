using NUnit.Framework;

namespace Sparky
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            // Arrange
            Calculator calculator = new();

            // Act
            int result = calculator.AddNumbers(10, 20);

            // Assert
            Assert.AreEqual(30, result);
        }

        [Test]
        [TestCase(5.4, 10.5)] // 15.9
        [TestCase(5.43, 10.53)] // 15.93
        [TestCase(5.49, 10.59)] // 16.08
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            // Arrange
            Calculator calculator = new();

            // Act
            double result = calculator.AddNumbersDouble(a, b);

            // Assert
            Assert.AreEqual(15.9, result, .2);
            // 15.7 - 16.1
        }

        [Test]
        public void IsOddChecker_InputEvenNumber_ReturnFalse()
        {
            // Arrange
            Calculator calculator = new();

            // Act
            bool IsOdd = calculator.IsOddNumber(10);

            // Assert
            Assert.That(IsOdd, Is.False);
            Assert.IsFalse(IsOdd);
        }

        [Test]
        [TestCase(11)]
        [TestCase(13)]
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            // Arrange
            Calculator calculator = new();

            // Act
            bool IsOdd = calculator.IsOddNumber(a);

            // Assert
            Assert.That(IsOdd, Is.EqualTo(true));
            Assert.IsTrue(IsOdd);
        }

        [Test]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(11, ExpectedResult = true)]
        public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a)
        {
            Calculator calculator = new();
            return calculator.IsOddNumber(a);
        }

        [Test]
        public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange()
        {
            Calculator calculator = new();
            List<int> expectedOddRange = new () { 5, 7, 9 }; // range from 5 - 10, inclusive of min and max values

            // Act
            List<int> result = calculator.GetOddRange(5, 10);

            // Assert
            Assert.That(result, Is.EquivalentTo(expectedOddRange));
            // Assert.AreEqual(expectedOddRange, result);
            // Assert.Contains(7, result);
            Assert.That(result, Does.Contain(7));
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result, Has.No.Member(6));
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }
    }
}
