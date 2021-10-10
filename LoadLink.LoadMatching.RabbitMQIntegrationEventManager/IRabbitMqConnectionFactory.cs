using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LoadLink.LoadMatching.RabbitMQIntegrationEventManager
{
    public interface IRabbitMqConnectionFactory
    {
        IConnectionFactory GetConnectionFactory();
    }
}
