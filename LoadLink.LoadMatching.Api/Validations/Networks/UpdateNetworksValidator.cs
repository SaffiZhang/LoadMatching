using FluentValidation;
using LoadLink.LoadMatching.Application.Networks.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.AssignedEquipment
{
    public class UpdateNetworksValidator : AbstractValidator<NetworksCommand>
    {
        public UpdateNetworksValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithMessage("Name is Mandatory.");
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Invalid Name - Cannot be empty.");
        }
    }
}
