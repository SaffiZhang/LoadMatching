
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class Flag
    {
        public int Id { get; set; }
        public string CustCd { get; set; }
        public string ContactCustCd { get; set; }
        public int LToken { get; set; }
        public int EToken { get; set; }
        public string LSrcCity { get; set; }
        public string LSrcSt { get; set; }
        public string LDestCity { get; set; }
        public string LDestSt { get; set; }
        public string LVType { get; set; }
        public string LVSize { get; set; }
        public DateTime LPostedDate { get; set; }
        public string LPAttrib { get; set; }
        public string LComment { get; set; }
        public string PSrcCity { get; set; }
        public string PSrcSt { get; set; }
        public string PDestCity { get; set; }
        public string PDestSt { get; set; }
        public string PVType { get; set; }
        public string PVSize { get; set; }
        public DateTime PPostedDate { get; set; }
        public string PPAttrib { get; set; }
        public string PComment { get; set; }
        public string PostType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
    }
}