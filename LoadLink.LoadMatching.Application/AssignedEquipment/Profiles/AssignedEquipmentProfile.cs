using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.AssignedEquipment.Profiles
{
    public class AssignedEquipmentProfile : AutoMapper.Profile
    {
        public AssignedEquipmentProfile()
        {
            CreateMap<UspGetAssignedLoadResult, GetAssignedLoadQuery>();
        }
    }
}
