using System;
using System.Collections.Generic;
using LoadLink.LoadMatching.Domain.Events;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings
{
    public class LegacyEquipmentLead : LeadBase
    {
        

        public LegacyEquipmentLead(PostingBase posting, PostingBase matchedPosting,  string dirO  ) : base(posting, matchedPosting,  dirO)
        {
            this.PType = "L";
            this.LeadType = "P";
            AddDomainEvent(new LegacyEquipmentLeadCreatedDomainEvent(this));
        }

        
    }
}
