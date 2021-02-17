using System;

namespace LoadLink.LoadMatching.Application.MemberSearch.Models.Queries
{
    public class GetMemberSearchResult
    {
        public int AccountId { get; set; }
        public string CustCD { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string SType { get; set; }
        public int Equifax { get; set; }
        public int TCC { get; set; }
        public int TCUS { get; set; }
        public bool Excluded { get; set; }
        public string DispatchNote { get; set; }
        public int StatusId { get; set; }
        public DateTime? LastUsed { get; set; }
    }
}
