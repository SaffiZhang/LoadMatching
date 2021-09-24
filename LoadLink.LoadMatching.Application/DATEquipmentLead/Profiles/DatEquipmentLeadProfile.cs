using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using LoadLink.LoadMatching.Domain.Procedures;

namespace EquipmentLink.EquipmentMatchings.Application.DATEquipmentLead.Profiles
{
    public class DatEquipmentLeadProfile : AutoMapper.Profile
    {
        public DatEquipmentLeadProfile()
        {
            CreateMap<UspGetDatEquipmentLeadResult, GetDatEquipmentLeadQuery>();
        }
    }
}
