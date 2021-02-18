using LoadLink.LoadMatching.Application.DATLoadLiveLead.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATLoadLiveLead.Services
{
    public interface IDatLoadLiveLeadService
    {
        bool HasQPSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }


        Task<IEnumerable<GetDatLoadLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);

    }
}
