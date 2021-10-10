using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.Events;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment.Matchings
{
    public class SecondaryLoadLead : LeadBase
    {
  
        public SecondaryLoadLead(PostingBase posting, PostingBase matchedPosting, string dirO) : base(posting, matchedPosting, dirO)
        {
            //this.PType = "E";
            this.LeadType = "S";
            this.EToken = matchedPosting.Token; 
            this.LToken = posting.Token;
            AddDomainEvent(new SecondaryLoadLeadCreatedDomainEvent(this));
        }
    }
}
