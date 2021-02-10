using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Profiles
{
    public class EquipmentPostingProfile : AutoMapper.Profile
    {
        public EquipmentPostingProfile()
        {
            CreateMap<UspGetDatEquipmentPostingResult, GetEquipmentPostingQuery>();
        }
    }
}
