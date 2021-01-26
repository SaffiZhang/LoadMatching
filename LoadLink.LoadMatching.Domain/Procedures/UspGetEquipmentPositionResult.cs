
namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetEquipmentPositionResult
    {
        public int LoadId { get; set; }
        public string CustCD { get; set; }
        public int? UserId { get; set; }
        public string LoadStatus { get; set; }
        public double? Lon { get; set; }
        public double? Lat { get; set; }
        public int? MessageId { get; set; }
        public int? EquipmentID { get; set; }
        public int? EToken { get; set; }
    }
}
