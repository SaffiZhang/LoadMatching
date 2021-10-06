using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using LoadLink.LoadMatching.IntegrationEventManager;

namespace LoadLink.LoadMatching.RabbitMQIntegrationEventManager
{
    public class IntegrationEventPublisher<T> : IPublishIntegrationEvent<T> where T:IIntegrationEvent
    {
     
       private MqConfig _mqConfig;

        public IntegrationEventPublisher(MqConfig mqConfig)
        {
            _mqConfig = mqConfig;
        }

        public void Publish(T integrationEvent, string queueName)
        {
            var queueConfig = GetConfig(queueName);
            if (queueConfig == null)
                throw new ArgumentNullException("Configuration error, MqConfig miss queue " + queueName);


            var rand = new Random();
            var queueNo = queueName+ rand.Next(queueConfig.MqCount).ToString();
            var factory = new ConnectionFactory() { 
                HostName = _mqConfig.HostName??"LocalHost",
                UserName=_mqConfig.UserName??"guest",
                Password=_mqConfig.Password??"guest",
                DispatchConsumersAsync = true };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueNo,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

          
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize<T>(integrationEvent));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                
                channel.BasicPublish(exchange: "",
                                     routingKey: queueNo,
                                     basicProperties: properties,
                                     body: body);
               
                

            }
        }

       

        private QueueConfig GetConfig(string queueName)
        {
            return _mqConfig.Queues.Where(q => q.QueueName == queueName).FirstOrDefault();
        }
    }
}
