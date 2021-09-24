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
            this.PType = "L";
            this.LeadType = "P";
            AddDomainEvent(new PlatformEquipmentLeadCreatedDomainEvent(this, posting, matchedPosting, isGlobalExcluded??false ));
        }
    }
}
