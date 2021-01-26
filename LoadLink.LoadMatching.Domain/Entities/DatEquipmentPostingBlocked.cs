
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class DatEquipmentPostingBlocked
    {
        public int Token { get; set; }
        public string CustCD { get; set; }
        public DateTime DateAvail { get; set; }
        public int SrceID { get; set; }
        public string SrceCity { get; set; }
        public string SrceSt { get; set; }
        public int SrceCountry { get; set; }
        public double SrceLong { get; set; }
        public double SrceLat { get; set; }
        public int SrceRadius { get; set; }
        public int DestID { get; set; }
        public string DestCity { get; set; }
        public string DestSt { get; set; }
        public int DestCountry { get; set; }
        public double DestLong { get; set; }
        public double DestLat { get; set; }
        public int DestRadius { get; set; }
        public long VSize { get; set; }
        public int VType { get; set; }
        public string Comment { get; set; }
        public int? Distance { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
        public string PStatus { get; set; }
        public string ReferenceID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long SrceRegUS { get; set; }
        public long SrceRegCDN { get; set; }
        public long DestRegUS { get; set; }
        public long DestRegCDN { get; set; }
        public DateTime? CREATEON_Date { get; set; }
    }
}