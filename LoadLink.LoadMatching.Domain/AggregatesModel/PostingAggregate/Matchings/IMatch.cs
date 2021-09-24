using System.Threading.Tasks;
using System.Collections.Generic;


namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings
{
    public interface IMatch
    {
        Task<IEnumerable<LeadBase>> Match(PostingBase posting, IEnumerable<PostingBase> preMatchedPostings,
           bool isMatchToPlatformPosting, bool? isGlobalExcluded=false);
    }
}
