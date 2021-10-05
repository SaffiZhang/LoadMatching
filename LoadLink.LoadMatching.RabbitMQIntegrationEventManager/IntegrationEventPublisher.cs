﻿using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using LoadLink.LoadMatching.IntegrationEventManager;

namespace LoadLink.LoadMatching.RabbitMQIntegrationEventManager
{
    public class IntegrationEventPublisher : IPublishIntegrationEvent
    {
     
        IEnumerable<MqConfig> _mqConfigs;

        public IntegrationEventPublisher(IEnumerable<MqConfig> mqConfigs)
        {
            _mqConfigs = mqConfigs;
        }

        public void Publish(IIntegrationEvent integrationEvent, string queueName)
        {
            var mqConfig = GetConfig(queueName);
            if (mqConfig == null)
                throw new ArgumentNullException("Configuration error, MqConfig miss " + queueName);


            var rand = new Random();
            var queueNo = queueName+ rand.Next(mqConfig.MqCount).ToString();
            var factory = new ConnectionFactory() { HostName = mqConfig.HostName, DispatchConsumersAsync = true };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueNo,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(integrationEvent);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: properties,
                                     body: body);

            }
        }

        

        private MqConfig GetConfig(string queueName)
        {
            return _mqConfigs.Where(m => m.QueueName == queueName).FirstOrDefault();
        }
    }
}
