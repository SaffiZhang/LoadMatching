using LoadLink.LoadMatching.Application.NetworkMembers.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.NetworkMember.Profiles
{
    public class NetworkMembersProfile : AutoMapper.Profile
    {
        public NetworkMembersProfile()
        {
            CreateMap<UspGetNetworkMemberResult, GetNetworkMembersQuery>();
        }
    }
}
