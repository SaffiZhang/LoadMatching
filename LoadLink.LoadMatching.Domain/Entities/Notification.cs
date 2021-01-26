
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string CustCd { get; set; }
        public int PostingId { get; set; }
        public int Leads { get; set; }
        public DateTime? LastPushDate { get; set; }
    }
}