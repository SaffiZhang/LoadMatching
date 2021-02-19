using LoadLink.LoadMatching.Application.LiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.LiveLead.Models.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LiveLead.Services
{
    public interface ILiveLeadService
    {
        bool B_QPAPIKey_Status { get; set; }
        bool B_EQFAPIKey_Status { get; set; }
        bool B_TCCAPIKey_Status { get; set; }
        bool B_TCUSAPIKey_Status { get; set; }
        bool B_DATAPIKey_Status { get; set; }
        bool C_QPAPIKey_Status { get; set; }
        bool C_EQFAPIKey_Status { get; set; }
        bool C_TCCAPIKey_Status { get; set; }
        bool C_TCUSAPIKey_Status { get; set; }
        bool C_DATAPIKey_Status { get; set; }

        Task<IEnumerable<GetLiveLeadResult>> GetLiveLeads(GetLiveLeadRequest LLRequest, string mileageProvider, string custCd);
        Task<DateTime> GetServerTime();

    }
}
