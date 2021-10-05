using System;
using System.Collections.Generic;
using LoadLink.LoadMatching.IntegrationEventManager;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.IntetrationEvents
{
    public class PostingCreatedEvent:IIntegrationEvent
    {
        public PostingCreatedEvent(PostingBase posting, bool? isGlobalExclued)
        {
            Posting = posting;
            IsGlobalExclued = isGlobalExclued;
        }

        public PostingBase Posting { get; }
        public bool? IsGlobalExclued { get; }
    }
}
