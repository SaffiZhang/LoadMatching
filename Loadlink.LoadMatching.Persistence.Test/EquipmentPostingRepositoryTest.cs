//using System.Collections.Generic;
//using Xunit;
//using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
//using LoadLink.LoadMatching.Persistence.Utilities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Storage;
//using System.Threading.Tasks;
//using Moq;
//using AutoMapper;
//using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories;
//using LoadLink.LoadMatching.Persistence.Data;
//using System.Linq;
//using System;
//namespace LoadLink.LoadMatching.Persistence.Test
//{
//    public class EquipmentPostingRepositoryTest
//    {
        
//        private IConnectionFactory connectionFactory 
//            = new ConnectionFactory("Server = USITDB1.linklogi.com; Database = LoadMatching_POC3; Trusted_Connection = yes;");
//        private Mock<IMapper> mockMapper = new Mock<IMapper>();
//        private Mock<ILeadCaching> mockLeadCaching = new Mock<ILeadCaching>();
//       [Fact]
//        public async Task RepositoryBulkInsertTest()
//        {
//            mockLeadCaching.Setup(m => m.BulkInsertLeads(It.IsAny<LeadPostingType>(), It.IsAny<int>(), It.IsAny<IEnumerable<LeadBase>>()))
//                            .Verifiable();
//            var option = new DbContextOptionsBuilder<EquipmentPostingContext>()
//                 .UseSqlServer("Server=.;Initial Catalog=Microsoft.eShopOnContainers.Services.OrderingDb;Integrated Security=true");
//            var context = new EquipmentPostingContext()
//            var repository = new EquipmentPostingRepository(connectionFactory, mockMapper.Object);
            
//                await repository.BulkInsertLead(SetupData().ToList(), "EquipmentLead");
//            mockLeadCaching.Verify(m => m.BulkInsertLeads(It.IsAny<LeadPostingType>(), It.IsAny<int>(), It.IsAny<IEnumerable<LeadBase>>()), Times.Once);
//        }
//        private IEnumerable<LeadBase> SetupData()
//        {
//            var lead = FakePosting.LeadBase();
//            lead.LeadType = "P";
//            lead.PType = "L";
//            lead.EToken = 1;
//            lead.LToken = 1;


//            return new List<LeadBase>() { lead };

//        }
//    }
//}
