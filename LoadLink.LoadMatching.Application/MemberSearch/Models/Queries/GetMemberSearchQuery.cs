using static LoadLink.LoadMatching.Application.Common.CommonLM;

namespace LoadLink.LoadMatching.Application.MemberSearch.Models.Queries
{
 
    public class GetMemberSearchQuery    
    {
        public string CompanyName { get; set; }
        public string ProvSt { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string MemberType { get; set; }
        public int GetLinkUS { get; set; }
        public int ShowExcluded { get; set; }
        public string CustCd { get; set; }
    }
}
