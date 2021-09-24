using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings
{
    public interface IMatchingServiceFactory
    {
        IMatch GetService( PostingType postingType, MatchingType matchingType);
    }
}
