using LoadLink.LoadMatching.Application.LoadLiveLead.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadLiveLead.Services
{
    public interface ILoadLiveLeadService
    {
        bool HasQPSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }


        Task<IEnumerable<GetLoadLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);

    }
}
