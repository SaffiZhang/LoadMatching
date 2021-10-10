//using MediatR;
//using LoadLink.LoadMatching.Domain.Events;

//using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
//using System.Threading.Tasks;
//using System.Threading;

//namespace LoadLink.LoadMatching.Application.EquipmentPosting.DomainEventHandlers
//{
//    public class LegacyEquipmentLeadCreatedDomainEventHandler : INotificationHandler<LegacyEquipmentLeadCreatedDomainEvent>
//    {
//        private IEquipmentPostingRepository _equipmentPostingRespository;

//        public LegacyEquipmentLeadCreatedDomainEventHandler(IEquipmentPostingRepository equipmentPostingRespository)
//        {
//            _equipmentPostingRespository = equipmentPostingRespository;
//        }

//        public async Task Handle(LegacyEquipmentLeadCreatedDomainEvent notification, CancellationToken cancellationToken)
//        {
//            //await _equipmentPostingRespository.SaveLegacyLead(notification.LeadBase);
//        }
//    }
//}
