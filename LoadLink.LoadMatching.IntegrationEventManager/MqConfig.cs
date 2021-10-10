using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.IntegrationEventManager
{
    public class MqConfig
    {
    
     public string HostName { get; set; } 
     public string UserName { get; set; }
     public string Password { get; set; }
     public int RetryCount { get; set; }
     public IEnumerable<QueueConfig> Queues { get; set; }
     
    }
    public class QueueConfig
    {
        public string QueueName { get; set; }
        public int MqCount { get; set; }
        public int MqNo { get; set; }

    }
    public enum LoadMatchingQue
    {
        PostingCreated,
         LeadCreated 
    }
       
}
