using System;

using MediatR;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings
{
    public class MatchingServiceFactory : IMatchingServiceFactory
    {
        private IMatchingConfig _matchingConfig;
        private IMediator _mediator;
        private IFillNotPlatformPosting _fillNotPlatformPosting;

        public MatchingServiceFactory(IMatchingConfig matchingConfig, IMediator mediator, IFillNotPlatformPosting fillNotPlatformPosting)
        {
            _matchingConfig = matchingConfig;
            _mediator = mediator;
            _fillNotPlatformPosting = fillNotPlatformPosting;
        }

        public IMatch GetService(PostingType postingType, MatchingType matchingType)
        {
            switch (postingType,matchingType)
            {
                case (PostingType.EquipmentPosting, MatchingType.Platform):
                    return new PlatformEquipmentMatching(_matchingConfig,_mediator,_fillNotPlatformPosting);
                case (PostingType.EquipmentPosting, MatchingType.Legacy):
                    return new LegacyEquipmentMatching(_matchingConfig, _mediator, _fillNotPlatformPosting);
                case (PostingType.EquipmentPosting, MatchingType.Dat):
                    return new DatEquipmentMatching(_matchingConfig, _mediator, _fillNotPlatformPosting);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
