using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.IntegrationEventManager
{
    public interface IIntegrationEventHandler<T> where T: IIntegrationEvent
    {
        Task Handle(IIntegrationEvent integrationEvent);
    }
}
