﻿
namespace LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands
{
    public class GetUSMemberSearchCommand
    {
        public string CompanyName { get; set; }
        public string ProvSt { get; set; }
        public string Phone { get; set; }
        public MemberSearch.Models.Queries.SearchType ShowExcluded { get; set; }
        public string CustCd { get; set; }
    }
}
