
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class Contacted
    {
        public string CustCd { get; set; }
        public string CnCustCd { get; set; }
        public int LToken { get; set; }
        public int EToken { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
    }
}