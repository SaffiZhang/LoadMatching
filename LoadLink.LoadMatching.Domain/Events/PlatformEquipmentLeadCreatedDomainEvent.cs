using MediatR;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Domain.Events
{

    public class PlatformEquipmentLeadCreatedDomainEvent : INotification
    {
        public PlatformEquipmentLeadCreatedDomainEvent(LeadBase leadBase, PostingBase posting, PostingBase matchPosting, bool isGlobalExcluded)
        {
            LeadBase = leadBase;
            Posting = posting;
            MatchPosting = matchPosting;
            IsGlobalExcluded = isGlobalExcluded;
        }

        public LeadBase LeadBase { get; }
        public PostingBase Posting { get; }
        public PostingBase MatchPosting { get; }
        public bool IsGlobalExcluded { get; }
    }
}
