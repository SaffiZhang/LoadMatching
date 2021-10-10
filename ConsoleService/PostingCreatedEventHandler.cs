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
        private readonly IEquipmentPostingRepository _equipmentPostingRespository;
        private readonly IMatchingServiceFactory _matchingFactory;
        private readonly ILeadCaching _leadCaching;
       

        public PostingCreatedEventHandler(ILeadCaching leadCaching, IEquipmentPostingRepository equipmentPostingRepository, IMatchingServiceFactory matchingServiceFactory )
        {
            _leadCaching = leadCaching;
            _equipmentPostingRespository = equipmentPostingRepository;
            _matchingFactory = matchingServiceFactory;
        }

        public async Task Handle(PostingCreatedEvent integrationEvent)
        {
            Console.WriteLine(integrationEvent.Posting.Token);
            await CreateLeads(integrationEvent.Posting, integrationEvent.IsGlobalExclued);

        }
     
        private async Task CreateLeads(PostingBase posting, bool? isGlobleExclude)
        {
            await Task.Factory.StartNew(() => CreateDatLead(posting));
            await Task.Factory.StartNew(() => CreatePlatformLead(posting, isGlobleExclude ?? false));
            await Task.Factory.StartNew(() => CreateLegacyLead(posting));

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

            var equipmentPlatformLeadMatchingService = _matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Platform);
            var leads = await equipmentPlatformLeadMatchingService.Match(posting, loadList, true, isGlobleExclude);
            
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

            var equipmentDatLeadMatchingService= _matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Dat);
            var leads = await equipmentDatLeadMatchingService.Match(posting, datLoadList, false, false);

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

            var equipmentLegacyLeadMatchingService= _matchingFactory.GetService(PostingType.EquipmentPosting, MatchingType.Legacy);
            var leads = await equipmentLegacyLeadMatchingService.Match(posting, legacyLoadList, false, false);

            if (leads == null)
                return;
            if (leads.Count() == 0)
                return;
            var leadsWithId = await _equipmentPostingRespository.BulkInsertLeadTable(leads);
            await _leadCaching.BulkInsertLeads(LeadPostingType.EquipmentLead, posting.Token, leadsWithId);

        }
   
    }
}

