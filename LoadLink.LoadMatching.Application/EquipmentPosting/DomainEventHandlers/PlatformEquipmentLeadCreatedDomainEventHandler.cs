using MediatR;
using LoadLink.LoadMatching.Domain.Events;

using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings;
using System.Threading.Tasks;
using System.Threading;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.DomainEventHandlers
{
    public class PlatformEquipmentLeadCreatedDomainEventHandler : INotificationHandler<PlatformEquipmentLeadCreatedDomainEvent>
    {

        IEquipmentPostingRepository _equipmentPostingRespository;
        IMediator _mediator;

        public PlatformEquipmentLeadCreatedDomainEventHandler(IEquipmentPostingRepository equipmentPostingRespository, IMediator mediator)
        {
            _equipmentPostingRespository = equipmentPostingRespository;
            _mediator = mediator;
        }

        public async Task Handle(PlatformEquipmentLeadCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var lead = notification.LeadBase;
            await _equipmentPostingRespository.SavePlatformLead(lead);
           
            if (notification.IsGlobalExcluded)
                return;

            //2nd lead
            var posting = notification.MatchPosting; //LoadPosting
            var matchedPosting = notification.Posting;//equipment posting

            var secondaryLead = new SecondaryLoadLead(posting, matchedPosting, lead.DirO);
            //2nd load lead event will be published
            foreach (var e in secondaryLead.DomainEvents)
                    await _mediator.Publish(e);

        }
    }

}