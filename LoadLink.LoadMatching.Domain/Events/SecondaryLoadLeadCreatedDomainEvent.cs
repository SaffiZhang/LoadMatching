using MediatR;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Domain.Events
{
    public class SecondaryLoadLeadCreatedDomainEvent : INotification
    {
        public SecondaryLoadLeadCreatedDomainEvent(LeadBase leadBase)
        {
            LeadBase = leadBase;
        }

        public LeadBase LeadBase { get; }
    }
}
