
namespace LoadLink.LoadMatching.Application.LoadLead.Models.Commands
{
    public class LoadLeadSubscriptionsStatus
    {
        public bool HasDATSubscription { get; set; }
        public bool HasEQSubscription { get; set; }
        public bool HasQPSubscription { get; set; }
        public bool HasTCUSSubscription { get; set; }
        public bool HasTCCSubscription { get; set; }
    }
}
