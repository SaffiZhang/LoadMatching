using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis.Extensions.Core.Abstractions;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Caching
{
    
    public class LeadCaching:ILeadCaching
    {
        private IRedisCacheClient _redisCacheClient;

        public LeadCaching(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task BulkInsertLeads(LeadType leadType, int token, IEnumerable<LeadBase> leads)
        {
            var values = new List<Tuple<string, LeadBase>>();
            foreach (var l in leads)
            {//leadtype + deleded + token + leadId
                var key = GetTokenKey(leadType,token) + l.Id.ToString();
                values.Add(new Tuple<string, LeadBase>(key, l));
            }
            await _redisCacheClient.Db0.AddAllAsync(values, DateTimeOffset.Now.AddMinutes(30));
        }

        public Task CleanLeadsCaching(LeadType leadType, int token, bool isDeleted)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLead(LeadType leadType, int token, int leadId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<int>> GetDeleteLeadsByToken(LeadType leadType, int token, int maxLeadId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LeadBase>> GetLeadsByToken(LeadType leadType, int token)
        {

            var keys = await _redisCacheClient.Db0.SearchKeysAsync(GetTokenKey(leadType, token) + "*");
            var list= await _redisCacheClient.Db0.GetAllAsync<LeadBase>(keys);
            var result = new List<LeadBase>();
            foreach (var l in list)
                result.Add(l.Value);
            return result;
            
        }
        private string GetTokenKey(LeadType leadType, int token)
        {
            return leadType.ToString() + "-" + "0" + "-" + token.ToString() + "-";
        }
    }
}
