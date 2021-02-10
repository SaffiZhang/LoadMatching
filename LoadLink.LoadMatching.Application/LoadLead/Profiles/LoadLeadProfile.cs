using LoadLink.LoadMatching.Application.LoadLead.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.LoadLead.Profiles
{
    public class LoadLeadProfile : AutoMapper.Profile
    {
        public LoadLeadProfile()
        {
            CreateMap<UspGetLoadLeadResult, GetLoadLeadQuery>();
        }
    }
}
