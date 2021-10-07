using MediatR;
using LoadLink.LoadMatching.Domain.Events;

using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System.Threading.Tasks;
using System.Threading;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.DomainEventHandlers
{
    public class DatEquipmentLeadCreatedDomainEventHandler : INotificationHandler<DatEquipmentLeadCreatedDomainEvent>
    {
        private IEquipmentPostingRepository _equipmentPostingRespository;

        public DatEquipmentLeadCreatedDomainEventHandler(IEquipmentPostingRepository equipmentPostingRespository)
        {
            _equipmentPostingRespository = equipmentPostingRespository;
        }

        public async Task Handle(DatEquipmentLeadCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            //await _equipmentPostingRespository.SaveDatLead(notification.LeadBase);
        }
    }
}
