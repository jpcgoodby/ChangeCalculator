using ChangeCalculator.Service.Validators;
using ChangeCalculator.Domain.Commands;

namespace ChangeCalculator.UnitTests.Validators
{
    public class PaymentAmountValidatorTests
    {
        private CalculateChangeValidator _validators;

        public PaymentAmountValidatorTests()
        {
            _validators = new CalculateChangeValidator();
        }

        [Fact]
        public void Given_Invalid_Empty_PaymentAmount_Argument_Validation_Fail()
        {
            var request = new CalculateChangeCommandRequest(new []{ "Sterling", "33.88", "" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Payment amount must be greater than zero.", result.Errors[0].ErrorMessage);

        }

        [Fact]
        public void Given_Invalid_PaymentAmount_Argument_Validation_Fail()
        {
            var request = new CalculateChangeCommandRequest(new[] { "Sterling", "33.88", "0" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Payment amount must be greater than zero.", result.Errors[0].ErrorMessage);

        }


        [Fact]
        public void Given_Valid_PaymentAmount_Argument_Validation_Success()
        {
            var request = new CalculateChangeCommandRequest(new[] { "Sterling", "33.88", "57.66" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsValid);

        }

    }
}



