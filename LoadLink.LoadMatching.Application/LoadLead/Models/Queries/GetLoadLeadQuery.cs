
using System;

namespace LoadLink.LoadMatching.Application.LoadLead.Models.Queries
{
    public class GetLoadLeadQuery
    {
        public string CustCD { get; set; }
        public DateTime DateAvail { get; set; }
        public int SrceID { get; set; }
        public string SrceCity { get; set; }
        public string SrceSt { get; set; }
        public int SrceCountry { get; set; }
        public double SrceLong { get; set; }
        public double SrceLat { get; set; }
        public int DestID { get; set; }
        public string DestCity { get; set; }
        public string DestSt { get; set; }
        public int DestCountry { get; set; }
        public double DestLong { get; set; }
        public double DestLat { get; set; }
        public string VehicleSize { get; set; }
        public string VehicleType { get; set; }
        public string Comment { get; set; }
        public string PostMode { get; set; }
        public string ClientRefNum { get; set; }
        public string ProductName { get; set; }
        public string PostingAttrib { get; set; }
        public decimal? Distance { get; set; }
        public int LToken { get; set; }
        public int EToken { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int QPStatus { get; set; }
        public int Equifax { get; set; }
        public int TCC { get; set; }
        public int TCUS { get; set; }
        public string DirO { get; set; }
        public decimal? DFO { get; set; }
        public decimal? DFD { get; set; }
        public string Contacted { get; set; }
        public int? Flag { get; set; }
        public string SubscriberType { get; set; }
        public string PType { get; set; }
        public bool? CustomerTracking { get; set; }
    }
}

