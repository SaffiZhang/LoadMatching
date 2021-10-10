using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using LoadLink.LoadMatching.Infrastructure.Caching;
using StackExchange.Redis.Extensions.Core.Abstractions;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System.Linq;

namespace LoadLink.LoadMatching.Persistence.Test
{
    public class LeadCachingTest
    {
        private IRedisCacheClient _redisCacheClient;
        private IServiceProvider _serviceProvider;

        public LeadCachingTest(IRedisCacheClient redisCacheClient, IServiceProvider serviceProvider)
        {
            _redisCacheClient = redisCacheClient;
            _serviceProvider = serviceProvider;
        }

        [Fact]
        public async Task BulkInsertLeadTest()
        {
            var leadCaching = new LeadCaching(_redisCacheClient,_serviceProvider);
            var lead = FakePosting.LeadBase();
            lead.Id = 1;
            var lead2 = FakePosting.LeadBase();
            lead2.Id = 2;
            await leadCaching.BulkInsertLeads(LeadPostingType.EquipmentLead, lead.EToken, new List<LeadBase>() { lead, lead2 });
            var leads = await leadCaching.GetLeadsByToken(LeadPostingType.EquipmentLead,lead.CustCD, lead.EToken);
            Assert.Equal(2, leads.Count());
            Assert.Equal(1, leads.FirstOrDefault().Id);


        }
    }
}
