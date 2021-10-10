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
    public class DatEquipmentLeadCreatedDomainEventHandlerTest
    {
        private Mock<IEquipmentPostingRepository> mockEquipmentPostingRepository = new Mock<IEquipmentPostingRepository>();
        [Fact]
        public async Task DatEquipmentLeadCreatedDomainEventHandlerShould()
        {
            //mockEquipmentPostingRepository.Setup(m => m.SaveDatLead(It.IsAny<LeadBase>())).Verifiable();
            //var handler = new DatEquipmentLeadCreatedDomainEventHandler(mockEquipmentPostingRepository.Object);
            //await handler.Handle(new DatEquipmentLeadCreatedDomainEvent(FakePosting.LeadBase()), new CancellationToken());
            //mockEquipmentPostingRepository.Verify(m => m.SaveDatLead(It.IsAny<LeadBase>()), Times.Never);
        }
               
    }
}
