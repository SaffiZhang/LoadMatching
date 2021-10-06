using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;
using System.Linq;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using Microsoft.Extensions.DependencyInjection;
using LoadLink.LoadMatching.IntegrationEventManager;
using LoadLink.LoadMatching.Application.EquipmentPosting.IntetrationEvents;


namespace ConsoleService
{
    public class LoadMatchingService
    {
        private IIntegrationEventHandler<PostingCreatedEvent> _postingCreatedEventHandler;
        private IIntegationEventHandlerRegister<PostingCreatedEvent> _integationEventHandlerRegister;
        private MqConfig _mqConfig;

        public LoadMatchingService(IIntegrationEventHandler<PostingCreatedEvent> postingCreatedEventHandler, IIntegationEventHandlerRegister<PostingCreatedEvent> integationEventHandlerRegister, MqConfig mqConfig)
        {
            _postingCreatedEventHandler = postingCreatedEventHandler;
            _integationEventHandlerRegister = integationEventHandlerRegister;
            _mqConfig = mqConfig;
        }

        public void Start(CancellationToken token)
        {
            Console.WriteLine("Start:" + _mqConfig.Queues.FirstOrDefault().QueueName + _mqConfig.Queues.FirstOrDefault().MqNo);
           _integationEventHandlerRegister.Register(_postingCreatedEventHandler, LoadMatchingQue.PostingCreated.ToString(),true);
        }
       
    }
}
