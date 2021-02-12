
namespace LoadLink.LoadMatching.Application.Contacted.Models.Commands
{
    public class UpdateContactedCommand
    {
        public int UserId { get; set; }
        public string CnCustCd { get; set; }
        public int EToken { get; set; }
        public int LToken { get; set; }
    }
}
