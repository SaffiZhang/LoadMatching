using LoadLink.LoadMatching.Application.DATLoadLiveLead.Models;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Models.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATLoadLiveLead.Services
{
    public interface IDatLoadLiveLeadService
    {
        Task<IEnumerable<GetDatLoadLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, DateTime? leadFrom,
                                                                    int? postingId, DatLoadLiveLeadSubscriptionsStatus subscriptions);
    }
}
