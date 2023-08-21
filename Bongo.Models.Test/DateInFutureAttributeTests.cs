using Bongo.Models.ModelValidations;
using NUnit.Framework;

namespace Bongo.Models.Test
{
    [TestFixture]
    public class DateInFutureAttributeTests
    {
        [Test]
        [TestCase(100, ExpectedResult = true)]
        [TestCase(-100, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        public bool DateValidator_ExpectedDateRange_DateValidity(int addTime)
        {
            DateInFutureAttribute dateInFutureAttribute = new(() => DateTime.Now);
            return dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(addTime));
        }
        [Test]
        public void DateValidator_AnyDate_ReturnErrorMessage()
        {
            var result = new DateInFutureAttribute();
            Assert.AreEqual("Date must be in the future", result.ErrorMessage);
        }
    }
}
