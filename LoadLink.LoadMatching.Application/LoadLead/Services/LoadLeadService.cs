using AutoMapper;
using LoadLink.LoadMatching.Application.LoadLead.Models.Commands;
using LoadLink.LoadMatching.Application.LoadLead.Models.Queries;
using LoadLink.LoadMatching.Application.LoadLead.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadLead.Services
{
    public class LoadLeadService : ILoadLeadService
    {
        private readonly ILoadLeadRepository _LoadLeadRepository;
        private readonly IMapper _mapper;
        
        public LoadLeadService(ILoadLeadRepository LoadLeadRepository, IMapper mapper)
        {
            _LoadLeadRepository = LoadLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetLoadLeadQuery>> GetByPostingAsync(string custCd, int postingID, string mileageProvider,
                                                                            LoadLeadSubscriptionsStatus subscriptions)
        {
            var result = await _LoadLeadRepository.GetByPostingAsync(custCd, postingID, mileageProvider);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.QPStatus = subscriptions.HasQPSubscription ? row.QPStatus : 0;
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                });

            return _mapper.Map<IEnumerable<GetLoadLeadQuery>>(result);
        }

        public async Task<IEnumerable<GetLoadLeadQuery>> GetListAsync(string custCd, string mileageProvider,
                                                                        LoadLeadSubscriptionsStatus subscriptions)
        {
            var result = await _LoadLeadRepository.GetListAsync(custCd, mileageProvider);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.QPStatus = subscriptions.HasQPSubscription ? row.QPStatus : 0;
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                });

            return _mapper.Map<IEnumerable<GetLoadLeadQuery>>(result);
        }

        public async Task<IEnumerable<GetLoadLeadQuery>> GetByPosting_CombinedAsync(string custCd, int postingID,
                                                                                    string mileageProvider, int leadsCap,
                                                                                    LoadLeadSubscriptionsStatus subscriptions)
        {
            var result = await _LoadLeadRepository.GetByPosting_CombinedAsync(custCd, postingID, mileageProvider, 
                                                                                leadsCap, subscriptions.HasDATSubscription);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.QPStatus = subscriptions.HasQPSubscription ? row.QPStatus : 0;
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                });

            return _mapper.Map<IEnumerable<GetLoadLeadQuery>>(result);
        }
    }
}
