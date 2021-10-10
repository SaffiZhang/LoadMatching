using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public interface ILeadCaching
    {
      
        Task BulkInsertLeads(LeadPostingType leadType, int token, IEnumerable<LeadBase> leads);
        Task InsertSingleLead(LeadPostingType leadType, int token, LeadBase lead);
        Task<IEnumerable<LeadBase>> GetLeadsByToken(LeadPostingType leadType, string custCD, int token);
        Task<IEnumerable<PostingLeadCount>> GetPostingLeadCountByCustCD(PostingLeadCount custCD);
        Task DeleteLead(LeadPostingType leadType, int token,int leadId);
        
        Task<IEnumerable<int>> GetDeleteLeadsByToken(LeadPostingType leadType,  int token, int maxLeadId);
        Task CleanLeadsCaching(LeadPostingType leadType, int token, bool isDeleted);
    
       


    }
}
