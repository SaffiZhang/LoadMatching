using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text.Json;
using System.Text;
using LoadLink.LoadMatching.IntegrationEventManager;
using Polly;
using Polly.Retry;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace LoadLink.LoadMatching.RabbitMQIntegrationEventManager
{
    public class RabbitMqEventPublisher<T> : IPublishIntegrationEvent<T> where T:IIntegrationEvent
    {
     
       private MqConfig _mqConfig;
        private IRabbitMQPersistentConnection _persistentConnection;
        private int _retryCount;
      

        public RabbitMqEventPublisher(MqConfig mqConfig, IRabbitMQPersistentConnection persistentConnection)
        {
            _mqConfig = mqConfig;
            _persistentConnection = persistentConnection;
           
        }

        public void Publish(T integrationEvent, string queueName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    
                });

            var queueConfig = GetConfig(queueName);
            if (queueConfig == null)
                throw new ArgumentNullException("Configuration error, MqConfig miss queue " + queueName);


            var rand = new Random();
            var queueNo = queueName+ rand.Next(queueConfig.MqCount).ToString();
            
            
            using (var channel = _persistentConnection.CreateModel())
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
