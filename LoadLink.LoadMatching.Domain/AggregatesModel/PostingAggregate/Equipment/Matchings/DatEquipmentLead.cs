using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.Events;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment.Matchings
{
    public class DatEquipmentLead : LeadBase
    {
        

        public DatEquipmentLead(PostingBase posting, PostingBase matchedPosting, string dirO) : base(posting, matchedPosting, dirO)
        {
            LeadType = "P";
            //this.PType = "L";
            this.EToken = posting.Token;
            this.LToken = matchedPosting.Token;
            //AddDomainEvent(new DatEquipmentLeadCreatedDomainEvent(this));
            
        }
    }
}
