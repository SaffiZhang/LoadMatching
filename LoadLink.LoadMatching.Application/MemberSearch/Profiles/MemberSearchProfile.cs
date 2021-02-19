using LoadLink.LoadMatching.Application.MemberSearch.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.MemberSearch.Profiles
{
    public class MemberSearchProfile : AutoMapper.Profile
    {
        public MemberSearchProfile()
        {
            CreateMap<UspGetMembersResult, GetMemberSearchResult>();
        }
    }
}
