using LoadLink.LoadMatching.Application.DATAccount.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.DATAccount.Profiles
{
    public class DatAccountProfile : AutoMapper.Profile
    {
        public DatAccountProfile()
        {
            CreateMap<UspGetDatAccountResult, GetDatAccountQuery>();
        }
    }
}
