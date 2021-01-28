using LoadLink.LoadMatching.Application.USMemberSearch.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.USMemberSearch.Profiles
{
    public class USMemberSearchProfile : AutoMapper.Profile
    {
        public USMemberSearchProfile()
        {
            CreateMap<UspGetUSMembersResult, GetUSMemberSearchQuery>();
        }
    }
}
