using FluentValidation;
using LoadLink.LoadMatching.Application.Contacted.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.Contacted
{
    public class UpdateContactedValidator : AbstractValidator<UpdateContactedCommand>
    {
        public UpdateContactedValidator()
        {
            RuleFor(x => x.CnCustCd).NotNull()
                .WithMessage("CnCustCd is Mandatory.");
            RuleFor(x => x.CnCustCd).NotEmpty()
                .WithMessage("Invalid CnCustCd - Cannot be empty.");
            RuleFor(x => x.EToken).NotNull()
                .WithMessage("EToken is Mandatory.");
            RuleFor(x => x.LToken).NotNull()
                .WithMessage("LToken is Mandatory.");
        }

    }
}
