using ChangeCalculator.Domain.Entities;
using MediatR;

namespace ChangeCalculator.Domain.Commands
{
    public class CalculateChangeCommandRequest : IRequest<CalculateChangeCommandResponse>
    {
        public CalculateChangeCommandRequest(string[] args)
        {
            Currency = (args[0].Length > 0) ? args[0].Substring(0, 1).ToUpper() + args[0].Substring(1, args[0].Length -1).ToLower() : null;
            ProductPrice = (args[1].Length > 0) ? decimal.Parse(args[1]) : 0;
            PaymentAmount = (args[2].Length > 0) ? decimal.Parse(args[2]) : 0;
            ChangeAmount = PaymentAmount - ProductPrice;
        }

        public string Currency { get; }

        public decimal ProductPrice { get; }

        public decimal PaymentAmount { get; }

        public decimal ChangeAmount { get; }
    }

    public class CalculateChangeCommandResponse
    {
        public decimal ChangeAmount { get; set; }

        public List<Tuple<CurrencyUnit, int>> Change { get; set; }

        public string Error { get; set; }
    }
}
