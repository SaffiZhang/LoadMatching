using AutoMapper;
using LoadLink.LoadMatching.Application.LiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.LiveLead.Models.Queries;
using LoadLink.LoadMatching.Application.LiveLead.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LiveLead.Services
{
    public class LiveLeadService : ILiveLeadService
    {
        private readonly ILiveLeadRepository _liveLeadRepository;
        private readonly IMapper _mapper;

        public LiveLeadService(ILiveLeadRepository liveLeadLiveLeadRepository, IMapper mapper)
        {
            _liveLeadRepository = liveLeadLiveLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetLiveLeadResult>> GetLiveLeads(GetLiveLeadRequest LLRequest, string mileageProvider, 
                                                                        string custCd, LiveLeadSubscriptionsStatus subscriptions)
        {
            var query = new GetLiveLeadQuery()
            {
                CustCD = custCd,
                MileageProvider = mileageProvider,
                LeadFrom = LLRequest.LeadFrom,
                LeadType = LLRequest.Type,
                GetBDAT = subscriptions.B_DATAPIKey_Status ? 1 : 0,
                GetCDAT = subscriptions.C_DATAPIKey_Status ? 1 : 0
            };

            var result = await _liveLeadRepository.GetLiveLeads(query);

            if (!result.Any())
                return null;


            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.QPStatus = ((row.PType == "L" && subscriptions.C_QPAPIKey_Status) || (row.PType == "E" && subscriptions.B_QPAPIKey_Status)) ? row.QPStatus : 0;
                    row.Equifax = ((row.PType == "L" && subscriptions.C_EQFAPIKey_Status) || (row.PType == "E" && subscriptions.B_EQFAPIKey_Status)) ? row.Equifax : -1;
                    row.TCC = ((row.PType == "L" && subscriptions.C_TCCAPIKey_Status) || (row.PType == "E" && subscriptions.B_TCCAPIKey_Status))? row.TCC : -1;
                    row.TCUS = ((row.PType == "L" && subscriptions.C_TCUSAPIKey_Status) || (row.PType == "E" && subscriptions.B_TCUSAPIKey_Status)) ? row.TCUS : -1;
                });

            return _mapper.Map<IEnumerable<GetLiveLeadResult>>(resultList);
        }

        public async Task<DateTime> GetServerTime()
        {
            return await _liveLeadRepository.GetServerTime();
        }
    }
}
