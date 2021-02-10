using FluentValidation;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.AssignedLoad
{
    public class UpdateAssignedLoadCustomerTrackingValidator : AbstractValidator<UpdateCustomerTrackingCommand>
    {
        public UpdateAssignedLoadCustomerTrackingValidator()
        {
            RuleFor(x => x.ID).NotNull()
                .WithMessage("ID is Mandatory.");
            RuleFor(x => x.ID).GreaterThan((short)0)
                .WithMessage("Invalid ID - must be greater than 0.");
            RuleFor(x => x.CustTracking).NotNull()
                .WithMessage("CustTracking is Mandatory.");
            RuleFor(x => x.CustTracking).NotEmpty()
                .WithMessage("CustTracking is Mandatory.");
        }

    }
}
