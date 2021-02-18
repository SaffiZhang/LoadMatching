using LoadLink.LoadMatching.Application.Flag.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.Flag.Profiles
{
    public class FlagProfile : AutoMapper.Profile
    {
        public FlagProfile()
        {
            CreateMap<UspGetFlagResult, GetFlagQuery>();
        }
    }
}
