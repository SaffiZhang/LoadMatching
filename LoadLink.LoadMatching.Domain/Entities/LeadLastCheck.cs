
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class LeadLastCheck
    {
        public string AppID { get; set; }
        public DateTime LastCheck { get; set; }
        public int EquipmentLeadID { get; set; }
        public int LoadLeadID { get; set; }
        public int DATEquipmentLeadID { get; set; }
        public int DATLoadLeadID { get; set; }
    }
}