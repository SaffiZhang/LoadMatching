using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.VehicleSize.Profiles
{
    public class VehicleAttributeProfile : AutoMapper.Profile
    {
        public VehicleAttributeProfile()
        {
            CreateMap<UspGetVehicleSizeResult, GetVehicleAttributeQuery>();
        }
    }
}
