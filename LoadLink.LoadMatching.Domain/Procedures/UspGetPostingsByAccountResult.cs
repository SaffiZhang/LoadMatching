
using System;

namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetPostingsByAccountResult
    {
        public string PStatus { get; set; }
        public DateTime DateAvail { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string VehicleSize { get; set; }
        public string VehicleType { get; set; }
        public string Corridor { get; set; }
        public string PostingAttrib { get; set; }
        public int? InitialLeadsCount { get; set; }
        public string ProductName { get; set; }
        public string PostedBy { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public string PType { get; set; }
    }
}
