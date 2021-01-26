
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class EquipmentLead
    {
        public int ID { get; set; }
        public string CustCD { get; set; }
        public int EToken { get; set; }
        public int LToken { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string LeadType { get; set; }
    }
}