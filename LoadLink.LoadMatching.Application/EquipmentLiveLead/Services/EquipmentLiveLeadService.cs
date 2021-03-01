using AutoMapper;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Models;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Repository;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentLiveLeadLiveLead.Services
{
    public class EquipmentLiveLeadService : IEquipmentLiveLeadService
    {

        private readonly IEquipmentLiveLeadRepository _EquipmentLiveLeadLiveLeadRepository;
        private readonly IMapper _mapper;

        public EquipmentLiveLeadService(IEquipmentLiveLeadRepository EquipmentLiveLeadLiveLeadRepository, IMapper mapper)
        {
            _EquipmentLiveLeadLiveLeadRepository = EquipmentLiveLeadLiveLeadRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<GetEquipmentLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, 
                                                                                DateTime? leadfrom, int? postingId,
                                                                                EquipmentLiveLeadSubscriptionsStatus subscriptions)
        {
            var result = await _EquipmentLiveLeadLiveLeadRepository.GetLeads(custCd, mileageProvider, leadfrom, postingId);
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
                    row.QPStatus = subscriptions.HasQPSubscription && row.QPStatus;
                });

            return _mapper.Map<IEnumerable<GetEquipmentLiveLeadQuery>>(resultList);
        }
    }
}
