using ChangeCalculator.Domain.Commands;
using ChangeCalculator.Domain.Data;
using ChangeCalculator.Domain.Handlers;
using Moq;

namespace ChangeCalculator.UnitTests.Handlers
{
    public class ChangeSterlingHandlerTests
    {
        private readonly ChangeCalculatorHandler _handler;
        private readonly Mock<IServiceProvider> _provider;

        public ChangeSterlingHandlerTests()
        {
            _provider = new Mock<IServiceProvider>();
            _handler = new ChangeCalculatorHandler(_provider.Object);
        }

        [Fact]
        public void Given_Valid_Sterling_Currency_Change_Returned_Successfully()
        {
            _provider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(new SterlingReferenceData());

            var response = _handler.Handle(new CalculateChangeCommandRequest(new[] { "Sterling", "338.88", "766.00" }), new CancellationToken()).Result;

            Assert.NotNull(response);
            Assert.Null(response.Error);
        }

        [Fact]
        public void Given_Valid_Sterling_Currency_Change_Returned_Fifties_Successfully()
        {
            _provider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(new SterlingReferenceData());

            var response = _handler.Handle(new CalculateChangeCommandRequest(new[] { "Sterling", "400.00", "800.00" }), new CancellationToken()).Result;

            Assert.NotNull(response);
            Assert.Null(response.Error);
            Assert.Single(response.Change);
            Assert.Equal("£50", response.Change.Single().Item1.UnitName);
            Assert.Equal(8, response.Change.Single().Item2);
        }

        [Fact]
        public void Given_Valid_Sterling_Currency_Change_Returned_FiftiesTwenties_Successfully()
        {
            _provider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(new SterlingReferenceData());

            var response = _handler.Handle(new CalculateChangeCommandRequest(new[] { "Sterling", "400.00", "490.00" }), new CancellationToken()).Result;

            Assert.NotNull(response);
            Assert.Null(response.Error);
            Assert.Equal(2, response.Change.Count);
            Assert.Equal("£50", response.Change[0].Item1.UnitName);
            Assert.Equal(1, response.Change[0].Item2);
            Assert.Equal("£20", response.Change[1].Item1.UnitName);
            Assert.Equal(2, response.Change[1].Item2);
        }

        [Fact]
        public void Given_Valid_Sterling_Currency_Change_Returned_All_Successfully()
        {
            _provider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(new SterlingReferenceData());

            var response = _handler.Handle(new CalculateChangeCommandRequest(new[] { "Sterling", "400.00", "596.96" }), new CancellationToken()).Result;

            Assert.NotNull(response);
            Assert.Null(response.Error);
            Assert.Equal(8, response.Change.Count);
            Assert.Equal("£50", response.Change[0].Item1.UnitName);
            Assert.Equal(3, response.Change[0].Item2);
            Assert.Equal("£20", response.Change[1].Item1.UnitName);
            Assert.Equal(2, response.Change[1].Item2);
            Assert.Equal("£5", response.Change[2].Item1.UnitName);
            Assert.Equal(1, response.Change[2].Item2);
            Assert.Equal("£1", response.Change[3].Item1.UnitName);
            Assert.Equal(1, response.Change[3].Item2);
            Assert.Equal("50p", response.Change[4].Item1.UnitName);
            Assert.Equal(1, response.Change[4].Item2);
            Assert.Equal("20p", response.Change[5].Item1.UnitName);
            Assert.Equal(2, response.Change[5].Item2);
            Assert.Equal("5p", response.Change[6].Item1.UnitName);
            Assert.Equal(1, response.Change[6].Item2);
            Assert.Equal("1p", response.Change[7].Item1.UnitName);
            Assert.Equal(1, response.Change[7].Item2);
        }
    }
}



