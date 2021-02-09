using LoadLink.LoadMatching.Application.EquipmentLead.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.EquipmentLead.Profiles
{
    public class EquipmentLeadProfile : AutoMapper.Profile
    {
        public EquipmentLeadProfile()
        {
            CreateMap<UspGetEquipmentLeadResult, GetEquipmentLeadQuery>();
            CreateMap<UspGetEquipmentLeadCombinedResult, GetEquipmentLeadCombinedQuery>();
        }
    }
}
