using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using LoadLink.LoadMatching.Domain.Procedures;

namespace EquipmentLink.EquipmentMatching.Application.DATEquipmentLead.Profiles
{
    public class DatEquipmentLiveLeadProfile : AutoMapper.Profile
    {
        public DatEquipmentLiveLeadProfile()
        {
            CreateMap<UspGetDatEquipmentLiveLeadResult, GetDatEquipmentLiveLeadQuery>();
        }
    }
}
