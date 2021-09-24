using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings
{
    public class MatchingConfig:IMatchingConfig
    {
        public int MatchingBatchSize { get; set; }
    }
}
