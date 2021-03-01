using LoadLink.LoadMatching.Application.LoadLiveLead.Models;
using LoadLink.LoadMatching.Application.LoadLiveLead.Models.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadLiveLead.Services
{
    public interface ILoadLiveLeadService
    {
        Task<IEnumerable<GetLoadLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, 
                                                                DateTime? leadfrom, int? postingId,
                                                                LoadLiveLeadSubscriptionsStatus subscriptions);
    }
}
