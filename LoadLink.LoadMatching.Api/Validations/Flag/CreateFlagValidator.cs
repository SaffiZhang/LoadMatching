using FluentValidation;
using LoadLink.LoadMatching.Application.Flag.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.Flag
{
    public class CreateFlagValidator : AbstractValidator<CreateFlagCommand>
    {
        public CreateFlagValidator()
        {
            RuleFor(x => x.LToken).NotNull()
                .WithMessage("LToken is Mandatory.");
            RuleFor(x => x.LToken).GreaterThan((short)0)
                .WithMessage("Invalid LToken - must be greater than 0.");
            RuleFor(x => x.EToken).NotNull()
                .WithMessage("EToken is Mandatory.");
            RuleFor(x => x.EToken).GreaterThan((short)0)
                .WithMessage("Invalid EToken - must be greater than 0.");
            RuleFor(x => x.ContactCustCD).NotNull()
                .WithMessage("ContactCustCD is Mandatory.");
            RuleFor(x => x.ContactCustCD).NotEmpty()
                .WithMessage("ContactCustCD is Mandatory.");
        }
    }
}
