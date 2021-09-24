using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public interface IPostingBaseQueries
    {

        Task<IEnumerable<PostingBase>> GetListAsync(string custCD, string mileageProvider, int pageSize, int pageNum, DateTime? lastRequestTime, bool? getDAT = false);
        Task<IEnumerable<PostingBase>> GetListLLAsync(string custCd, string mileageProvider, DateTime liveLeadTime, int pageSize, int pageNum, DateTime? lastRequestTime, bool? getDAT = false);
        Task<PostingBase> GetAsync(int token, string custCd, string mileageProvider);
        Task<IEnumerable<LeadBase>> GetListAsync(string custCD, string mileageProvider);
        Task<IEnumerable<LeadBase>> GetByPostingAsync(string custCD, int postingId, string mileageProvider);
        Task<IEnumerable<LeadBase>> GetCombinedAsync(string custCD, int postingId, string mileageProvider,
                                                                                int leadsCap, bool getDAT);
        Task<IEnumerable<LeadBase>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);
 
                                                                               
    }
}
