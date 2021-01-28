using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.VehicleAttribute.Profiles
{
    public class VehicleAttributeProfile : AutoMapper.Profile
    {
        public VehicleAttributeProfile()
        {
            CreateMap<UspGetVehicleAttributeResult, GetVehicleAttributeQuery>();
        }
    }
}
