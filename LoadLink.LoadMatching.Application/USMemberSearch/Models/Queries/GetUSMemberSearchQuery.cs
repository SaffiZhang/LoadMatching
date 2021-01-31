
using System;

namespace LoadLink.LoadMatching.Application.USMemberSearch.Models.Queries
{
    public class GetUSMemberSearchQuery
    {
        public string CustCd { get; set; }
        public int? AccountId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string SType { get; set; }
        public int Excluded { get; set; }
        public string DispatchNote { get; set; }
        public int Equifax { get; set; }
        public int TCC { get; set; }
        public int TCUS { get; set; }
        public int StatusId { get; set; }
        public DateTime? LastUsed { get; set; }
    }
}

