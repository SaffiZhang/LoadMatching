


namespace LoadLink.LoadMatching.Domain.Entities
{
    public class AccountInMem
    {
        public string CustCD { get; set; }
        public string CommonName { get; set; }
        public int AccountId { get; set; }
        public string ProductType { get; set; }
        public string MailingProvinceCode { get; set; }
        public string Phone { get; set; }
        public string SubscriberType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailingCity { get; set; }
        public string Email { get; set; }
        public short? StatusId { get; set; }
        public bool? GlobalExcluded { get; set; }
        public bool? DATExcluded { get; set; }
    }
}