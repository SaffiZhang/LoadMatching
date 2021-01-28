using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.VehicleSize.Profiles
{
    public class VehicleSizeProfile : AutoMapper.Profile
    {
        public VehicleSizeProfile()
        {
            CreateMap<UspGetVehicleSizeResult, GetVehicleSizeQuery>();
        }
    }
}
