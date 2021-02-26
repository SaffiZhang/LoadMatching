using AutoMapper;
using LoadLink.LoadMatching.Application.LoadLiveLead.Models;
using LoadLink.LoadMatching.Application.LoadLiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.LoadLiveLead.Repository;
using LoadLink.LoadMatching.Application.LoadLiveLead.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadLiveLeadLiveLead.Services
{
    public class LoadLiveLeadService : ILoadLiveLeadService
    {
        private readonly ILoadLiveLeadRepository _loadLiveLeadLiveLeadRepository;
        private readonly IMapper _mapper;

        public LoadLiveLeadService(ILoadLiveLeadRepository loadLiveLeadLiveLeadRepository, IMapper mapper)
        {
            _loadLiveLeadLiveLeadRepository = loadLiveLeadLiveLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetLoadLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, 
                                                                            DateTime? leadfrom, int? postingId,
                                                                            LoadLiveLeadSubscriptionsStatus subscriptions)
        {
            var result = await _loadLiveLeadLiveLeadRepository.GetLeads(custCd, mileageProvider, leadfrom, postingId);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = subscriptions.HasQPSubscription ? row.QPStatus : 0;
                });

            return _mapper.Map<IEnumerable<GetLoadLiveLeadQuery>>(resultList);
        }
    }
}
