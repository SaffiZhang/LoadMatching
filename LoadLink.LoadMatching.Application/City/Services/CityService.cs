using AutoMapper;
using LoadLink.LoadMatching.Application.City.Models.Commands;
using LoadLink.LoadMatching.Application.City.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.City.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCityCommand>> GetListAsync(string city, short sortType)
        {
            var result = await _cityRepository.GetListAsync(city, sortType);
            if (result == null)
                return null;

            return _mapper.Map<IEnumerable<GetCityCommand>>(result);
        }
    }
}
