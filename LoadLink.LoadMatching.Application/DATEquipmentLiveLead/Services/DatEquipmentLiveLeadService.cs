using AutoMapper;
using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Models;
using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Services
{
    public class DatEquipmentLiveLeadService : IDatEquipmentLiveLeadService
    {

        private readonly IDatEquipmentLiveLeadRepository _datEquipmentLiveLeadRepository;
        private readonly IMapper _mapper;


        public bool HasEQSubscription { get; set; } = false;
        public bool HasQPSubscription { get; set; } = false;
        public bool HasTCUSSubscription { get; set; } = false;
        public bool HasTCCSubscription { get; set; } = false;


        public DatEquipmentLiveLeadService(IDatEquipmentLiveLeadRepository datEquipmentLiveLeadRepository, IMapper mapper)
        {
            _datEquipmentLiveLeadRepository = datEquipmentLiveLeadRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<GetDatEquipmentLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, DateTime? leadFrom, int? postingId)
        {
            var result = await _datEquipmentLiveLeadRepository.GetLeads(custCd,mileageProvider, leadFrom, postingId);
            if (!result.Any())
                return null;


            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = HasQPSubscription ? row.QPStatus : 0;
                });

            return _mapper.Map<IEnumerable<GetDatEquipmentLiveLeadQuery>>(resultList);
        }

  
    }
}
