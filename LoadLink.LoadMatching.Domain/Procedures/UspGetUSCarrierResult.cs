
using System;

namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetUSCarrierResult
    {
        public string CustCd { get; set; }
        public int Token { get; set; }
        public DateTime DateAvail { get; set; }
        public string SrceStateCity { get; set; }
        public string DestStateCity { get; set; }
        public string CompanyName { get; set; }
        public DateTime PostDate { get; set; }
        public string VehicleSize { get; set; }
        public string VehicleType { get; set; }
        public string Comment { get; set; }
        public string CustomerOf { get; set; }
        public int Equifax { get; set; }
        public int TCC { get; set; }
        public int TCUS { get; set; }
        public string Contacted { get; set; }
        public int? CreatedBy { get; set; }
        public string SubscriberType { get; set; }
    }
}
