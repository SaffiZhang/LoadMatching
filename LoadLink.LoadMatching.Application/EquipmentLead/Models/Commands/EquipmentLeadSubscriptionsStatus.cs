
namespace LoadLink.LoadMatching.Application.EquipmentLead.Models.Commands
{
    public class EquipmentLeadSubscriptionsStatus
    {
        public bool HasDATSubscription { get; set; }
        public bool HasEQSubscription { get; set; }
        public bool HasQPSubscription { get; set; }
        public bool HasTCUSSubscription { get; set; }
        public bool HasTCCSubscription { get; set; }
    }
}
