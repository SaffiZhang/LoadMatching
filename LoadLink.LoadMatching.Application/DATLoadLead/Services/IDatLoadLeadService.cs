using LoadLink.LoadMatching.Application.DATLoadLead.Models.Commands;
using LoadLink.LoadMatching.Application.DATLoadLead.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATLoadLead.Services
{
    public interface IDatLoadLeadService
    {
        Task<IEnumerable<GetDatLoadLeadQuery>> GetListAsync(string custCD, string mileageProvider,
                                                            DatLoadLeadSubscriptionsStatus subscriptions);
        Task<IEnumerable<GetDatLoadLeadQuery>> GetByPostingAsync(string custCD, int postingId, string mileageProvider,
                                                                    DatLoadLeadSubscriptionsStatus subscriptions);
    }
}
