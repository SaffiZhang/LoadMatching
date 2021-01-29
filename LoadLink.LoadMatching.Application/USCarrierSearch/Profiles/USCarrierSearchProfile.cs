using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.USCarrierSearch.Profiles
{
    public class USCarrierSearchProfile : AutoMapper.Profile
    {
        public USCarrierSearchProfile()
        {
            CreateMap<UspGetUSCarrierResult, GetUSCarrierSearchQuery>();
        }
    }
}
