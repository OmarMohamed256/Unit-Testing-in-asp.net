using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class CustomerXUnitTests
    {
        private Customer _customer;

        public CustomerXUnitTests()
        {
            _customer = new Customer();
        }

        [Fact]
        public void GreetAndCombineName_InputFirstAndLastName_ReturnFullName()
        {
            // Arrange


            // Act
            _customer.GreetAndCombineNames("Ben", "Spark");

            // Assert
            Assert.Equal("Hello, Ben Spark", _customer.GreetMessage);
            Assert.Contains("ben spark", _customer.GreetMessage.ToLower()); // Case sensitive
            Assert.StartsWith("Hello", _customer.GreetMessage); // Case sensitive
            Assert.EndsWith("Spark", _customer.GreetMessage); // Case sensitive
            Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _customer.GreetMessage);
        }

        [Fact]
        public void GreetMessage_NotGreeted_ReturnNull()
        {
            // Arrange


            // Act

            // Assert
            Assert.Null(_customer.GreetMessage);
        }

        [Fact]
        public void DiscountCheck_DefaultCostumer_ReturnDiscountInRange()
        {
            int result = _customer.Discount;

            Assert.InRange(result, 10, 25);
        }

        [Fact]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            _customer.GreetAndCombineNames("Ben", "");

            Assert.NotNull(_customer.GreetMessage);
            Assert.False(string.IsNullOrEmpty(_customer.GreetMessage));
        }

        [Fact]
        public void GreetMessage_EmptyFirstName_ThrowsException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => _customer.GreetAndCombineNames("", "Spark"));

            Assert.Equal("Empty First Name", exceptionDetails.Message);


            // Checks oly if an Exception has been throwed
            Assert.Throws<ArgumentException>(() => _customer.GreetAndCombineNames("", "Spark"));
        }

        [Fact]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
        {
            _customer.OrderTotal = 10;
            var result = _customer.GetCustomerDetails();

            Assert.IsType<BasicCustomer>(result);
        }

        [Fact]
        public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnBasicPlatinum()
        {
            _customer.OrderTotal = 101;
            var result = _customer.GetCustomerDetails();

            Assert.IsType<PlatinumCustomer>(result);
        }
    }
}
