using FluentValidation;
using LoadLink.LoadMatching.Application.Exclude.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.Exclude
{
    public class CreateExcludeValidator : AbstractValidator<CreateExcludeCommand>
    {
        public CreateExcludeValidator()
        {
            RuleFor(x => x.ExcludeCustCD).NotNull()
                .WithMessage("ExcludeCustCD is Mandatory.");
            RuleFor(x => x.ExcludeCustCD).NotEmpty()
                .WithMessage("Invalid ExcludeCustCD - can not be empty.");
        }
    }
}
