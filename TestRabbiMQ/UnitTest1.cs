using System.Text.Json;
using System.Text;
using Xunit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;

namespace TestRabbiMQ
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            send();
            receive();
            
        }
        private void send()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                
            }

        }
        private void receive()
        {
            var m = "";
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    m = message;
                };
                var a= channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                
            }
        }
        private void test()
        {
          
        }
    }
}
