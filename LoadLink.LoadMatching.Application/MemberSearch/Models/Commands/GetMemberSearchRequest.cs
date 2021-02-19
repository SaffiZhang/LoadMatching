using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Application.MemberSearch.Models.Queries
{
    public enum SearchType
    {
        All = 0, // Only Included 
        Included = 1, // All the result (Included + Excluded)
        Excluded = 2  // Only Excluded
    }
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
