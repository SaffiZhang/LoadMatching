using FluentValidation;
using LoadLink.LoadMatching.Application.PDRatio.Models.Commands;

namespace LoadLink.LoadMatching.Api.Validations.PDRatio
{
    public class GetPDRatioValidator : AbstractValidator<GetPDRatioCommand>
    {
        public GetPDRatioValidator()
        {
            string vehicleTypes = "VRKFSDTCUHLONPIE";

            RuleFor(x => x.VehicleType).NotNull()
                .WithMessage("Vehicle type is Mandatory.");
            RuleFor(x => x.VehicleType).NotEmpty()
                .WithMessage("Vehicle type cannot be empty.");
            RuleFor(x => x.VehicleType)
                .Must(x => vehicleTypes.Contains(x.ToUpper()))
                .WithMessage("Invalid vehicle type.");

            RuleFor(x => x.SrceCity).NotNull()
                .WithMessage("SrceCity is Mandatory.");
            RuleFor(x => x.SrceCity).NotEmpty()
                .WithMessage("SrceCity cannot be empty.");

            RuleFor(x => x.SrceSt).NotNull()
                .WithMessage("SrceSt is Mandatory.");
            RuleFor(x => x.SrceSt).NotEmpty()
                .WithMessage("SrceSt cannot be empty.");

            RuleFor(x => x.DestCity).NotNull()
                .WithMessage("DestCity is Mandatory.");
            RuleFor(x => x.DestCity).NotEmpty()
                .WithMessage("DestCity cannot be empty.");

            RuleFor(x => x.DestSt).NotNull()
                .WithMessage("DestSt is Mandatory.");
            RuleFor(x => x.DestSt).NotEmpty()
                .WithMessage("DestSt cannot be empty.");
        }
    }
}
