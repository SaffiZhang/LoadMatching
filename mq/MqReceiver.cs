using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
namespace mq
{
    public class MqReceiver
    {
        private readonly string queueName = "matchingQue";
        public  async Task Receive(string name,int sleep)
        {
             
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
                ,
                  DispatchConsumersAsync = true
              };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    var c = channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    Console.WriteLine("size:" + c.MessageCount);
                    var consumer = new AsyncEventingBasicConsumer(channel);
                    channel.BasicConsume(queue: queueName,
                                        autoAck: true,
                                        consumer: consumer);
                    consumer.Received += 
                        async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        await run();

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                        Console.WriteLine(name + ": Received {0}", message);

                    };

                    
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
           
        }
        public async Task run()
        {
            var bulk = new LeadBulkInsertTest();


            await bulk.BulkInsertLeadTest("test");
        }
    }
}
