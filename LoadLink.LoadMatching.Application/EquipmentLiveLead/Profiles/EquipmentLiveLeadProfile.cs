using LoadLink.LoadMatching.Application.EquipmentLiveLead.Models;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.EquipmentLiveLead.Profiles
{
    public class EquipmentLiveLeadProfile : AutoMapper.Profile
    {
        public EquipmentLiveLeadProfile()
        {
            CreateMap<UspGetEquipmentLeadResult, GetEquipmentLiveLeadQuery>();
        }
    }
}
