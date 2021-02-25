
namespace LoadLink.LoadMatching.Application.NetworkMembers.Models.Queries
{
    public class CreateNetworkMemberQuery
    {
        public int Id { get; set; }
        public int NetworkId { get; set; }
        public string MemberCustCD { get; set; }
        public string RegisteredName { get; set; }
        public string CommonName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyLocation { get; set; }
        public string ContactPhone { get; set; }
        public string PrimaryContactName { get; set; }
    }
}
