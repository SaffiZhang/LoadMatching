using System.Threading.Tasks;
using Xunit;
using StackExchange.Redis;
using LoadLink.LoadMatching.Caching;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;


namespace LoadLink.LoadMatching.Caching.Test
{
    public class RedisLeadsCachingTest
    {
        [Fact]
        public async Task Test()
        {
            var connection = new RedisConnection(new RedisConfiguration() { Server = "10.50.7.236:6379", Password = "L0@dL1nk#1" });
            
           
            
            var redis = new RedisLeadCaching(connection);
            var lead = FakePosting.LeadBase();
            lead.Id = 1;
            await redis.InsertSingleLead(LeadType.EquipmentLead, false, lead);

           
            //var leads = await redis.GetLeadsByTokenAndMaxLeadId(LeadType.EquipmentLead, lead.EToken, 0);
            await redis.CleanLeadsCaching(LeadType.EquipmentLead, lead.EToken, true);
            await redis.CleanLeadsCaching(LeadType.EquipmentLead, lead.EToken, false);
            lead.Id = 1;
            await redis.InsertSingleLead(LeadType.EquipmentLead, false, lead);
            await redis.DeleteLead(LeadType.EquipmentLead, 0, 0);
            lead.Id = 2;
            await redis.InsertSingleLead(LeadType.EquipmentLead, false, lead);
            lead.Id = 3;
            await redis.InsertSingleLead(LeadType.EquipmentLead, false, lead);
           // leads = await redis.GetLeadsByTokenAndMaxLeadId(LeadType.EquipmentLead, lead.EToken, 0);
           // leads= await redis.GetLeadsByTokenAndMaxLeadId(LeadType.EquipmentLead, lead.EToken, 1);
           // await redis.DeleteLead(LeadType.EquipmentLead, lead.EToken, 1);
           //var deletes= await redis.GetDeleteLeadsByTokenAndMaxLeadId(LeadType.EquipmentLead, lead.EToken, 1);
           //leads= await redis.GetLeadsByTokenAndMaxLeadId(LeadType.EquipmentLead, lead.EToken, 1);
           // Assert.NotNull(leads);

        }
    }
}
