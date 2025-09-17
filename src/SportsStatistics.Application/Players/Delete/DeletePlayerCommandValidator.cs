using FluentValidation;

namespace SportsStatistics.Application.Players.Delete;

internal sealed class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
{
    public DeletePlayerCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
    }
}
