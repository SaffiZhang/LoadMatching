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
        }
    }
}
