
namespace LoadLink.LoadMatching.Application.Member.Models.Queries
{
    public class GetMemberQuery
    {
        public int MemberId { get; set; }
        public string CustCd { get; set; }
        public string MemberCustCd { get; set; }
        public string DispatchNote { get; set; }
    }
}
