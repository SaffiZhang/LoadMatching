//using System.Threading.Tasks;
//using System.Threading;
//using Xunit;
//using Moq;
//using System.Linq;
//using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
//using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings;
//using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
//using System;
//using System.Collections.Generic;
//using MediatR;
//using LoadLink.LoadMatching.Domain.Entities;
//using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;



//namespace LoadLink.LoadMatching.Application.Test
//{
//    public class LoadMatchingServiceTest
//    {
//        private Point ottawa = new Point(-75.68861, 45.41972, 50);
//        private Point cambridge = new Point(-80.29972, 43.36583, 50);
       

//        private PostingBase posting;
//        private List<PostingBase> preMatchedPostings;

//        private Mock<IEquipmentPostingRepository> mockEquipmentPostingRepository = new Mock<IEquipmentPostingRepository>();
//        private MatchingConfig matchingConfig = new MatchingConfig();
//        private Mock<IMediator> mockMediator = new Mock<IMediator>();
//        private Mock<IFillNotPlatformPosting> mockFillNotPlatformPosting = new Mock<IFillNotPlatformPosting>();

//        private Mock<IMatchingServiceFactory> mockMatchingServiceFactory = new Mock<IMatchingServiceFactory>();

   
//        [Fact]
//        public async Task CreatEquipmentPostingCommandHandlerShould()
//        {
//            var request = new CreatEquipmentPostingCommand() {
//                CustCD = "1", SrceCity = "1", SrceSt = "1", DestCity = "1", DestSt = "1", SrceRadius = 1, DestRadius = 1,
//                VehicleSize = "U", VehicleType = "V", PostingAttrib = "A"
//            };


//            Setup();


//            var handler = new LoadMatchingService(mockEquipmentPostingRepository.Object, mockMatchingServiceFactory.Object);

//             await handler.ExecuteAsync(CancellationToken.None);
//            //await handler.StopAsync(CancellationToken.None);

//            //save one posting to DB
           


//        }
//        private void Setup()
//        {

//            SetupData();
//            SetupObjects();
//            SetupRepository();

//        }
//        private void SetupData()
//        {
//            posting = FakePosting.NotPlatformPostingOfCorridor("");
//            posting.UpdateDistanceAndPointId(FakePosting.PostingDistanceAndPointId(ottawa, cambridge));
//            preMatchedPostings = new List<PostingBase>();
//            preMatchedPostings.Add(posting);
//        }
//        private void SetupObjects()
//        {
//            mockFillNotPlatformPosting.Setup(m => m.Fill(It.IsAny<PostingBase>())).ReturnsAsync(posting);
//            mockMediator.Setup(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>())).Verifiable();
//            matchingConfig.MatchingBatchSize = 5;

      


//            mockMatchingServiceFactory.Setup(m => m.GetService(PostingType.EquipmentPosting, MatchingType.Platform))
//                                      .Returns(new PlatformEquipmentMatching(matchingConfig, mockMediator.Object, mockFillNotPlatformPosting.Object, mockEquipmentPostingRepository.Object));
//            mockMatchingServiceFactory.Setup(m => m.GetService(PostingType.EquipmentPosting, MatchingType.Legacy))
//                                      .Returns(new LegacyEquipmentMatching(matchingConfig, mockMediator.Object, mockFillNotPlatformPosting.Object));
//            mockMatchingServiceFactory.Setup(m => m.GetService(PostingType.EquipmentPosting, MatchingType.Dat))
//                                      .Returns(new DatEquipmentMatching(matchingConfig, mockMediator.Object,mockFillNotPlatformPosting.Object));



//        }
//        private void SetupRepository()
//        {
//            mockEquipmentPostingRepository.Setup(m => m.SavePosting(It.IsAny<PostingBase>()))
//            .Returns(Task.FromResult(FakePosting.PostingDistanceAndPointId(ottawa, cambridge)));
//            mockEquipmentPostingRepository.Setup(m => m.GetDatPostingForMatching(It.IsAny<DateTime>(),
//                                                                                 It.IsAny<int>(),
//                                                                                 It.IsAny<int>(),
//                                                                                 It.IsAny<int>(),
//                                                                                 It.IsAny<int>(),
//                                                                                 It.IsAny<string>(),
//                                                                                 It.IsAny<int>()))
//                                            .ReturnsAsync(preMatchedPostings);
//            mockEquipmentPostingRepository.Setup(m => m.GetPlatformPostingForMatching(It.IsAny<string>(),
//                                                                                It.IsAny<DateTime>(),
//                                                                                It.IsAny<int>(),
//                                                                                It.IsAny<int>(),
//                                                                                It.IsAny<int>(),
//                                                                                It.IsAny<int>(),
//                                                                                It.IsAny<string>(),
//                                                                                It.IsAny<int>()))
//                                            .ReturnsAsync(preMatchedPostings);
//            mockEquipmentPostingRepository.Setup(m => m.GetLegacyPostingForMatching(It.IsAny<string>(),
//                                                                               It.IsAny<DateTime>(),
//                                                                               It.IsAny<int>(),
//                                                                               It.IsAny<int>(),
//                                                                               It.IsAny<int>(),
//                                                                               It.IsAny<int>(),
//                                                                               It.IsAny<string>(),
//                                                                               It.IsAny<int>()))
//                                           .ReturnsAsync(preMatchedPostings);
//        }
        
//    }
//    }
    
