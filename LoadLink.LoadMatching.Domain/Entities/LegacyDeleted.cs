
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class LegacyDeleted
    {
        public int TokenID { get; set; }
        public int Token { get; set; }
        public bool LLDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}