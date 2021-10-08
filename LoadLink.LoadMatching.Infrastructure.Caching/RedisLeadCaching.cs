using System;
using System.Collections.Generic;
using StackExchange.Redis;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using Newtonsoft.Json;

namespace LoadLink.LoadMatching.Caching
{
    public class RedisLeadCaching : ILeadCaching
    {
       
        private IDatabase _leadDb;
        public RedisLeadCaching(RedisConnection redisConnection)
        {
            _leadDb = redisConnection.Redis.GetDatabase();

        }

        public async Task InsertSingleLead(LeadType leadType, bool isDeleted, LeadBase leadBase)
        {

            var token = leadType == LeadType.EquipmentLead ? leadBase.EToken : leadBase.LToken;
            var key = GetRedisKey(leadType, false, token);
            leadBase.Id = 1;
            await _leadDb.SortedSetAddAsync(key,
                                                   JsonConvert.SerializeObject(leadBase), leadBase.Id);
            leadBase.Id = 2;
            await _leadDb.SortedSetAddAsync(key,
                                                  JsonConvert.SerializeObject(leadBase), leadBase.Id);
            var objs = await _leadDb.SortedSetRangeByRankAsync(key, start: 0, stop: -1);
            if (objs == null)
                return;
            if (objs.Length == 0)
                return;
            var leads = new List<LeadBase>();
            foreach (var obj in objs)
                leads.Add(JsonConvert.DeserializeObject<LeadBase>(obj));
            var t = leads;
        }
           


        

        public async Task DeleteLead(LeadType leadType, int token, int leadId)
        {
            var key = GetRedisKey(leadType, false, token);
            
            await _leadDb.SortedSetRemoveAsync(key, leadId);
            await _leadDb.SortedSetAddAsync(key,leadId.ToString(), leadId);

        }
        

        private string GetRedisKey(LeadType leadType, bool isDeleted, int token)
        {
            var isDeletedInt = isDeleted ? 1 : 0;
            return leadType.ToString() + "-" + isDeletedInt.ToString() + "-" + token.ToString();
        }

        public async Task<IEnumerable<LeadBase>> GetLeadsByTokenAndMaxLeadId(LeadType leadType, int token, int maxLeadId)
        {
            var key = GetRedisKey(leadType, false, token);
           
            var objs = await _leadDb.SortedSetRangeByRankAsync(key,start:maxLeadId, stop:-1);
            if (objs == null)
                return null;
            if (objs.Length == 0)
                return null;
            var leads = new List<LeadBase>();
            foreach (var obj in objs)

                leads.Add(JsonConvert.DeserializeObject<LeadBase>(obj));
            return leads;
        }

        public async Task<IEnumerable<int>> GetDeleteLeadsByTokenAndMaxLeadId(LeadType leadType, int token, int maxLeadId)
        {
            var key = GetRedisKey(leadType, false, token);
            var objs = await _leadDb.SortedSetRangeByRankAsync(key, start: maxLeadId, stop: -1);
            var leads = new List<int>();
            foreach (var obj in objs)

                leads.Add(int.Parse(obj));
            return leads;
        }

        public async Task CleanLeadsCaching(LeadType leadType, int token, bool isDeleted)
        {
            var key = GetRedisKey(leadType, isDeleted, token);
            try
            {
                var i = _leadDb.SortedSetLength(key);
                if (i>0)
                {
                    var item = _leadDb.SortedSetRangeByScore(key, 0);

                    _leadDb.SortedSetRemove(key, item);
                    var objs = await _leadDb.SortedSetRangeByRankAsync(key, start: 0, stop: -1);
                    var t = objs.Length;
                }
            }
            catch(Exception ex)
            {

            }
            
            
            
            
            //await _leadDb.SortedSetRemoveRangeByRankAsync(key,start:0, stop:-1);
        }
    }
}
