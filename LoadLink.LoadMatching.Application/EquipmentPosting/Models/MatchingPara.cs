using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Models
{
    public class MatchingPara
    {
        public MatchingPara()
        {
        }

        public MatchingPara(PostingBase posting, bool? isGlobalExcluded)
        {
            Posting = posting;
            IsGlobalExcluded = isGlobalExcluded;
        }

        public PostingBase Posting { get; set; }
        public bool? IsGlobalExcluded { get; set; } 
    }
}
