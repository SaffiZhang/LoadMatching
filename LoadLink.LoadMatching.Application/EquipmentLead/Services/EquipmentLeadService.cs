using AutoMapper;
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

        public bool HasQPSubscription { get; set; } = false;
        public bool HasDATSubscription { get; set; } = false;
        public bool HasEQSubscription { get; set; } = false;
        public bool HasTCCSubscription { get; set; } = false;
        public bool HasTCUSSubscription { get; set; } = false;


        public EquipmentLeadService(IEquipmentLeadRepository equipmentLeadRepository, IMapper mapper)
        {
            _equipmentLeadRepository = equipmentLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetEquipmentLeadQuery>> GetListAsync(string custCD)
        {
            var result = await _equipmentLeadRepository.GetListAsync(custCD);
            if (!result.Any())
                return null;

            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = HasQPSubscription && row.QPStatus;
                });

            return _mapper.Map<IEnumerable<GetEquipmentLeadQuery>>(resultList);
        }

        public async Task<IEnumerable<GetEquipmentLeadQuery>> GetByPostingAsync(string custCD, int postingId)
        {
            var result = await _equipmentLeadRepository.GetByPostingAsync(custCD, postingId);
            if (!result.Any())
                return null;

            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = HasQPSubscription && row.QPStatus;
                });

            return _mapper.Map<IEnumerable<GetEquipmentLeadQuery>>(resultList);
        }

        public async Task<IEnumerable<GetEquipmentLeadCombinedQuery>> GetCombinedAsync(string custCD, int postingId)
        {
            var result = await _equipmentLeadRepository.GetCombinedAsync(custCD, postingId, HasDATSubscription);
            if (!result.Any())
                return null;

            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = HasQPSubscription ? row.QPStatus : 0;
                });

            return _mapper.Map<IEnumerable<GetEquipmentLeadCombinedQuery>>(resultList);
        }
    }
}
