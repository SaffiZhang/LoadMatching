using LoadLink.LoadMatching.Application.PDRatio.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.PDRatio.Profiles
{
    public class PDRatioProfile : AutoMapper.Profile
    {
        public PDRatioProfile()
        {
            CreateMap<UspGetPDRatioResult, GetPDRatioQuery>();
        }
    }
}
