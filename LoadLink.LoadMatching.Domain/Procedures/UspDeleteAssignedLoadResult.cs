
namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspDeleteAssignedLoadResult
    {
        public string CustCd { get; set; }
        public int? EquipCreatorUserId { get; set; }
        public int? EToken { get; set; }
        public string LoadCustCd { get; set; }
        public int? LoadCreatorUserId { get; set; }
        public int Token { get; set; }
        public int? DriverID { get; set; }
    }
}
