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
    class Program
    {
        static void Main(string[] args)
        {

            var tasks = new List<Task>();



            tasks.Add(Task.Run(() => r1()));
            tasks.Add(Task.Run(() => r2()));
            var t = Task.WhenAll(tasks);
            t.Wait();
            var worker = new MqWorker();
            worker.asyncTask().Wait();
            worker.Send();
            //tasks = new List<Task>();
            //tasks.Add(Task.Run(() => r1()));
            //tasks.Add(Task.Run(() => r2()));
            //t = Task.WhenAll(tasks);
            //t.Wait();

        }
        static void r1()
        {
            var r = new MqReceiver();
            r.Receive("a",1000).Wait();
        }
        static void r2()
        {
            var r1 = new MqReceiver();
            r1.Receive("b",500).Wait();
        }
    }
}
