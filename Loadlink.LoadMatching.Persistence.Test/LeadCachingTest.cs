using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using LoadLink.LoadMatching.Persistence.Caching;
using StackExchange.Redis.Extensions.Core.Abstractions;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System.Linq;

namespace LoadLink.LoadMatching.Persistence.Test
{
    public class LeadCachingTest
    {
        private IRedisCacheClient _redisCacheClient;

        public LeadCachingTest(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        [Fact]
        public async Task BulkInsertLeadTest()
        {
            var leadCaching = new LeadCaching(_redisCacheClient);
            var lead = FakePosting.LeadBase();
            lead.Id = 1;
            var lead2 = FakePosting.LeadBase();
            lead2.Id = 2;
            await leadCaching.BulkInsertLeads(LeadType.EquipmentLead, lead.EToken, new List<LeadBase>() { lead, lead2 });
            var leads = await leadCaching.GetLeadsByToken(LeadType.EquipmentLead, lead.EToken);
            Assert.Equal(2, leads.Count());
            Assert.Equal(1, leads.FirstOrDefault().Id);


        }
    }
}
