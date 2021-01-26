
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class LoadPostingBin
    {
        public int Token { get; set; }
        public decimal Rate { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string PickupAddress1 { get; set; }
        public string PickupAddress2 { get; set; }
        public string PickupCity { get; set; }
        public string PickupSt { get; set; }
        public string PickupZipCode { get; set; }
        public int PickupCountry { get; set; }
        public DateTime DropoffDateTime { get; set; }
        public string DropoffAddress1 { get; set; }
        public string DropoffAddress2 { get; set; }
        public string DropoffCity { get; set; }
        public string DropoffSt { get; set; }
        public string DropoffZipCode { get; set; }
        public int DropoffCountry { get; set; }
    }
}