using LoadLink.LoadMatching.Application.City.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.City.Profiles
{
    public class CityProfile : AutoMapper.Profile
    {
        public CityProfile()
        {
            CreateMap<UspGetCityListResult, GetCityCommand>();
        }
    }
}
