using System;

namespace LoadLink.LoadMatching.Application.LiveLead.Models.Queries
{
    public class GetLiveLeadQuery
    {
        public string CustCD { get; set; }
        public string MileageProvider { get; set; }
        public DateTime LeadFrom { get; set; }
        public int LeadType { get; set; }
        public int GetBDAT { get; set; }
        public int GetCDAT { get; set; }
    }
}
