using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Moq;
using MediatR;

using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.Events;

using LoadLink.LoadMatching.Application.EquipmentPosting.DomainEventHandlers;

namespace LoadLink.LoadMatching.Application.Test
{
    public class SecondaryLoadLeadCreatedDomainEventHandlerTest
    {
        private Mock<IEquipmentPostingRepository> mockEquipmentPostingRepository = new Mock<IEquipmentPostingRepository>();
        [Fact]
        public async Task SecondaryEquipmentLeadCreatedDomainEventHandlerShould()
        {
            mockEquipmentPostingRepository.Setup(m => m.Save2ndLead(It.IsAny<LeadBase>())).Verifiable();
            var handler = new SecondaryLoadLeadCreatedDomainEventHandler(mockEquipmentPostingRepository.Object);
            await handler.Handle(new SecondaryLoadLeadCreatedDomainEvent(FakePosting.LeadBase()), new CancellationToken());
            mockEquipmentPostingRepository.Verify(m => m.Save2ndLead(It.IsAny<LeadBase>()), Times.Once);
        }
    }
}
