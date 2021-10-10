using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Moq;
using MediatR;
using System.Collections.Generic;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment.Matchings;
using LoadLink.LoadMatching.Domain.Events;

using LoadLink.LoadMatching.Application.EquipmentPosting.DomainEventHandlers;
using LoadLink.LoadMatching.Domain.Entities;


namespace LoadLink.LoadMatching.Application.Test
{
    public class PlatformEquipmentLeadCreatedEventHandlerTest
    {
        private Point ottawa = new Point(-75.68861, 45.41972, 50);
        private Point cambridge = new Point(-80.29972, 43.36583, 50);

        private Mock<IEquipmentPostingRepository> mockEquipmentPostingRepository ;
        private Mock<IMediator> mockMediator ;

        private PostingBase posting;
        private PlatformEquipmentLead lead;
    
       [Fact]
        public async Task HandlerShould()
        {
            // 1. platform match platform , IsGlobleExclude = true , no 2nd lead
            Setup();
          
            var notification = new PlatformEquipmentLeadCreatedDomainEvent(lead, posting,posting , true);
            var handler = new PlatformEquipmentLeadCreatedDomainEventHandler( );
            
            await handler.Handle(notification, new CancellationToken());
            
            mockEquipmentPostingRepository.Verify(m => m.BulkInsertLeadTable(It.IsAny<IEnumerable<LeadBase>>()), Times.Never);
            mockMediator.Verify(m=>m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Never);
            
            // 2. platform match platform , IsGlobleExclude = false ,  2nd lead
            Setup();
            
            notification = new PlatformEquipmentLeadCreatedDomainEvent(lead, posting, posting,  false);
            handler = new PlatformEquipmentLeadCreatedDomainEventHandler();
            
            await handler.Handle(notification, new CancellationToken());
            
            mockEquipmentPostingRepository.Verify(m => m.BulkInsertLeadTable(It.IsAny<IEnumerable<LeadBase>>()), Times.Never);
            mockMediator.Verify(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);




        }
        private void Setup()
        {
            mockEquipmentPostingRepository = new Mock<IEquipmentPostingRepository>();
            mockMediator = new Mock<IMediator>();
            mockEquipmentPostingRepository.Setup(m => m.BulkInsertLeadTable(It.IsAny<IEnumerable< LeadBase>>())).Verifiable();
            mockMediator.Setup(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>())).Verifiable();
            posting = FakePosting.NotPlatformPostingOfCorridor("");
            posting.UpdateDistanceAndPointId(FakePosting.PostingDistanceAndPointId(ottawa, cambridge));
            lead = new PlatformEquipmentLead(posting, posting, "N", true);

        }
        
       
    }
}
