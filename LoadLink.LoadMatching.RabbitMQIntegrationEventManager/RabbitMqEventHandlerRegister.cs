using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using LoadLink.LoadMatching.IntegrationEventManager;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace LoadLink.LoadMatching.RabbitMQIntegrationEventManager
{
    public class RabbitMqEventHandlerRegister<T> : IIntegationEventHandlerRegister<T> 
        where T:IIntegrationEvent 
    {
        private MqConfig _mqConfig;
        private IConnection _connection;

        public RabbitMqEventHandlerRegister(MqConfig mqConfig)
        {
            _mqConfig = mqConfig;
        }

        public void Register(IIntegrationEventHandler<T> handler, string queueName, bool? isConsoleWaiting = null)
        {
            var queueConfig = GetConfig(queueName);
            if (queueConfig == null)
                throw new ArgumentNullException("Configuration error, MqConfig miss queue:" + queueName);

            ListenToQueue(handler, queueConfig,  isConsoleWaiting);
        }
        private void ListenToQueue(IIntegrationEventHandler<T> handler,  QueueConfig queueConfig, bool? isConsoleWaiting = null)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _mqConfig.HostName??"LocalHost",
                DispatchConsumersAsync = true,
                UserName=_mqConfig.UserName??"guest",
                Password=_mqConfig.Password??"guest"
            };
            var queueName = queueConfig.QueueName + queueConfig.MqNo;
            _connection = factory.CreateConnection();
            using (_connection)
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.Received +=
                    async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                       
                        var message = Encoding.UTF8.GetString(body);
                     
                        await handler.Handle(JsonSerializer.Deserialize<T>(message));

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                    };
                channel.BasicConsume(queue: queueName,
                                     autoAck: false,
                                     consumer: consumer);
                if (isConsoleWaiting == null)
                    return;
                if (isConsoleWaiting.Value)
                {
                    string input = "";
                    do
                    {
                        input = Console.ReadLine();
                    } while (input != "stop");


                }
               

            }
        }
       
        

        private QueueConfig GetConfig(string queueName)
        {
            return _mqConfig.Queues.Where(q => q.QueueName == queueName).FirstOrDefault();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}
