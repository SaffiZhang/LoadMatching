using LoadLink.LoadMatching.Application.LegacyDeleted.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.LegacyDeleted.Profiles
{
    public class LegacyDeletedProfile : AutoMapper.Profile
    {
        public LegacyDeletedProfile()
        {
            CreateMap<UspGetUserLegacyDeletedResult, GetUserLegacyDeletedQuery>();
        }
    }
}
