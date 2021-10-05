using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.IntegrationEventManager
{
    public class MqConfig
    {
     public string QueueName { get; set; }
     public string HostName { get; set; } 
     public int MqCount { get; set; }
     public int MqNo { get; set; }
    }
    public enum LoadMatchingQue
    {
        PostingCreated,
         LeadCreated 
    }
       
}
