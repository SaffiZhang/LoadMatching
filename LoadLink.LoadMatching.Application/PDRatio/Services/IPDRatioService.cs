using LoadLink.LoadMatching.Application.PDRatio.Models.Commands;
using LoadLink.LoadMatching.Application.PDRatio.Models.Queries;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.PDRatio.Services
{
    public interface IPDRatioService
    {
        Task<GetPDRatioQuery> GetAsync(GetPDRatioCommand requestLane);
    }
}
