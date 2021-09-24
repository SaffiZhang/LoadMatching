using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using System.Linq;


namespace LoadLink.LoadMatching.Application.EquipmentPosting.Commands
{
    public class CreateEquipmentPostingCommandHandler : IRequestHandler<CreateEquipmentPostingCommand, IEnumerable<LeadBase>>
    {
        private readonly IEquipmentPostingRepository _equipmentPostingRespository;
        private readonly IMatch _equipmentLegacyLeadMatchingService;
        private readonly IMatch _equipmentDatLeadMatchingService;
        private readonly IMatch _equipmentPlatformLeadMatchingService;

        public CreateEquipmentPostingCommandHandler(IEquipmentPostingRepository equipmentPostingRespository, IMatchingServiceFactory matchingServiceFactory)
        {
            _equipmentPostingRespository = equipmentPostingRespository;
            _equipmentPlatformLeadMatchingService = matchingServiceFactory.GetService(PostingType.EquipmentPosting,MatchingType.Platform);
            _equipmentLegacyLeadMatchingService = matchingServiceFactory.GetService(PostingType.EquipmentPosting, MatchingType.Legacy);
            _equipmentDatLeadMatchingService = matchingServiceFactory.GetService(PostingType.EquipmentPosting, MatchingType.Dat);
        }

        public async Task<IEnumerable<LeadBase>> Handle(CreateEquipmentPostingCommand request, CancellationToken cancellationToken)
        {
            var vSize = CommonLM.EquipmentVSizeStringToNum(request.VehicleSize);
            if (vSize == 0)
                return null;
            var vType = CommonLM.VTypeStringToNum(request.VehicleType);
            if (vType == 0)
                return null;
            var pAttrib = CommonLM.PostingAttributeStringToNum(request.PostingAttrib);
            if (pAttrib == 0)
                return null;
            var isGlobalExcluded = request.GlobalExcluded == null ? false : request.GlobalExcluded.Value;

            var posting = new PostingBase(
                                          request.CustCD,
                                          request.DateAvail,

                                          request.SrceCity,
                                          request.SrceSt,
                                          request.SrceRadius,
                                          
                                          request.DestCity,
                                          request.DestSt,
                                          request.DestRadius,
                                          
                                          vSize,
                                          vType,
                                          request.VehicleSize,
                                          request.VehicleType,
                                          
                                          pAttrib,
                                          request.PostingAttrib,
                                          
                                          request.Comment,
                                          request.PostMode,
                                          request.ClientRefNum,
                                          request.ProductName,
                                        
                                          request.NetworkId,
                                          request.Corridor,
                                       
                                          request.CustomerTracking,
                                          request.CreatedBy);

            var resultFromDB = await _equipmentPostingRespository.SavePosting(posting);
            posting.UpdateDistanceAndPointId(resultFromDB);

            var tasks = new List<Task<IEnumerable<LeadBase>>>();
            tasks.Add(Task.Run(() => CreateDatLead(posting)));
            tasks.Add(Task.Run(() => CreatePlatformLead(posting, request.GlobalExcluded??false)));
            tasks.Add(Task.Run(() => CreateLegacyLead(posting)));

             await Task.WhenAll(tasks);
           
            
            var leads = new List<LeadBase>();
            foreach (var task in tasks)
                leads.AddRange(task.Result);


            return leads;
            
        }
        private async Task<IEnumerable<LeadBase>> CreatePlatformLead(PostingBase posting, bool? isGlobleExclude)
        {
            var loadList = await _equipmentPostingRespository.GetPlatformPostingForMatching (
                                                                           posting.CustCD,
                                                                           posting.DateAvail,
                                                                           posting.VSize,
                                                                           posting.VType,
                                                                           posting.SrceCountry,
                                                                           posting.DestCountry,
                                                                           posting.PostMode,
                                                                           posting.NetworkId);
            if (loadList.Count() == 0)
                return new List<LeadBase>();

            var leads= await _equipmentPlatformLeadMatchingService.Match(posting, loadList,true, isGlobleExclude);
            await _equipmentPostingRespository.UpdatePostingForPlatformLeadCompleted(posting.Token, leads.Count());
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
            if (datLoadList.Count() == 0)
                return new List<LeadBase>();

            var leads= await _equipmentDatLeadMatchingService.Match(posting, datLoadList,false);
            await _equipmentPostingRespository.UpdatePostingForDatLeadCompleted(posting.Token, leads.Count());
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
            if (legacyLoadList.Count() == 0)
                return new List<LeadBase>();
            var leads= await _equipmentLegacyLeadMatchingService.Match(posting, legacyLoadList,false);
            await _equipmentPostingRespository.UpdatePostingForLegacyLeadCompleted(posting.Token, leads.Count());
            return leads;
            
        }
    }
}
