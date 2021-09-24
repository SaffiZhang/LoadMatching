using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings
{
    public interface IMatchingConfig
    {
        int MatchingBatchSize { get; set; }
    }
}
