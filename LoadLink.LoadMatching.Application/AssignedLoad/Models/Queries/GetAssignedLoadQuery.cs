using System;

namespace LoadLink.LoadMatching.Application.AssignedLoad.Models.Queries
{
    public class GetAssignedLoadQuery
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
        public bool CustTracking { get; set; }
        public DateTime DateAvail { get; set; }
        public string SrceCity { get; set; }
        public string SrceSt { get; set; }
        public int SrceCountry { get; set; }
        public double SrceLong { get; set; }
        public double SrceLat { get; set; }
        public string DestCity { get; set; }
        public string DestSt { get; set; }
        public int DestCountry { get; set; }
        public double DestLong { get; set; }
        public double DestLat { get; set; }
        public string VehicleSize { get; set; }
        public string VehicleSizeDetail { get; set; }
        public string VehicleType { get; set; }
        public string VehicleTypeDetail { get; set; }
        public string Comment { get; set; }
        public string PostMode { get; set; }
        public string ClientRefNum { get; set; }
        public string PostingAttrib { get; set; }
        public decimal? Distance { get; set; }
        public string PStatus { get; set; }
        public string Company { get; set; }
        public string LoadCustCd { get; set; }
        public int? LoadCreatorUserId { get; set; }
        public int? EquipCreatorUserId { get; set; }
    }
}
