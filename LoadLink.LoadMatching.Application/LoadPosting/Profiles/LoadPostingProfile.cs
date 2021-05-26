using LoadLink.LoadMatching.Application.LoadPosting.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.LoadPosting.Profiles
{
    public class LoadPostingProfile : AutoMapper.Profile
    {
        public LoadPostingProfile()
        {
            CreateMap<UspGetLoadPostingResult, GetLoadPostingQuery>();
            CreateMap<UspGetLoadPostingLLResult, GetLoadPostingLLQuery>();
        }
    }
}
