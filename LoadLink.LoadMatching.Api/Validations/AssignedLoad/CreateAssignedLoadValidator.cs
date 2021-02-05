
using FluentValidation;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.AssignedLoad
{
    public class CreateAssignedLoadValidator : AbstractValidator<CreateAssignedLoadCommand>
    {
        public CreateAssignedLoadValidator()
        {
            RuleFor(x => x.Token).NotNull()
                .WithMessage("Token is Mandatory.");
            RuleFor(x => x.Token).GreaterThan((short)0)
                .WithMessage("Invalid Token - must be greater than 0.");
        }

    }
}
