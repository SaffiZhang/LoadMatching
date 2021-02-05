using LoadLink.LoadMatching.Application.City.Models.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.City.Services
{
    public interface ICityService
    {
        Task<IEnumerable<GetCityCommand>> GetListAsync(string city, short sortType);
    }
}
