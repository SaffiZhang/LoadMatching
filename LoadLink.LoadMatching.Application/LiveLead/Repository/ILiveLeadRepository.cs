using LoadLink.LoadMatching.Application.LiveLead.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LiveLead.Repository
{
    public interface ILiveLeadRepository
    { 
        Task<IEnumerable<UspGetLiveLeadsListResult>> GetLiveLeads(GetLiveLeadQuery LLRequest);
        Task<DateTime> GetServerTime();
    }
}
