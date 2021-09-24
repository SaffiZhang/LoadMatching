
using MediatR;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings
{
    public class DatEquipmentMatching : MatchingService
    {
        public DatEquipmentMatching(IMatchingConfig matchingConfig, IMediator mediator, IFillNotPlatformPosting fillNotPlatformPosting) : base(matchingConfig, mediator, fillNotPlatformPosting)
        {
        }

        protected override LeadBase CreateLead(PostingBase posting, PostingBase matchedPosting, string dirO, bool? isGlobalExcluded)
        {
            return new DatEquipmentLead(posting, matchedPosting, dirO);
        }
    }
}
