
using MediatR;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment.Matchings
{
    public class PlatformEquipmentMatching : MatchingService
    {
        private IEquipmentPostingRepository _equipmentPostingRepository;
        public PlatformEquipmentMatching(IMatchingConfig matchingConfig, IMediator mediator, IFillNotPlatformPosting fillNotPlatformPosting, IEquipmentPostingRepository equipmentPostingRepository) : base(matchingConfig, mediator, fillNotPlatformPosting)
        {
            _equipmentPostingRepository = equipmentPostingRepository;
        }

        protected override async Task<LeadBase> CreateLead(PostingBase posting, PostingBase matchedPosting,  string dirO, bool? isGlobalExcluded)
        {
            var lead =new PlatformEquipmentLead(posting, matchedPosting, dirO, isGlobalExcluded);
            
            return lead;
          
            
       }
    }
}
