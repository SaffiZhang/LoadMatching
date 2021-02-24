using AutoMapper;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Models.Commands;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLead.Services
{
    public class DatEquipmentLeadService : IDatEquipmentLeadService
    {
        private readonly IDatEquipmentLeadRepository _datEquipmentLeadRepository;
        private readonly IMapper _mapper;

        public DatEquipmentLeadService(IDatEquipmentLeadRepository datEquipmentLeadRepository, 
                                        IMapper mapper)
        {
            _datEquipmentLeadRepository = datEquipmentLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDatEquipmentLeadQuery>> GetAsyncByPosting(string custCd, int postingId, DatEquipmentSubscriptionsStatus subscriptions, string mileageProvider)
        {
            var result = await _datEquipmentLeadRepository.GetByPosting(custCd, postingId, mileageProvider);
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

            return _mapper.Map<IEnumerable<GetDatEquipmentLeadQuery>>(resultList);
        }

        public async Task<IEnumerable<GetDatEquipmentLeadQuery>> GetListAsync(string custCd, DatEquipmentSubscriptionsStatus subscriptions, string mileageProvider)
        {
            var result = await _datEquipmentLeadRepository.GetList(custCd, mileageProvider);
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

            return _mapper.Map<IEnumerable<GetDatEquipmentLeadQuery>>(resultList);
        }
    }
}
