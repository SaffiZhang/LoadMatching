using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public interface ICreateLead
    {
        Task<LeadBase> Create(PostingBase posting, PostingBase matchedPosting, bool? isGlobalExcluded=null);
   }
}
