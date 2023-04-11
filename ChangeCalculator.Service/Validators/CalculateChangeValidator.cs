using ChangeCalculator.Domain.Commands;
using FluentValidation;

namespace ChangeCalculator.Service.Validators
{
    public class CalculateChangeValidator : AbstractValidator<CalculateChangeCommandRequest>
    {
        public CalculateChangeValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(change => change.Currency).Must(x => x == "Sterling" || x == "Dollar").WithMessage("Currency must be either Sterling or Dollar.");
            RuleFor(change => change.ProductPrice).GreaterThan(0).WithMessage("Product price must be greater than zero.");
            RuleFor(change => change.PaymentAmount).GreaterThan(0).WithMessage("Payment amount must be greater than zero.");
            RuleFor(change => change.ChangeAmount).GreaterThan(0).WithMessage("Change amount must be greater than zero.");

        }

    }
}
