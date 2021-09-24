using System;
using Xunit;
using LoadLink.LoadMatching.Domain.Seedwork;
using System.Collections.Generic;
using MediatR;

using System.Threading.Tasks;
using System.Linq;

using LoadLink.LoadMatching.Domain.Entities;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using Moq;
using System.Threading;

namespace LoadLink.LoadMatching.Domain.Test
{
    public class MatchingServiceTest
    {

        private Point ottawa = new Point(-75.68861, 45.41972, 50);
        private Point cambridge = new Point(-80.29972, 43.36583, 50);
        private Point kanata = new Point(-75.91139, 45.34306, 50);// in ottawa radius
        private Point peterborough = new Point(-78.32083, 44.28278, 50);// in corridor
        private Point mississauga = new Point(-79.61167, 43.58167);//in corridor
        private Point waterloo = new Point(-80.51083, 43.46389, 50);//in cambridge radius
        private Point sudbury = new Point(-80.99667, 46.46222);//out of corridor and radius


        private PostingBase posting;
        private List<PostingBase> preMatchedPostings;

        private MatchingConfig matchingConfig = new MatchingConfig();

        private Mock<IMediator> mockMediator;
        private Mock<IEquipmentPostingRepository> mockEquipmentPostingRepository;
        
        private Mock<IFillNotPlatformPosting> mockFillNotPlatformPosting;
    

        [Fact]
        public async Task MatchingShould()
        {
            // matching to platform posting only by radius
            matchingConfig.MatchingBatchSize = 3;
            InitMock();
            posting = FakePosting.NotPlatformPostingOfCorridor("");
            SetupPlatFormPosting();

            var matchingService = new PlatformEquipmentMatching(matchingConfig, mockMediator.Object,mockFillNotPlatformPosting.Object);
            var leads = await matchingService.Match(posting, preMatchedPostings, true);
            Assert.Equal(2, leads.Count());

            // matching to platform posting by corridor
            matchingConfig.MatchingBatchSize = 5;
            InitMock();
            posting = FakePosting.NotPlatformPostingOfCorridor("C");
            SetupPlatFormPosting();
            matchingService = new PlatformEquipmentMatching(matchingConfig, mockMediator.Object, mockFillNotPlatformPosting.Object);
            leads = await matchingService.Match(posting, preMatchedPostings, true);
            Assert.Equal(3, leads.Count());

            // matching to not platform posting by corridor
            matchingConfig.MatchingBatchSize = 5;
            InitMock();
            SetupNotPlatFormPosting();
            var datMatchingService = new DatEquipmentMatching(matchingConfig, mockMediator.Object, mockFillNotPlatformPosting.Object);
            leads = await datMatchingService.Match(posting, preMatchedPostings, false);
            
            Assert.Single(leads);

            matchingConfig.MatchingBatchSize = 2;
            InitMock();
            SetupNotPlatFormPosting();
            var legacyMatchingService = new LegacyEquipmentMatching(matchingConfig, mockMediator.Object, mockFillNotPlatformPosting.Object);
            leads = await legacyMatchingService.Match(posting, preMatchedPostings, false);

            Assert.Single(leads);



        }
        private void InitMock()
        {

            mockEquipmentPostingRepository = new Mock<IEquipmentPostingRepository>();
            mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>())).Verifiable();
           
            mockFillNotPlatformPosting = new Mock<IFillNotPlatformPosting>();
            
        }
        private void SetupPlatFormPosting()
        {

            posting.UpdateDistanceAndPointId(FakePosting.PostingDistanceAndPointId(ottawa, cambridge));
            //5 postings in the lists
            preMatchedPostings = new List<PostingBase>();
            preMatchedPostings.Add(posting);

            var p1 = FakePosting.NotPlatformPosting();
            p1.UpdateDistanceAndPointId(FakePosting.PostingDistanceAndPointId(kanata, waterloo));//in radius
            preMatchedPostings.Add(p1);

            var p2 = FakePosting.NotPlatformPosting();
            p2.UpdateDistanceAndPointId(FakePosting.PostingDistanceAndPointId(peterborough, mississauga));//in corridor
            preMatchedPostings.Add(p2);

            var p3 = FakePosting.NotPlatformPosting();
            p3.UpdateDistanceAndPointId(FakePosting.PostingDistanceAndPointId(cambridge, ottawa));//wrong direcion
            preMatchedPostings.Add(p3);

            var p4 = FakePosting.NotPlatformPosting();
            p4.UpdateDistanceAndPointId(FakePosting.PostingDistanceAndPointId(kanata, sudbury));//out of corridor
            preMatchedPostings.Add(p4);



        }
        private void SetupNotPlatFormPosting()
        {
            posting = FakePosting.NotPlatformPosting();
            posting.UpdateDistanceAndPointId(FakePosting.PostingDistanceAndPointId(kanata, waterloo));
            preMatchedPostings = new List<PostingBase>();
            //platform posting
            var p1 = FakePosting.NotPlatformPosting();
            preMatchedPostings.Add(p1);
           
            mockFillNotPlatformPosting.Setup(m => m.Fill(It.IsAny<PostingBase>())).ReturnsAsync(posting);

            


        }

    }
}
