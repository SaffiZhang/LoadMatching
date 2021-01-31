using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.VehicleAttribute.Profiles
{
    public class USMemberSearchProfile : AutoMapper.Profile
    {
        public USMemberSearchProfile()
        {
            CreateMap<UspGetVehicleAttributeResult, GetVehicleAttributeQuery>();
        }
    }
}
