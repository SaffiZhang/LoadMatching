
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class Mileage
    {
        public int SrceID { get; set; }
        public int DestID { get; set; }
        public decimal? GoogleMiles { get; set; }
        public decimal? PCMilerMiles { get; set; }
        public int? CalcStatus { get; set; }
        public int AccessCount { get; set; }
        public DateTime? CalculatedOn { get; set; }
    }
}