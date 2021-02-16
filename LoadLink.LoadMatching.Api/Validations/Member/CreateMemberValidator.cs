using FluentValidation;
using LoadLink.LoadMatching.Application.Member.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.Member
{
    public class CreateMemberValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberValidator()
        {
            RuleFor(x => x.MemberCustCd).NotNull()
                .WithMessage("MemberCustCd is Mandatory.");
            RuleFor(x => x.MemberCustCd).NotEmpty()
                .WithMessage("Invalid MemberCustCd - can not be empty.");
        }
    }
}
