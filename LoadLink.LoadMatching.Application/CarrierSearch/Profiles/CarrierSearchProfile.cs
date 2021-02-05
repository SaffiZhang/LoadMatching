using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.CarrierSearch.Profiles
{
    public class CarrierSearchProfile : AutoMapper.Profile
    {
        public CarrierSearchProfile()
        {
            CreateMap<UspGetCarrierResult, GetCarrierSearchResult>();
        }
    }
}
