using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using MediatR;

namespace LoadLink.LoadMatching.Domain.Events
{
    public class DatEquipmentLeadCreatedDomainEvent : INotification
    {
        public DatEquipmentLeadCreatedDomainEvent(LeadBase leadBase)
        {
            LeadBase = leadBase;
        }

        public LeadBase LeadBase { get; }
    }
}
