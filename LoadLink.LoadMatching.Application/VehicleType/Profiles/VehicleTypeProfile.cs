using LoadLink.LoadMatching.Application.VehicleType.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.VehicleType.Profiles
{
    public class VehicleTypeProfile : AutoMapper.Profile
    {
        public VehicleTypeProfile()
        {
            CreateMap<UspGetVehicleTypeResult, GetVehicleTypesQuery>();
        }
    }
}
