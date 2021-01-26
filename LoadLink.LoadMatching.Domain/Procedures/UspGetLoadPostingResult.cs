
using System;

namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetLoadPostingResult
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
        public int? SrceMarketAreaID { get; set; }
        public int DestID { get; set; }
        public string DestCity { get; set; }
        public string DestSt { get; set; }
        public int DestCountry { get; set; }
        public double DestLong { get; set; }
        public double DestLat { get; set; }
        public int DestRadius { get; set; }
        public int? DestMarketAreaID { get; set; }
        public string VehicleSize { get; set; }
        public string VehicleType { get; set; }
        public string Comment { get; set; }
        public string PostMode { get; set; }
        public string ClientRefNum { get; set; }
        public string ProductName { get; set; }
        public string PostingAttrib { get; set; }
        public decimal? Distance { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int VType { get; set; }
        public string PStatus { get; set; }
        public int NetworkID { get; set; }
        public int? RIType { get; set; }
        public long VSize { get; set; }
        public long PAttrib { get; set; }
        public int OriginalToken { get; set; }
        public int? LeadsCount { get; set; }
        public bool BIN { get; set; }
    }
}
