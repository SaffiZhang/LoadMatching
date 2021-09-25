using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.Events;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings
{
    public class PlatformEquipmentLead : LeadBase
    {
        public PlatformEquipmentLead(PostingBase posting, PostingBase matchedPosting,string dirO,  bool? isGlobalExcluded ) : base(posting, matchedPosting,  dirO,  isGlobalExcluded)
        {
            LeadType = "P";
            this.PType = "L";
            this.EToken = posting.Token;
            this.LToken = matchedPosting.Token;
            AddDomainEvent(new PlatformEquipmentLeadCreatedDomainEvent(this, posting, matchedPosting, isGlobalExcluded??false ));
        }
    }
}
