
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class EquipmentPostingLegacy
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
        public string PostMode { get; set; }
        public string ClientRefNum { get; set; }
        public string ProductName { get; set; }
        public int? Route { get; set; }
        public long PAttrib { get; set; }
        public int? Distance { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
        public string PStatus { get; set; }
        public int NetworkId { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string Corridor { get; set; }
        public int OriginalToken { get; set; }
        public bool ExactMatch { get; set; }
        public bool Processed { get; set; }
        public DateTime? InsertedOn { get; set; }
    }
}