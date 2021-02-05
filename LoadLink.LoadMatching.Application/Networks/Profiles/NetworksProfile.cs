using LoadLink.LoadMatching.Application.Networks.Models.Queries;
using LoadLink.LoadMatching.Application.Networks.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.Networks.Profiles
{
    public class NetworksProfile : AutoMapper.Profile
    {
        public NetworksProfile()
        {
            CreateMap<UspGetNetworkResult, GetNetworksQuery>();
            CreateMap<GetNetworksQuery, PatchNetworksCommand>();
        }
    }
}
