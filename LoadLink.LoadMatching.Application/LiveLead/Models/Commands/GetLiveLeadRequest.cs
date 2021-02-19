using System;
using System.ComponentModel.DataAnnotations;

namespace LoadLink.LoadMatching.Application.LiveLead.Models.Commands
{
    public class GetLiveLeadRequest
    {
        public GetLiveLeadRequest()
        {
            Broker = new Broker();
            Carrier = new Carrier();
        }

        [Required]
        public int Type { get; set; }
        [Required]
        public DateTime LeadFrom { get; set; }
        public Broker Broker { get; set; }
        public Carrier Carrier { get; set; }
    }
    public class Broker
    {
        public string B_LLAPIKey { get; set; }
        public string B_QPAPIKey { get; set; }
        public string B_EQFAPIKey { get; set; }
        public string B_TCUSAPIKey { get; set; }
        public string B_TCCAPIKey { get; set; }
        public string B_DATAPIKey { get; set; }
    }

    public class Carrier
    {
        public string C_LLAPIKey { get; set; }
        public string C_QPAPIKey { get; set; }
        public string C_EQFAPIKey { get; set; }
        public string C_TCUSAPIKey { get; set; }
        public string C_TCCAPIKey { get; set; }
        public string C_DATAPIKey { get; set; }
    }
}
