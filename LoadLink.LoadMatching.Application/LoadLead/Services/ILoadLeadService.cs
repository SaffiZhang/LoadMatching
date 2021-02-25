using LoadLink.LoadMatching.Application.LoadLead.Models.Commands;
using LoadLink.LoadMatching.Application.LoadLead.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.LoadLead.Services
{
    public interface ILoadLeadService
    {
        Task<IEnumerable<GetLoadLeadQuery>> GetByPostingAsync(string custCd, int postingID, string mileageProvider, 
                                                                LoadLeadSubscriptionsStatus subscriptions);
        Task<IEnumerable<GetLoadLeadQuery>> GetListAsync(string custCd, string mileageProvider,
                                                            LoadLeadSubscriptionsStatus subscriptions);
        Task<IEnumerable<GetLoadLeadQuery>> GetByPosting_CombinedAsync(string custCd, int postingID, 
                                                                        string mileageProvider, int leadsCap,
                                                                        LoadLeadSubscriptionsStatus subscriptions);
    }
}
