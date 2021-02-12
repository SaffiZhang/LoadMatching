using AutoMapper;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Models;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATLoadLiveLead.Services
{
    public class DatLoadLiveLeadService : IDatLoadLiveLeadService
    {

        private readonly IDatLoadLiveLeadRepository _datLoadLiveLeadRepository;
        private readonly IMapper _mapper;


        public bool HasEQSubscription { get; set; } = false;
        public bool HasQPSubscription { get; set; } = false;
        public bool HasTCUSSubscription { get; set; } = false;
        public bool HasTCCSubscription { get; set; } = false;


        public DatLoadLiveLeadService(IDatLoadLiveLeadRepository datLoadLiveLeadRepository, IMapper mapper)
        {
            _datLoadLiveLeadRepository = datLoadLiveLeadRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<GetDatLoadLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, DateTime? leadFrom, int? postingId)
        {
            var result = await _datLoadLiveLeadRepository.GetLeads(custCd,mileageProvider, leadFrom, postingId);
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
                    row.QPStatus = HasQPSubscription ? row.QPStatus : -1;
                });

            return _mapper.Map<IEnumerable<GetDatLoadLiveLeadQuery>>(resultList);
        }

  
    }
}
