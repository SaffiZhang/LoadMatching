using LoadLink.LoadMatching.Application.RIRate.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.RIRate.Profiles
{
    public class RIRateProfile : AutoMapper.Profile
    {
        public RIRateProfile()
        {
            CreateMap<UspGetRIRateResult, GetRIRateQuery>();
        }
    }
}
