using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.IntegrationEventManager
{
    public interface IPublishIntegrationEvent<T> where T: IIntegrationEvent
    {
        void Publish(T integrationEvent, string queueName);
    }
}
