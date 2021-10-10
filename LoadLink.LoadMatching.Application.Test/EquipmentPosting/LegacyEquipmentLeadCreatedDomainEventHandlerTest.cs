using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Moq;
using MediatR;

using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.Events;

using LoadLink.LoadMatching.Application.EquipmentPosting.DomainEventHandlers;
using System;

namespace LoadLink.LoadMatching.Application.Test
{
   public class LegacyEquipmentLeadCreatedDomainEventHandlerTest
    {
        private Mock<IEquipmentPostingRepository> mockEquipmentPostingRepository = new Mock<IEquipmentPostingRepository>();
        [Fact]
        public async Task LegacyEquipmentLeadCreatedDomainEventHandlerShould()
        {
            //mockEquipmentPostingRepository.Setup(m => m.SaveLegacyLead(It.IsAny<LeadBase>())).Verifiable();
            //var handler = new LegacyEquipmentLeadCreatedDomainEventHandler(mockEquipmentPostingRepository.Object);
            //await handler.Handle(new LegacyEquipmentLeadCreatedDomainEvent(FakePosting.LeadBase()), new CancellationToken());
            //mockEquipmentPostingRepository.Verify(m => m.SaveLegacyLead(It.IsAny<LeadBase>()), Times.Never);
        }
    }
}
