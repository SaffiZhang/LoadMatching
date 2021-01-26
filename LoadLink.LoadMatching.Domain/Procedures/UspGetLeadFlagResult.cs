
namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetLeadFlagResult
    {
        public string CustCD { get; set; }
        public int EquipmentLead { get; set; }
        public int LoadLead { get; set; }
        public int DATEquipmentLead { get; set; }
        public int DATLoadLead { get; set; }
    }
}
