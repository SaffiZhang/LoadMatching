using LoadLink.LoadMatching.Persistence.Utilities;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories;
using LoadLink.LoadMatching.Persistence.Data;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using StackExchange.Redis.Extensions.Core.Abstractions;
using LoadLink.LoadMatching.Infrastructure.Caching;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System;

namespace LoadLink.LoadMatching.Persistence.Test
{
    public class EquipmentPostingRepositoryIntegratedWithCachingTest
    {
        private IRedisCacheClient _redisCacheClient;
        private IServiceProvider _serviceProvider;
        private ILeadCaching _leadCaching;
        private IMapper _mapper;
        private IConnectionFactory _connectionFactory
            = new ConnectionFactory("Server = USITDB1.linklogi.com; Database = LoadMatching_POC3; Trusted_Connection = yes;");
        private readonly EquipmentPostingContext _equipmentPostingContext;

        public EquipmentPostingRepositoryIntegratedWithCachingTest(IRedisCacheClient redisCacheClient, IServiceProvider serviceProvider, ILeadCaching leadCaching, IMapper mapper, IConnectionFactory connectionFactory, EquipmentPostingContext equipmentPostingContext)
        {
            _redisCacheClient = redisCacheClient;
            _serviceProvider = serviceProvider;
            _leadCaching = leadCaching;
            _mapper = mapper;
            _connectionFactory = connectionFactory;
            _equipmentPostingContext = equipmentPostingContext;
        }

        [Fact]
        public async Task RepositoryBulkInsertTest()
        {
            var leadCaching = new LeadCaching(_redisCacheClient, _serviceProvider);
            var repository = new EquipmentPostingRepository(_connectionFactory, _mapper, _equipmentPostingContext);
       //     await repository.BulkInsertLead(SetupData().ToList(), "EquipmentLead");
        }

        private IEnumerable<LeadBase> SetupData()
        {
            var lead = FakePosting.LeadBase();
            lead.LeadType = "P";
       //     lead.PType = "L";
            lead.EToken = 1;
            lead.LToken = 1;


            return new List<LeadBase>() { lead };

        }
    }
}
