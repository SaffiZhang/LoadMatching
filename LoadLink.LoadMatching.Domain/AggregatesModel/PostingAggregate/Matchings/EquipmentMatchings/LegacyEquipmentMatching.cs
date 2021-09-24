
using MediatR;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings
{
    public class LegacyEquipmentMatching : MatchingService
    {
        public LegacyEquipmentMatching(IMatchingConfig matchingConfig, IMediator mediator, IFillNotPlatformPosting fillNotPlatformPosting) : base(matchingConfig, mediator, fillNotPlatformPosting)
        {
        }

        protected override LeadBase CreateLead(PostingBase posting, PostingBase matchedPosting, string dirO, bool? isGlobalExcluded)
        {
            return new LegacyEquipmentLead(posting, matchedPosting,dirO);
        }
    }
}
