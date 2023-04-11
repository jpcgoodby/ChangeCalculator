using FluentValidation;
using MediatR;
using ChangeCalculator.Domain.Commands;

namespace ChangeCalculator.Service.Behaviours
{
    public class RulesBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _rules;

        public RulesBehaviour(IEnumerable<IValidator<TRequest>> rules) => _rules = rules;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_rules.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                string errorResponse = null;

                foreach (var rule in _rules)
                {
                    var validationResults = await rule.ValidateAsync(context, cancellationToken);
                    if (!validationResults.IsValid)
                    {
                        if (validationResults.Errors.Count > 0)
                        {
                            foreach (var validationError in validationResults.Errors)
                            {
                                errorResponse += validationError.ErrorMessage + ", ";
                            }

                            errorResponse = errorResponse.Substring(0, errorResponse.Length - 2);

                        }

                        throw new ValidationException(errorResponse);
                    }
                }
            }

            return await next();
        }
    }
}
