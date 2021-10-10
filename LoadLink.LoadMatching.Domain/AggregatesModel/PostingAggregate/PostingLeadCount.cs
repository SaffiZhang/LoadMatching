using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public class PostingLeadCount
    {
        public PostingLeadCount(string custCD, LeadPostingType leadPostingType, int token, int leadCount)
        {
            CustCD = custCD;
            LeadPostingType = leadPostingType;
            Token = token;
            LeadCount = leadCount;
        }

        public string CustCD { get; set; }
        public LeadPostingType LeadPostingType { get; set; }
        public int Token { get; set; }
        public int LeadCount { get; set; }
    }
}
