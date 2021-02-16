using LoadLink.LoadMatching.Application.LoadLiveLead.Models;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.LoadLiveLead.Profiles
{
    public class LoadLiveLeadProfile : AutoMapper.Profile
    {
        public LoadLiveLeadProfile()
        {
            CreateMap<UspGetLoadLeadResult, GetLoadLiveLeadQuery>();
        }
    }
}
