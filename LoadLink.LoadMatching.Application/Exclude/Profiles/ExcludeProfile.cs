
using LoadLink.LoadMatching.Application.Exclude.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.Exclude.Profiles
{
    public class ExcludeProfile : AutoMapper.Profile
    {
        public ExcludeProfile()
        {
            CreateMap<UspGetExcludeResult, GetExcludeQuery>();
        }
    }
}
