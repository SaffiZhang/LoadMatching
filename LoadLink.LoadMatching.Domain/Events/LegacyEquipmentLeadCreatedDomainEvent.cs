using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using MediatR;


namespace LoadLink.LoadMatching.Domain.Events
{
    public class LegacyEquipmentLeadCreatedDomainEvent : INotification
    {
        public LegacyEquipmentLeadCreatedDomainEvent(LeadBase leadBase) 
        {
        }
        public LeadBase LeadBase { get; }
    }
}
