using AutoMapper;
using LoadLink.LoadMatching.Application.EquipmentLead.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentLead.Models.Queries;
using LoadLink.LoadMatching.Application.EquipmentLead.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentLead.Services
{
    public class EquipmentLeadService : IEquipmentLeadService
    {
        private readonly IEquipmentLeadRepository _equipmentLeadRepository;
        private readonly IMapper _mapper;

        public EquipmentLeadService(IEquipmentLeadRepository equipmentLeadRepository, IMapper mapper)
        {
            _equipmentLeadRepository = equipmentLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetEquipmentLeadQuery>> GetListAsync(string custCD, string mileageProvider,
                                                                            EquipmentLeadSubscriptionsStatus subscriptions)
        {
            var result = await _equipmentLeadRepository.GetListAsync(custCD, mileageProvider);
            if (!result.Any())
                return null;

            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = subscriptions.HasQPSubscription && row.QPStatus;
                });

            return _mapper.Map<IEnumerable<GetEquipmentLeadQuery>>(resultList);
        }

        public async Task<IEnumerable<GetEquipmentLeadQuery>> GetByPostingAsync(string custCD, int postingId, string mileageProvider,
                                                                                EquipmentLeadSubscriptionsStatus subscriptions)
        {
            var result = await _equipmentLeadRepository.GetByPostingAsync(custCD, postingId, mileageProvider);
            if (!result.Any())
                return null;

            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = subscriptions.HasQPSubscription && row.QPStatus;
                });

            return _mapper.Map<IEnumerable<GetEquipmentLeadQuery>>(resultList);
        }

        public async Task<IEnumerable<GetEquipmentLeadCombinedQuery>> GetCombinedAsync(string custCD, int postingId,
                                                                                        string mileageProvider, int leadsCap,
                                                                                        EquipmentLeadSubscriptionsStatus subscriptions)
        {
            var result = await _equipmentLeadRepository.GetCombinedAsync(custCD, postingId, mileageProvider, leadsCap, 
                                                                            subscriptions.HasDATSubscription);
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

            return _mapper.Map<IEnumerable<GetEquipmentLeadCombinedQuery>>(resultList);
        }
    }
}
