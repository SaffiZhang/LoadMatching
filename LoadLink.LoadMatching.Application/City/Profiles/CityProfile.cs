using LoadLink.LoadMatching.Application.City.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.City.Profiles
{
    public class CityProfile : AutoMapper.Profile
    {
        public CityProfile()
        {
            CreateMap<UspGetCityListResult, GetCityQuery>();
        }
    }
}
