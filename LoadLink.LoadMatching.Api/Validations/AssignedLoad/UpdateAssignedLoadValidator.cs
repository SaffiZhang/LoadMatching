
using FluentValidation;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.AssignedLoad
{
    public class UpdateAssignedLoadValidator : AbstractValidator<UpdateAssignedLoadCommand>
    {
        public UpdateAssignedLoadValidator()
        {
            RuleFor(x => x.PIN).NotNull()
                .WithMessage("PIN is Mandatory.");
            RuleFor(x => x.PIN).NotEmpty()
                .WithMessage("Invalid PIN - Cannot be empty.");
            RuleFor(x => x.UserId).NotNull()
                .WithMessage("UserId is Mandatory.");
            RuleFor(x => x.UserId).GreaterThan((short)0)
                .WithMessage("Invalid UserId - must be greater than 0.");
        }

    }
}
