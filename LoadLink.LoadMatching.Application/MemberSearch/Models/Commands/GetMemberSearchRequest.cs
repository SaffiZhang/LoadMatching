using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Application.MemberSearch.Models.Queries
{
    public class GetMemberSearchRequest
    {
        public string CompanyName { get; set; }
        public string ProvSt { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string MemberType { get; set; }
        public string GetLinkUS { get; set; }
        public SearchType ShowExcluded { get; set; }
        public string CustCd { get; set; }
    }
}
