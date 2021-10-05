
using System;
using RabbitMQ.Client;

namespace LoadLink.LoadMatching.RabbitMQIntegrationEventManager
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
        void CloseConnection();
    }
}
