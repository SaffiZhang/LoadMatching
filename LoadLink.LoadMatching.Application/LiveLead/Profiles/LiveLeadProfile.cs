using LoadLink.LoadMatching.Application.LiveLead.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.LiveLead.Profiles
{
    public class LiveLeadProfile : AutoMapper.Profile
    {
        public LiveLeadProfile()
        {
            CreateMap<UspGetLiveLeadsListResult, GetLiveLeadResult>();
        }
    }
}
