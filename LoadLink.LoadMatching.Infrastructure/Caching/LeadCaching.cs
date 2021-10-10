using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis.Extensions.Core.Abstractions;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace LoadLink.LoadMatching.Infrastructure.Caching
{
    
    public class LeadCaching:ILeadCaching
    {
        private IRedisCacheClient _redisCacheClient;
        private IServiceProvider _serviceProvider;
        public LeadCaching(IRedisCacheClient redisCacheClient, IServiceProvider serviceProvider)
        {
            _redisCacheClient = redisCacheClient;
            _serviceProvider = serviceProvider;
        }

       // when a new posting is created, after saving to db, save leads to redis
       // when data loader can't find any leads of a posting query database and save to redis

        public async Task BulkInsertLeads(LeadPostingType leadType, int token, IEnumerable<LeadBase> leads)
        {
            if (leads == null)
                return;
            if (leads.Count() == 0)
                return;
            var custCD = leads.FirstOrDefault().CustCD;
            foreach (var l in leads)
            {//leadtype + deleded + token + leadId
                var mToken = l.EToken == token ? l.LToken : l.EToken;
                var key = GetLeadKey(leadType, custCD, token, l.ID,mToken);
               
                var dates = new List<DateTime>(){ l.DateAvail, l.MDateAvail };
                await _redisCacheClient.Db0.AddAsync(key,l,dates.Min() );
            }
            await UpdateLeadCount(leadType, custCD, token, leads.Count());
           
        }
        // UI get leads of a posting
        public async Task<IEnumerable<LeadBase>> GetLeadsByToken(LeadPostingType leadType, string custCD, int token)
        {
            IEnumerable<string> keys;
            IDictionary<string, LeadBase> list;
            keys = await _redisCacheClient.Db0.SearchKeysAsync(GetTokenKey(leadType, custCD, token) + "*");
            list = await _redisCacheClient.Db0.GetAllAsync<LeadBase>(keys);


            var result = new List<LeadBase>();
            foreach (var l in list)
                result.Add(l.Value);
            return result;

        }
        // matching service create a 2nd lead, save to DB and save to redis
        public async Task InsertSingleLead(LeadPostingType leadType, int token, LeadBase lead)
        {
            var mToken = lead.EToken == token ? lead.LToken : lead.EToken;
            var key = GetLeadKey(leadType, lead.CustCD, token, lead.ID, mToken);
           
            var dates = new List<DateTime>() { lead.DateAvail, lead.MDateAvail };
            await _redisCacheClient.Db0.AddAsync(key, lead, dates.Min());
            await UpdateLeadCount(leadType, lead.CustCD, token, 1);
        }
  
        //Interface showing leadcount for all postings for one customer
        public async Task<IEnumerable<PostingLeadCount>> GetPostingLeadCountByCustCD(LeadPostingType leadType, PostingLeadCount custCD)
        {
            IEnumerable<string> keys;
            IDictionary<string, PostingLeadCount> list;

            keys = await _redisCacheClient.Db0.SearchKeysAsync("LeadCount-"+leadType.ToString() + "-" + custCD + "*");
            list = await _redisCacheClient.Db0.GetAllAsync<PostingLeadCount>(keys);


            var result = new List<PostingLeadCount>();
            foreach (var l in list)
                result.Add(l.Value);
            return result;
        }
        // Lead count is updated and created by inserting leads to redis
        private async Task UpdateLeadCount(LeadPostingType leadType, string custCD, int token, int leadCount)
        {
            var key = GetLeadCountKey(leadType, custCD, token);
            var leadCountRecord = await _redisCacheClient.Db0.GetAsync<PostingLeadCount>(key);
            if (leadCountRecord==null)
            {
                var postingLeadCount = new PostingLeadCount(custCD, leadType, token, leadCount);
                await _redisCacheClient.Db0.AddAsync(key, postingLeadCount);
            }
            else
            {
                var postingLeadCount = new PostingLeadCount(custCD, leadType, token, leadCount + leadCountRecord.LeadCount);
                await _redisCacheClient.Db0.RemoveAsync(key);
                await _redisCacheClient.Db0.AddAsync(key, postingLeadCount);
            }
        }

        private string GetTokenKey(LeadPostingType leadType, string custCD, int token)
        {
            return leadType.ToString() + "-" + custCD + "-" + "0" + "-" + token.ToString() + "-";
        }
        private string GetLeadKey(LeadPostingType leadType, string custCD, int token, int leadId, int mToken)
        {
            return GetTokenKey(leadType, custCD, token) + leadId.ToString() +"-"+mToken  ;
        }
        private string GetLeadCountKey(LeadPostingType leadType, string custCD, int token)
        {
            return "LeadCount-" +leadType.ToString() + "-" + custCD + token.ToString() ;
        }
        public Task CleanLeadsCaching(LeadPostingType leadType, int token, bool isDeleted)
        {
            throw new NotImplementedException();
        }
        //when a posting is deleted, all the 2nd lead of it are deleted
        public async Task DeleteLead(LeadPostingType leadType, int token)
        {
            IEnumerable<string> keys;
            keys = await _redisCacheClient.Db0.SearchKeysAsync(leadType.ToString() + "*-" + token.ToString());
            foreach (var key in keys)
            {
                var lead = await _redisCacheClient.Db0.GetAsync<LeadBase>(key);
                await _redisCacheClient.Db0.RemoveAsync(key);
                var secondaryLeadType = leadType == LeadPostingType.EquipmentLead ? LeadPostingType.LoadLead : LeadPostingType.EquipmentLead;
                var deletedToken= leadType == LeadPostingType.EquipmentLead ? lead.LToken : lead.EToken;
                await UpdateLeadCount(secondaryLeadType, lead.CustCD, deletedToken, -1);
            }
               
        }
       
        public Task<IEnumerable<int>> GetDeleteLeadsByToken(LeadPostingType leadType, int token, int maxLeadId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLead(LeadPostingType leadType, string custCD, int token, int leadId)
        {
            throw new NotImplementedException();
        }
    }
}
