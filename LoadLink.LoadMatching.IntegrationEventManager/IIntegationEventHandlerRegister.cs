using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.IntegrationEventManager
{
    public interface IIntegationEventHandlerRegister<T> where T:IIntegrationEvent 
    {
        void Register(IIntegrationEventHandler<T> handler,string queueName, bool? isConsoleWaiting=null);
        void CloseConnection();
    }
}
