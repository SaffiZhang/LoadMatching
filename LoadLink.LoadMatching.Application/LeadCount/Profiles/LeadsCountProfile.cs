using LoadLink.LoadMatching.Application.LeadCount.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.LeadCount.Profiles
{
    public class LeadsCountProfile : AutoMapper.Profile
    {
        public LeadsCountProfile()
        {
            CreateMap<UspGetLoadLeadsCountResult, GetLeadsCountQuery>();
            CreateMap<UspGetEquipmentLeadsCountResult, GetLeadsCountQuery>();
        }
    }
}
