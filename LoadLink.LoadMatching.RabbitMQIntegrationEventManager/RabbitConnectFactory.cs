using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using LoadLink.LoadMatching.IntegrationEventManager;

namespace LoadLink.LoadMatching.RabbitMQIntegrationEventManager
{
    public class RabbitConnectFactory:IRabbitMqConnectionFactory
    {
        private MqConfig _mqConfig;

        public RabbitConnectFactory(MqConfig mqConfig)
        {
            _mqConfig = mqConfig;
        }

        public IConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory()
            {
                HostName = _mqConfig.HostName ?? "LocalHost",
                UserName = _mqConfig.UserName ?? "guest",
                Password = _mqConfig.Password ?? "guest",
                DispatchConsumersAsync = true
            };
        }
    }
}
