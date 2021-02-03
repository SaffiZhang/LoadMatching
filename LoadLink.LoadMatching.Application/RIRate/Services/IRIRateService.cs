using LoadLink.LoadMatching.Application.RIRate.Models.Commands;
using LoadLink.LoadMatching.Application.RIRate.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.RIRate.Services
{
    public interface IRIRateService
    {
        Task<GetRIRateQuery> GetAsync(GetRIRateCommand requestLane);
    }
}
