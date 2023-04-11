using ChangeCalculator.Service.Validators;
using ChangeCalculator.Domain.Commands;

namespace ChangeCalculator.UnitTests.Validators
{
    public class CurrencyValidatorTests
    {
        private CalculateChangeValidator _validators;

        public CurrencyValidatorTests()
        {
            _validators = new CalculateChangeValidator();
        }

        [Fact]
        public void Given_Invalid_Empty_Currency_Argument_Validation_Fail()
        {
            var request = new CalculateChangeCommandRequest(new []{ "", "33.88", "57.66" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Currency must be either Sterling or Dollar.", result.Errors[0].ErrorMessage);

        }

        [Fact]
        public void Given_Invalid_Currency_Argument_Validation_Fail()
        {
            var request = new CalculateChangeCommandRequest(new[] { "Invalid", "33.88", "57.66" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Currency must be either Sterling or Dollar.", result.Errors[0].ErrorMessage);

        }


        [Fact]
        public void Given_Valid_Sterling_Currency_Argument_Validation_Success()
        {
            var request = new CalculateChangeCommandRequest(new[] { "Sterling", "33.88", "57.66" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsValid);

        }

        [Fact]
        public void Given_Valid_Dollar_Currency_Argument_Validation_Success()
        {
            var request = new CalculateChangeCommandRequest(new[] { "Dollar", "33.88", "57.66" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsValid);

        }

    }
}



