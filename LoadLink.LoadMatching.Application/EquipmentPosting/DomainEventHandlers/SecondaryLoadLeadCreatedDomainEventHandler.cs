//using MediatR;
//using LoadLink.LoadMatching.Domain.Events;

//using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
//using System.Threading.Tasks;
//using System.Threading;

//namespace LoadLink.LoadMatching.Application.EquipmentPosting.DomainEventHandlers
//{
//    public class SecondaryLoadLeadCreatedDomainEventHandler : INotificationHandler<SecondaryLoadLeadCreatedDomainEvent>
//    {
//        IEquipmentPostingRepository _equipmentPostingRespository;

//        public SecondaryLoadLeadCreatedDomainEventHandler(IEquipmentPostingRepository equipmentPostingRespository)
//        {
//            _equipmentPostingRespository = equipmentPostingRespository;
//        }

//        public async Task Handle(SecondaryLoadLeadCreatedDomainEvent notification, CancellationToken cancellationToken)
//        {
//            notification.LeadBase
//        }
//    }
//}
