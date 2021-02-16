using LoadLink.LoadMatching.Application.DATLoadLiveLead.Models;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.DATLoadLiveLead.Profiles
{
    public class DatLoadLiveLeadProfile : AutoMapper.Profile
    {
        public DatLoadLiveLeadProfile()
        {
            CreateMap<UspGetDatLoadLeadResult, GetDatLoadLiveLeadQuery>();
        }
    }
}
