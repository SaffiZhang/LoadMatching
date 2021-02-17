using LoadLink.LoadMatching.Application.Member.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.Member.Profiles
{
    public class MemberProfile : AutoMapper.Profile
    {
        public MemberProfile()
        {
            CreateMap<UspGetMemberResult, GetMemberQuery>();
        }
    }
}
