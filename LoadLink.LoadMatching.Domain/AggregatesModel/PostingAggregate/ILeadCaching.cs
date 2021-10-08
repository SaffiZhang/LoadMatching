using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public interface ILeadCaching
    {
      
        Task BulkInsertLeads(LeadType leadType, int token, IEnumerable<LeadBase> leads);
        Task DeleteLead(LeadType leadType, int token,int leadId);
        Task<IEnumerable<LeadBase>> GetLeadsByToken(LeadType leadType,  int token);
        Task<IEnumerable<int>> GetDeleteLeadsByToken(LeadType leadType,  int token, int maxLeadId);
        Task CleanLeadsCaching(LeadType leadType, int token, bool isDeleted);


    }
}
