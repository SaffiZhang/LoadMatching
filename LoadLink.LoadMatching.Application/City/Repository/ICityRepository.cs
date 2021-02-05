using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.City.Repository
{
    public interface ICityRepository
    {
        Task <IEnumerable<UspGetCityListResult>> GetListAsync(string city, short sortType);
    }
}
