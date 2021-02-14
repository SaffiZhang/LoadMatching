
using LoadLink.LoadMatching.Application.LoadPosition.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.LoadPosition.Profiles
{
    public class LoadPositionProfile : AutoMapper.Profile
    {
        public LoadPositionProfile()
        {
            CreateMap<UspGetLoadPositionResult, GetLoadPositionQuery>();
        }
    }
}
