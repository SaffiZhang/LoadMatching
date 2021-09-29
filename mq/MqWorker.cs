using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;
namespace mq
{
    public class MqWorker
    {
        private readonly string queueName = "matchingQue";
        public void Send()
        {
            int i = 0;
            var posting = FakePosting.NotPlatformPosting();
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable:true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(new MatchingPara(posting, false));
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
                
                Console.WriteLine(" [x] Sent {0}", posting.CustCD);
            }

            Console.WriteLine(" Press [enter] to exit.");

        }
        
        public async Task asyncTask()
        {
            Console.WriteLine(" I am async;");
        }
        
       
    }
}
