
using System;

namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetFlagResult
    {
        public int? Id { get; set; }
        public string CustCD { get; set; }
        public string ContactCustCD { get; set; }
        public int? LToken { get; set; }
        public int? EToken { get; set; }
        public string ChatUserId { get; set; }
        public string RegisteredName { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HoursOfOperation { get; set; }
        public string Email { get; set; }
        public double? Rating { get; set; }
        public DateTime? LPostedDate { get; set; }
        public string LSrcCity { get; set; }
        public string LSrcSt { get; set; }
        public string LDestCity { get; set; }
        public string LDestSt { get; set; }
        public string LVSize { get; set; }
        public string LVType { get; set; }
        public string LPAttrib { get; set; }
        public string LComment { get; set; }
        public DateTime? PPostedDate { get; set; }
        public string PSrcCity { get; set; }
        public string PSrcSt { get; set; }
        public string PDestCity { get; set; }
        public string PDestSt { get; set; }
        public string PVSize { get; set; }
        public string PVType { get; set; }
        public string PPAttrib { get; set; }
        public string PComment { get; set; }
        public string PostType { get; set; }
        public string SubscriberType { get; set; }
    }
}
