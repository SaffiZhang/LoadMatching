using FluentValidation;
using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.AssignedEquipment
{
    public class UpdateAssignedEquipmentValidator : AbstractValidator<UpdateAssignedEquipmentCommand>
    {
        public UpdateAssignedEquipmentValidator()
        {
            RuleFor(x => x.PIN).NotNull()
                .WithMessage("PIN is Mandatory.");
            RuleFor(x => x.PIN).NotEmpty()
                .WithMessage("Invalid PIN - Cannot be empty.");
            RuleFor(x => x.EToken).NotNull()
                .WithMessage("EToken is Mandatory.");
            RuleFor(x => x.EToken).GreaterThan((short)0)
                .WithMessage("Invalid EToken - must be greater than 0.");
        }

    }
}
