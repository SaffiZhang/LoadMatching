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
using StackExchange.Redis.Extensions.Core.Abstractions;
using LoadLink.LoadMatching.Infrastructure.Caching;

namespace ConsoleService
{
    public class PostingCreatedEventHandler : IIntegrationEventHandler<PostingCreatedEvent>
    {
        private IEquipmentPostingRepository _equipmentPostingRespository;
        private IMatch _equipmentLegacyLeadMatchingService;
        private IMatch _equipmentDatLeadMatchingService;
        private IMatch _equipmentPlatformLeadMatchingService;
        private ILeadCaching _leadCaching;
        private readonly IServiceProvider _service;
       

        public PostingCreatedEventHandler(IServiceProvider service)
        {
            _service = service;
            
            using (var scope = _service.CreateScope())
            {
                _leadCaching = scope.ServiceProvider.GetRequiredService<ILeadCaching>();
                _equipmentPostingRespository = scope.ServiceProvider.GetRequiredService<IEquipmentPostingRepository>();
                var matchingFactory = scope.ServiceProvider.GetRequiredService<IMatchingServiceFactory>();
                _equipmentDatLeadMatchingService = matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Dat);
                _equipmentLegacyLeadMatchingService = matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Legacy);
                _equipmentPlatformLeadMatchingService = matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Platform);
            };
        }

        public async Task Handle(PostingCreatedEvent integrationEvent)
        {
            Console.WriteLine(integrationEvent.Posting.Token);
            await CreateLeads(integrationEvent.Posting, integrationEvent.IsGlobalExclued);

        }
     
        private async Task CreateLeads(PostingBase posting, bool? isGlobleExclude)
        {
            var tasks = new List<Task>();
            tasks.Add(Task.Run(() => CreateDatLead(posting)));
            tasks.Add(Task.Run(() => CreatePlatformLead(posting, isGlobleExclude ?? false)));
            tasks.Add(Task.Run(() => CreateLegacyLead(posting)));


        }
        private async Task CreatePlatformLead(PostingBase posting, bool? isGlobleExclude)
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
                return;

            var leads = await _equipmentPlatformLeadMatchingService.Match(posting, loadList, true, isGlobleExclude, _service);
            
            if (leads == null)
                return;
            if (leads.Count() == 0)
                return;

           var leadsWithId= await _equipmentPostingRespository.BulkInsertLeadTable(leads);
            await _leadCaching.BulkInsertLeads(LeadPostingType.EquipmentLead, posting.Token, leadsWithId);
            
            if (posting.SecondaryLeads != null)
            {
                leadsWithId= await _equipmentPostingRespository.BulkInsert2ndLead(posting.SecondaryLeads);
                foreach (var s in leadsWithId)
                    await _leadCaching.InsertSingleLead(LeadPostingType.LoadLead, s.LToken, s);
            }
               
            


        }
        private async Task CreateDatLead(PostingBase posting)
        {
            var datLoadList = await _equipmentPostingRespository.GetDatPostingForMatching(posting.DateAvail,
                                                                           posting.VSize,
                                                                           posting.VType,
                                                                           posting.SrceCountry,
                                                                           posting.DestCountry,
                                                                           posting.PostMode,
                                                                           posting.NetworkId);
            if (!datLoadList.Any())
                return;

            var leads = await _equipmentDatLeadMatchingService.Match(posting, datLoadList, false, false, _service);

            if (leads == null)
                return;
            if (leads.Count() == 0)
                return;
            var leadsWithId = await _equipmentPostingRespository.BulkInsertDatLeadTable(leads);
        
            await _leadCaching.BulkInsertLeads(LeadPostingType.EquipmentLead, posting.Token, leadsWithId);


        }
        private async Task CreateLegacyLead(PostingBase posting)
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
                return;
            var leads = await _equipmentLegacyLeadMatchingService.Match(posting, legacyLoadList, false, false, _service);

            if (leads == null)
                return;
            if (leads.Count() == 0)
                return;
            var leadsWithId = await _equipmentPostingRespository.BulkInsertLeadTable(leads);
            await _leadCaching.BulkInsertLeads(LeadPostingType.EquipmentLead, posting.Token, leadsWithId);


        }

       
    }
}

