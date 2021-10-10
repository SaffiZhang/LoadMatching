using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Load
{
    [Table("LoadLead")]
    public class LoadLead:LeadBase
    {
        public LoadLead()
        {

        }

        public LoadLead(PostingBase posting,
            PostingBase matchedPosting, string dirO) : base(posting, matchedPosting, dirO)
        {

        }
    }
}
