using FluentValidation;
using LoadLink.LoadMatching.Application.Member.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.Member
{
    public class UpdateMemberValidator : AbstractValidator<UpdateMemberCommand>
    {
        public UpdateMemberValidator()
        {
            RuleFor(x => x.MemberId).NotNull()
                .WithMessage("MemberId is Mandatory.");
            RuleFor(x => x.MemberId).GreaterThan((short)0)
                .WithMessage("Invalid MemberId - must be greater than 0.");
        }
    }
}
