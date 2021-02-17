using LoadLink.LoadMatching.Application.EquipmentPosition.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.EquipmentPosition.Profiles
{
    public class EquipmentPositionProfile : AutoMapper.Profile
    {
        public EquipmentPositionProfile()
        {
            CreateMap<UspGetEquipmentPositionResult, GetEquipmentPositionQuery>();
        }
    }
}
