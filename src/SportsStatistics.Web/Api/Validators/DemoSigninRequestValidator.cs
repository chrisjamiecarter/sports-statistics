using FluentValidation;
using SportsStatistics.Web.Contracts.Requests;

namespace SportsStatistics.Web.Api.Validators;

internal sealed class DemoSigninRequestValidator : AbstractValidator<DemoSigninRequest>
{
    public DemoSigninRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");
    }
}
