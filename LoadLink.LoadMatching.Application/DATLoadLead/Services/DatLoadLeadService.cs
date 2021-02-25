using AutoMapper;
using LoadLink.LoadMatching.Application.DATLoadLead.Models.Commands;
using LoadLink.LoadMatching.Application.DATLoadLead.Models.Queries;
using LoadLink.LoadMatching.Application.DATLoadLead.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATLoadLead.Services
{
    public class DatLoadLeadService : IDatLoadLeadService
    {
        private readonly IDatLoadLeadRepository _datLoadLeadRepository;
        private readonly IMapper _mapper;

        public DatLoadLeadService(IDatLoadLeadRepository datLoadLeadRepository, IMapper mapper)
        {
            _datLoadLeadRepository = datLoadLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDatLoadLeadQuery>> GetListAsync(string custCD, string mileageProvider,
                                                                            DatLoadLeadSubscriptionsStatus subscriptions)
        {
            var result = await _datLoadLeadRepository.GetListAsync(custCD, mileageProvider);
            if (!result.Any())
                return null;

            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = subscriptions.HasQPSubscription ? row.QPStatus : 0;
                });

            return _mapper.Map<IEnumerable<GetDatLoadLeadQuery>>(resultList);
        }

        public async Task<IEnumerable<GetDatLoadLeadQuery>> GetByPostingAsync(string custCD, int postingId, string mileageProvider,
                                                                                DatLoadLeadSubscriptionsStatus subscriptions)
        {
            var result = await _datLoadLeadRepository.GetByPostingAsync(custCD, postingId, mileageProvider);
            if (!result.Any())
                return null;

            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = subscriptions.HasQPSubscription ? row.QPStatus : 0;
                });

            return _mapper.Map<IEnumerable<GetDatLoadLeadQuery>>(resultList);
        }
    }
}
