using MediatR;
using LoadLink.LoadMatching.Domain.Events;

using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment.Matchings;
using System.Threading.Tasks;
using System.Threading;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.DomainEventHandlers
{
    public class PlatformEquipmentLeadCreatedDomainEventHandler : INotificationHandler<PlatformEquipmentLeadCreatedDomainEvent>
    {
        public PlatformEquipmentLeadCreatedDomainEventHandler()
        {
        }

        public async Task Handle(PlatformEquipmentLeadCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var lead = notification.LeadBase;
            //await _equipmentPostingRespository.SavePlatformLead(lead);

            if (notification.IsGlobalExcluded)
                return;

            //2nd lead
            var posting = notification.MatchPosting; //LoadPosting
            var matchedPosting = notification.Posting;//equipment posting


            posting.Add2ndLeads(new SecondaryLoadLead(posting, matchedPosting, lead.DirO));

    }
    }

}