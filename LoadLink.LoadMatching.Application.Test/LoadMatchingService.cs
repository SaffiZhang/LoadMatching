//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Hosting;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text.Json;
//using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
//using LoadLink.LoadMatching.Application.EquipmentPosting.Models;
//using System.Linq;
//using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;

//namespace LoadLink.LoadMatching.Application.Test
//{
//    public class LoadMatchingService 
//    {
//        private readonly IEquipmentPostingRepository _equipmentPostingRespository;
//        private readonly IMatch _equipmentLegacyLeadMatchingService;
//        private readonly IMatch _equipmentDatLeadMatchingService;
//        private readonly IMatch _equipmentPlatformLeadMatchingService;
//        private readonly string queueName = "matchingQue";

//        public LoadMatchingService(IEquipmentPostingRepository equipmentPostingRespository, IMatchingServiceFactory matchingServiceFactory)
//        {
//            _equipmentPostingRespository = equipmentPostingRespository;
//            _equipmentPlatformLeadMatchingService = matchingServiceFactory.GetService(PostingType.EquipmentPosting, MatchingType.Platform);
//            _equipmentLegacyLeadMatchingService = matchingServiceFactory.GetService(PostingType.EquipmentPosting, MatchingType.Legacy);
//            _equipmentDatLeadMatchingService = matchingServiceFactory.GetService(PostingType.EquipmentPosting, MatchingType.Dat);
//        }

//        public async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            var factory = new ConnectionFactory()
//            {
//                HostName = "localhost"
//                ,
//                DispatchConsumersAsync = true
//            };
//            using (var connection = factory.CreateConnection())
//            {
//                using (var channel = connection.CreateModel())
//                {

//                    var c = channel.QueueDeclare(queue: queueName,
//                                     durable: true,
//                                     exclusive: false,
//                                     autoDelete: false,
//                                     arguments: null);
//                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                   
//                    var consumer = new AsyncEventingBasicConsumer(channel);
//                    channel.BasicConsume(queue: queueName,
//                                        autoAck: true,
//                                        consumer: consumer);
//                    consumer.Received +=
//                        async (model, ea) =>
//                        {
//                            var body = ea.Body.ToArray();
//                            var message = Encoding.UTF8.GetString(body);
//                            var para = JsonSerializer.Deserialize<MatchingPara>(message);

//                            await CreateLeads(para.Posting, para.IsGlobalExcluded);

//                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);



//                        };
//                }
//            }
//        }
//        private async Task run()
//        {
//            await Task.Delay(10);
//        }
//        private async Task CreateLeads(PostingBase posting, bool? isGlobleExclude)
//        {
//            var tasks = new List<Task<IEnumerable<LeadBase>>>();
//            tasks.Add(Task.Run(() => CreateDatLead(posting)));
//            tasks.Add(Task.Run(() => CreatePlatformLead(posting, isGlobleExclude ?? false)));
//            tasks.Add(Task.Run(() => CreateLegacyLead(posting)));

//            await Task.WhenAll(tasks);


//            var leads = new List<LeadBase>();
//            foreach (var task in tasks)
//                leads.AddRange(task.Result);

//            await _equipmentPostingRespository.BulkInsertLead(leads);


//        }
//        private async Task<IEnumerable<LeadBase>> CreatePlatformLead(PostingBase posting, bool? isGlobleExclude)
//        {
//            var loadList = await _equipmentPostingRespository.GetPlatformPostingForMatching(
//                                                                           posting.CustCD,
//                                                                           posting.DateAvail,
//                                                                           posting.VSize,
//                                                                           posting.VType,
//                                                                           posting.SrceCountry,
//                                                                           posting.DestCountry,
//                                                                           posting.PostMode,
//                                                                           posting.NetworkId);
//            if (!loadList.Any())
//                return new List<LeadBase>();

//            var leads = await _equipmentPlatformLeadMatchingService.Match(posting, loadList, true, isGlobleExclude);
//            //await _equipmentPostingRespository.UpdatePostingForPlatformLeadCompleted(posting.Token, leads.Count());
//            return leads;


//        }
//        private async Task<IEnumerable<LeadBase>> CreateDatLead(PostingBase posting)
//        {
//            var datLoadList = await _equipmentPostingRespository.GetDatPostingForMatching(posting.DateAvail,
//                                                                           posting.VSize,
//                                                                           posting.VType,
//                                                                           posting.SrceCountry,
//                                                                           posting.DestCountry,
//                                                                           posting.PostMode,
//                                                                           posting.NetworkId);
//            if (!datLoadList.Any())
//                return new List<LeadBase>();

//            var leads = await _equipmentDatLeadMatchingService.Match(posting, datLoadList, false);
//            //await _equipmentPostingRespository.UpdatePostingForDatLeadCompleted(posting.Token, leads.Count());
//            return leads;

//        }
//        private async Task<IEnumerable<LeadBase>> CreateLegacyLead(PostingBase posting)
//        {
//            var legacyLoadList = await _equipmentPostingRespository.GetLegacyPostingForMatching(posting.CustCD,
//                                                                           posting.DateAvail,
//                                                                           posting.VSize,
//                                                                           posting.VType,
//                                                                           posting.SrceCountry,
//                                                                           posting.DestCountry,
//                                                                           posting.PostMode,
//                                                                           posting.NetworkId);
//            if (!legacyLoadList.Any())
//                return new List<LeadBase>();
//            var leads = await _equipmentLegacyLeadMatchingService.Match(posting, legacyLoadList, false);
//            //await _equipmentPostingRespository.UpdatePostingForLegacyLeadCompleted(posting.Token, leads.Count());
//            return leads;

//        }
//    }
//    }
