using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Application.RIRate.Models.Commands;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.RIRate.Repository
{
    public interface IRIRateRepository
    {
        Task<UspGetRIRateResult> GetAsync(GetRIRateCommand requestLane);
    }
}
