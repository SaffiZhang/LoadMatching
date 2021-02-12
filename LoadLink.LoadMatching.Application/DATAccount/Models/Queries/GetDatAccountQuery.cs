using System;

namespace LoadLink.LoadMatching.Application.DATAccount.Models.Queries
{
    public class GetDatAccountQuery
    {
        public string AccountId { get; set; }
        public string CustCD { get; set; }
        public string CommonName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string CITY { get; set; }
        public string ProvinceCode { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Website { get; set; }
        public string HoursOfOperation { get; set; }
        public string SubscriberType { get; set; }
    }
}
