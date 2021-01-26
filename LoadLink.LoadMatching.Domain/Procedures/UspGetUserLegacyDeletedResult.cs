
using System;

namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetUserLegacyDeletedResult
    {
        public int TokenID { get; set; }
        public int Token { get; set; }
        public bool LLDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
