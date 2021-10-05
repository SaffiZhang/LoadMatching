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
    public class IntegrationHandlerRegister<T,TH> : IIntegationEventHandlerRegister<T> 
        where T:IIntegrationEvent where TH:IIntegrationEventHandler<T>
    {
        private IEnumerable<MqConfig> _mqConfigs;
        private IConnection _connection;

        public IntegrationHandlerRegister(IEnumerable<MqConfig> mqConfigs)
        {
            _mqConfigs = mqConfigs;
        }

        public void Register(IIntegrationEventHandler<T> handler, string queueName, bool? isConsoleWaiting = null)
        {
            var mqConfig = GetConfig(queueName);
            if (mqConfig == null)
                throw new ArgumentNullException("Configuration error, MqConfig miss " + queueName);

            ListenToQueue(handler, mqConfig, isConsoleWaiting);
        }
        private void ListenToQueue(IIntegrationEventHandler<T> handler, MqConfig mqConfig, bool? isConsoleWaiting = null)
        {
            var factory = new ConnectionFactory()
            {
                HostName = mqConfig.HostName,
                DispatchConsumersAsync = true
            };
            var queueName = mqConfig.QueueName + mqConfig.MqNo;
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
                        var @event = JsonSerializer.Deserialize<T>(message);
                        await handler.Handle(@event);

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

        

        private MqConfig GetConfig(string queueName)
        {
            return _mqConfigs.Where(m => m.QueueName == queueName).FirstOrDefault();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}
