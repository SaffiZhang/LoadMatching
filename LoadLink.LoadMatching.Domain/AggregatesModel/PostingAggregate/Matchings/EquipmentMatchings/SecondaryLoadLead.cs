using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.Events;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings
{
    public class SecondaryLoadLead : LeadBase
    {
  
        public SecondaryLoadLead(PostingBase posting, PostingBase matchedPosting, string dirO) : base(posting, matchedPosting, dirO)
        {
            this.PType = "E";
            this.LeadType = "S";
            AddDomainEvent(new SecondaryLoadLeadCreatedDomainEvent(this));
        }
    }
}
