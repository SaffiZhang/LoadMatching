using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment
{
    [Table("DATLoadLead")]
    public class EquipmentLead:LeadBase
    {
        public EquipmentLead()
        {

        }

        public EquipmentLead(PostingBase posting,
            PostingBase matchedPosting, string dirO) : base(posting, matchedPosting, dirO)
        {

        }
    }
}
