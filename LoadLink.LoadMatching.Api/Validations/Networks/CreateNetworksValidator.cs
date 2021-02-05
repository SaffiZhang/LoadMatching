using FluentValidation;
using LoadLink.LoadMatching.Application.Networks.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.AssignedEquipment
{
    public class CreateNetworksValidator : AbstractValidator<NetworksCommand>
    {
        public CreateNetworksValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithMessage("Name is Mandatory.");
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Invalid Name - Cannot be empty.");
            RuleFor(x => x.Type).NotNull()
                .WithMessage("Type is Mandatory.");
            RuleFor(x => x.Type).NotEmpty()
                .WithMessage("Invalid Type - Cannot be empty.");
        }

    }
}
