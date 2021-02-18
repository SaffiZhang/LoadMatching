using System;

namespace LoadLink.LoadMatching.Application.LegacyDeleted.Models.Queries
{
    public class GetUserLegacyDeletedQuery
    {
        public int TokenID { get; set; }
        public int Token { get; set; }
        public bool LLDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
