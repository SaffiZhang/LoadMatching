using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public interface ILeadCaching
    {
        // notification service-- display all the leads of a posting,
        // could they be filtered by max LeadId? or the the notificaiton service has to filter it in .net code 
        Task<IEnumerable<LeadBase>> GetLeadsByToken(LeadPostingType leadType, string custCD, int token);
        //1. when a new posting is created, the related leads are created, then save to db and redis
        //2. in data loader layer, if it can't find the leads of a posting, query them from db and save to redis 
        Task BulkInsertLeads(LeadPostingType leadType, int token, IEnumerable<LeadBase> leads);
        //insert a single 2nd lead from matching service, because they have different posting token
        Task InsertSingleLead(LeadPostingType leadType, int token, LeadBase lead);
        // notification service-- get the postings' lead count and keep UI update frequently
        Task<IEnumerable<PostingLeadCount>> GetPostingLeadCountByCustCD(LeadPostingType leadType, PostingLeadCount custCD);
        // when user delete a posting , the related lead should be deleted
        Task DeleteLead(LeadPostingType leadType,  int token);
        // Get the lead id which are deleted of a posting which having token as id
        Task<IEnumerable<int>> GetDeleteLeadsByToken(LeadPostingType leadType,  int token, int maxLeadId);
        Task CleanLeadsCaching(LeadPostingType leadType, int token, bool isDeleted);
    
       


    }
}
