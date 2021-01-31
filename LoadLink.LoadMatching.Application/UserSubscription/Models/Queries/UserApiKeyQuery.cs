using System.Collections.Generic;

namespace LoadLink.LoadMatching.Application.UserSubscription.Models.Queries
{
    public class UserApiKeyQuery
    {
        public int UserId { get; set; }
        public List<string> ApiKeys { get; set; }
    }
}
