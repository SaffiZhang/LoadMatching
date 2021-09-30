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
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;

namespace LoadLink.LoadMatching.Api.BackgroundTasks
{
    public class LoadMatchingService:BackgroundService
    {
        private  IEquipmentPostingRepository _equipmentPostingRespository;
        private IMatch _equipmentLegacyLeadMatchingService;
        private  IMatch _equipmentDatLeadMatchingService;
        private  IMatch _equipmentPlatformLeadMatchingService;
        
        private readonly IServiceProvider _service;

        private IConnection _connection;
        private IModel _channel;
    
        private string _queueName;
        public LoadMatchingService(IServiceProvider service, MqConfig mqConfig)
        {
            _service = service;
            using (var scope = _service.CreateScope())
            {
                _queueName = mqConfig.MqNo.ToString();
                _equipmentPostingRespository = scope.ServiceProvider.GetRequiredService<IEquipmentPostingRepository>();
                var matchingFactory = scope.ServiceProvider.GetRequiredService<IMatchingServiceFactory>();
                _equipmentDatLeadMatchingService = matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Dat);
                _equipmentLegacyLeadMatchingService = matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Legacy);
                _equipmentPlatformLeadMatchingService = matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Platform);
            };
        }
            
        public override Task StartAsync(CancellationToken cancellationToken)
        {

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            HandlePosting();
            await Task.CompletedTask;
        }
       
        private  void HandlePosting()
        {
            
  

                    var consumer = new AsyncEventingBasicConsumer(_channel);
                    
                    consumer.Received +=
                        async (model, ea) =>
                        {
                           
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            var para = JsonSerializer.Deserialize<MatchingPara>(message);
                          
                            await CreateLeads(para.Posting, para.IsGlobalExcluded);

                            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                        };
                    _channel.BasicConsume(queue: _queueName,
                                         autoAck: false,
                                         consumer: consumer);
        }
            
        
        private async Task CreateLeads(PostingBase posting, bool? isGlobleExclude)
        {
            var tasks = new List<Task<IEnumerable<LeadBase>>>();
            tasks.Add(Task.Run(() => CreateDatLead(posting)));
            tasks.Add(Task.Run(() => CreatePlatformLead(posting, isGlobleExclude ?? false)));
            tasks.Add(Task.Run(() => CreateLegacyLead(posting)));

            await Task.WhenAll(tasks);


            var leads = new List<LeadBase>();
            foreach (var task in tasks)
                leads.AddRange(task.Result);

            //await _equipmentPostingRespository.BulkInsertLead(leads);


        }
        private async Task<IEnumerable<LeadBase>> CreatePlatformLead(PostingBase posting, bool? isGlobleExclude)
        {
            var loadList = await _equipmentPostingRespository.GetPlatformPostingForMatching(
                                                                           posting.CustCD,
                                                                           posting.DateAvail,
                                                                           posting.VSize,
                                                                           posting.VType,
                                                                           posting.SrceCountry,
                                                                           posting.DestCountry,
                                                                           posting.PostMode,
                                                                           posting.NetworkId);
            if (!loadList.Any())
                return new List<LeadBase>();

            var leads = await _equipmentPlatformLeadMatchingService.Match(posting, loadList, true, isGlobleExclude);
            //await _equipmentPostingRespository.UpdatePostingForPlatformLeadCompleted(posting.Token, leads.Count());
            return leads;


        }
        private async Task<IEnumerable<LeadBase>> CreateDatLead(PostingBase posting)
        {
            var datLoadList = await _equipmentPostingRespository.GetDatPostingForMatching(posting.DateAvail,
                                                                           posting.VSize,
                                                                           posting.VType,
                                                                           posting.SrceCountry,
                                                                           posting.DestCountry,
                                                                           posting.PostMode,
                                                                           posting.NetworkId);
            if (!datLoadList.Any())
                return new List<LeadBase>();

            var leads = await _equipmentDatLeadMatchingService.Match(posting, datLoadList, false);
            //await _equipmentPostingRespository.UpdatePostingForDatLeadCompleted(posting.Token, leads.Count());
            return leads;

        }
        private async Task<IEnumerable<LeadBase>> CreateLegacyLead(PostingBase posting)
        {
            var legacyLoadList = await _equipmentPostingRespository.GetLegacyPostingForMatching(posting.CustCD,
                                                                           posting.DateAvail,
                                                                           posting.VSize,
                                                                           posting.VType,
                                                                           posting.SrceCountry,
                                                                           posting.DestCountry,
                                                                           posting.PostMode,
                                                                           posting.NetworkId);
            if (!legacyLoadList.Any())
                return new List<LeadBase>();
            var leads = await _equipmentLegacyLeadMatchingService.Match(posting, legacyLoadList, false);
            //await _equipmentPostingRespository.UpdatePostingForLegacyLeadCompleted(posting.Token, leads.Count());
            return leads;

        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            
        }
    }
}
