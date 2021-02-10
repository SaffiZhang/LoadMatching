using LoadLink.LoadMatching.Application.DATLoadLead.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.DATLoadLead.Profiles
{
    public class DatLoadLeadProfile : AutoMapper.Profile
    {
        public DatLoadLeadProfile()
        {
            CreateMap<UspGetDatLoadLeadResult, GetDatLoadLeadQuery>();
        }
    }
}
