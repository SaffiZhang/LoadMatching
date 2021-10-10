
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment
{
    [Table("DATEquipmentLead")]
    public class DatEquipmentLead : LeadBase
    {
        public DatEquipmentLead()
        {

        }

        public DatEquipmentLead(PostingBase posting,
            PostingBase matchedPosting, string dirO) : base(posting, matchedPosting, dirO)
        {

        }
    }
}
