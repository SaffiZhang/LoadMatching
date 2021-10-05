using System;
using System.Collections.Generic;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Linq;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Geometries;
using Microsoft.Extensions.DependencyInjection;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings
{
    public abstract class MatchingService:IMatch
    {
        private IMatchingConfig _matchingConfig;
        private IMediator _mediator;
    
        private IFillNotPlatformPosting _fillNotPlatformPosting;
       
        
        protected MatchingService(IMatchingConfig matchingConfig, IMediator mediator, IFillNotPlatformPosting fillNotPlatformPosting)
        {
            _matchingConfig = matchingConfig;
            _mediator = mediator;
            _fillNotPlatformPosting = fillNotPlatformPosting;
        }
        protected abstract Task<LeadBase> CreateLead(PostingBase posting, PostingBase matchedPosting, string dirO, bool? isGlobalExcluded);
        public async Task<IEnumerable<LeadBase>> Match(PostingBase posting, 
                                                        IEnumerable<PostingBase> preMatchedPostings,
                                                        bool isMatchToPlatformPosting,
                                                        bool? isGlobalExcluded=false, IServiceProvider service=null)
        {
        
            var tasks = new List<Task>();
            var lists = GetSmallBatch(preMatchedPostings);
            foreach (var list in lists)
                tasks.Add(Task.Run(() => BatchMatch(posting, list,isMatchToPlatformPosting, isGlobalExcluded, service)));

            await Task.WhenAll(tasks);
            var liveLeads = new List<LeadBase>();

            foreach (var task in tasks)
         
                liveLeads.AddRange(((Task<IEnumerable<LeadBase>>)task).Result);

           
            return liveLeads;
           

        }
        
        private List<List<PostingBase>> GetSmallBatch(IEnumerable<PostingBase> preMatchedPostings)
        {
            var size = _matchingConfig.MatchingBatchSize;
            var list = preMatchedPostings.ToList();
            var result = new List<List<PostingBase>>();
            int listCount = 0;
            for (int i = 0; i < list.Count(); i += size)
            {
                var smallList = list.GetRange(i, ((list.Count() - listCount) >= size)?size: (list.Count() - listCount) );
                result.Add(smallList);
                listCount = listCount + size;
            }
                
            return result;
        }
        private async Task<IEnumerable<LeadBase>> BatchMatch(PostingBase posting, 
                                                            IEnumerable<PostingBase> preMatchedPostings,
                                                            bool isMatchToPlatformPosting,
                                                            bool? isGlobalExcluded=false, IServiceProvider service = null)
        {
            var pRoute = posting.GetRoute();
          
            var isToMatchCorridor = posting.Corridor == "C" ? true : false;
            var leads = new List<LeadBase>();
            var corridor = new Corridor(pRoute);
            foreach (var mp in preMatchedPostings)
            {
                var mRoute = mp.GetRoute();
                if (IsMatched(pRoute, mRoute, corridor, isToMatchCorridor))
                {
                    var m = mp;
                    if (!isMatchToPlatformPosting)
                        m = await _fillNotPlatformPosting.Fill(m);

                    var dirO = Geometry.DestDirection(posting.GetSourcePoint(), m.GetSourcePoint());
                    var lead =await CreateLead(posting, m,dirO, isGlobalExcluded);

                    //for response to request
                    leads.Add(lead);

                    if (service != null)
                        using (var scope = service.CreateScope())
                        {
                            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                            //publish event
                            foreach (var e in lead.DomainEvents)
                            {
                                
                                    await _mediator.Publish(e);
                               
                            }
                            return leads;
                        }
                   




                }
            }
            return leads;

        }
        private bool IsMatched(Route pRoute, Route mRoute, Corridor corridor, bool isToMatchCorridor)
        {
            return Geometry.IsMatchedRadius(pRoute, mRoute) ? true
                                                                  : isToMatchCorridor ? corridor.IsMatchedCorridor(mRoute)
                                                                                      : false;
        }
       
    }
}
