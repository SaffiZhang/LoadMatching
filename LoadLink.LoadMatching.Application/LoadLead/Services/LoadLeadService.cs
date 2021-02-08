using AutoMapper;
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

        public bool HasDATStatusEnabled { get; set; } = false;
        public bool HasQPSubscription { get; set; } = false;
        public bool HasEQSubscription { get; set; } = false;
        public bool HasTCSubscription { get; set; } = false;
        public bool HasTCUSSubscription { get; set; } = false;
        
        public LoadLeadService(ILoadLeadRepository LoadLeadRepository, IMapper mapper)
        {
            _LoadLeadRepository = LoadLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetLoadLeadQuery>> GetByPostingAsync(int postingID, string custCd, string mileageProvider)
        {
            var result = await _LoadLeadRepository.GetByPostingAsync(postingID, custCd, mileageProvider);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.QPStatus = HasQPSubscription ? row.QPStatus : 0;
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                });

            return _mapper.Map<IEnumerable<GetLoadLeadQuery>>(result);
        }

        public async Task<IEnumerable<GetLoadLeadQuery>> GetListAsync(string custCd, string mileageProvider)
        {
            var result = await _LoadLeadRepository.GetListAsync(custCd, mileageProvider);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.QPStatus = HasQPSubscription ? row.QPStatus : 0;
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                });

            return _mapper.Map<IEnumerable<GetLoadLeadQuery>>(result);
        }

        public async Task<IEnumerable<GetLoadLeadQuery>> GetByPosting_CombinedAsync(int postingID, string custCd, string mileageProvider, int leadsCap)
        {
            var result = await _LoadLeadRepository.GetByPosting_CombinedAsync(postingID, custCd, mileageProvider, HasDATStatusEnabled, leadsCap);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.QPStatus = HasQPSubscription ? row.QPStatus : 0;
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                });

            return _mapper.Map<IEnumerable<GetLoadLeadQuery>>(result);
        }
    }
}
