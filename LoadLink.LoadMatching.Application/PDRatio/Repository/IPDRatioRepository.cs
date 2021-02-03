using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Application.PDRatio.Models.Commands;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.PDRatio.Repository
{
    public interface IPDRatioRepository
    {
        Task<UspGetPDRatioResult> GetAsync(GetPDRatioCommand requestLane);
    }
}
