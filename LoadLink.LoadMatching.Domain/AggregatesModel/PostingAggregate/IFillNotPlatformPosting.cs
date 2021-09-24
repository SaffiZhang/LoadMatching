using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public interface IFillNotPlatformPosting
    {
        Task<PostingBase> Fill(PostingBase posting);
    }
}
