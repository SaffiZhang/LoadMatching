using LoadLink.LoadMatching.Application.City.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.City.Services
{
    public interface ICityService
    {
        Task<IEnumerable<GetCityQuery>> GetListAsync(string city, short sortType);
    }
}
