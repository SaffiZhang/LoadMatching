
using MediatR;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment.Matchings
{
    public class LegacyEquipmentMatching : MatchingService
    {
        public LegacyEquipmentMatching(IMatchingConfig matchingConfig, IMediator mediator, IFillNotPlatformPosting fillNotPlatformPosting) : base(matchingConfig, mediator, fillNotPlatformPosting)
        {
        }

        protected override async Task<LeadBase> CreateLead(PostingBase posting, PostingBase matchedPosting, string dirO, bool? isGlobalExcluded)
        {
            return new LegacyEquipmentLead(posting, matchedPosting,dirO);
        }
    }
}
