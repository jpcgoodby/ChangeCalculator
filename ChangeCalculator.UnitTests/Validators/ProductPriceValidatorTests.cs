using ChangeCalculator.Service.Validators;
using ChangeCalculator.Domain.Commands;

namespace ChangeCalculator.UnitTests.Validators
{
    public class ProductPriceValidatorTests
    {
        private CalculateChangeValidator _validators;

        public ProductPriceValidatorTests()
        {
            _validators = new CalculateChangeValidator();
        }

        [Fact]
        public void Given_Invalid_Empty_ProductPrice_Argument_Validation_Fail()
        {
            var request = new CalculateChangeCommandRequest(new []{ "Sterling", "", "7.66" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Product price must be greater than zero.", result.Errors[0].ErrorMessage);

        }

        [Fact]
        public void Given_Invalid_ProductPrice_Argument_Validation_Fail()
        {
            var request = new CalculateChangeCommandRequest(new[] { "Sterling", "0", "7.66" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("Product price must be greater than zero.", result.Errors[0].ErrorMessage);

        }


        [Fact]
        public void Given_Valid_ProductPrice_Argument_Validation_Success()
        {
            var request = new CalculateChangeCommandRequest(new[] { "Sterling", "33.88", "57.66" });

            var result = _validators.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsValid);

        }

    }
}



