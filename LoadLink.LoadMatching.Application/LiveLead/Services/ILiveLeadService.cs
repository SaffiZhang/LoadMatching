using LoadLink.LoadMatching.Application.LiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.LiveLead.Models.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LiveLead.Services
{
    public interface ILiveLeadService
    {
        Task<IEnumerable<GetLiveLeadResult>> GetLiveLeads(GetLiveLeadRequest LLRequest, string mileageProvider, string custCd,
                                                            LiveLeadSubscriptionsStatus subscriptions);
        Task<DateTime> GetServerTime();
    }
}
