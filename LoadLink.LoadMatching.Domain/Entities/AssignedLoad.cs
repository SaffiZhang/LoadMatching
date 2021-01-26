
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class AssignedLoad
    {
        public int ID { get; set; }
        public string CustCd { get; set; }
        public int? UserId { get; set; }
        public int Token { get; set; }
        public string PIN { get; set; }
        public string Instruction { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? EquipmentID { get; set; }
        public int? DriverID { get; set; }
        public int? EToken { get; set; }
        public bool? CustTracking { get; set; }
    }
}