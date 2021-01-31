using System.Collections.Generic;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.UserSubscriptionAggregate
{
    public class UserApiKey
    {
        public int UserId { get; set; }
        public List<string> ApiKeys { get; set; }

        public UserApiKey()
        {

        }
    }
}
