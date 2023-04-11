using MediatR;
using ChangeCalculator.Domain.Entities;
using ChangeCalculator.Domain.Commands;
using ChangeCalculator.Domain.Data;

namespace ChangeCalculator.Domain.Handlers
{
    public class ChangeCalculatorHandler : IRequestHandler<CalculateChangeCommandRequest, CalculateChangeCommandResponse>
    {
        private readonly IServiceProvider _provider;

        public ChangeCalculatorHandler(IServiceProvider provider) => (_provider) = (provider);

        public Task<CalculateChangeCommandResponse> Handle(CalculateChangeCommandRequest request, CancellationToken cancellationToken)
        {
            var dataType = Type.GetType("ChangeCalculator.Domain.Data." + request.Currency + "ReferenceData");
            var currencyData = _provider.GetService(dataType) as ICurrencyReferenceData;

            var units = currencyData?.GetUnits();

            var result = new CalculateChangeCommandResponse
            {
                ChangeAmount = request.ChangeAmount,
                Change = Calculate(units, request.ChangeAmount)
            };

            return Task.FromResult(result);

        }

        private List<Tuple<CurrencyUnit, int>> Calculate(List<CurrencyUnit> units, decimal changeAmount) {
            var permutations = new List<Tuple<CurrencyUnit, int>>();

            foreach (var unit in units) {
                var unitQuantity = (int)(changeAmount / unit.UnitAmount);

                if (changeAmount == 0)
                {
                    break;
                }

                if (unitQuantity > 0)
                {
                    permutations.Add(new Tuple<CurrencyUnit, int>(unit, unitQuantity));
                    changeAmount %= unit.UnitAmount;
                }
                
            }

            return permutations;
        }
    }
}
