using FluentValidation;
using LoadLink.LoadMatching.Application.NetworkMembers.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.NetworkMember
{
    public class CreateNetworkMemberValidator : AbstractValidator<CreateNetworkMembersCommand>
    {
        public CreateNetworkMemberValidator()
        {
            RuleFor(x => x.NetworkId).NotNull()
                .WithMessage("NetworkId is Mandatory.");
            RuleFor(x => x.NetworkId).GreaterThan((short)0)
                .WithMessage("Invalid NetworkId - must be greater than 0.");
            RuleFor(x => x.MemberCustCD).NotNull()
                .WithMessage("MemberCustCD is Mandatory.");
            RuleFor(x => x.MemberCustCD).NotEmpty()
                .WithMessage("Invalid MemberCustCD - Cannot be empty.");
        }
    }
}

