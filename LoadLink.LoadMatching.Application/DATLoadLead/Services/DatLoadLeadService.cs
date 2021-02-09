using AutoMapper;
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

        public bool HasQPSubscription { get; set; } = false;
        public bool HasEQSubscription { get; set; } = false;
        public bool HasTCCSubscription { get; set; } = false;
        public bool HasTCUSSubscription { get; set; } = false;


        public DatLoadLeadService(IDatLoadLeadRepository datLoadLeadRepository, IMapper mapper)
        {
            _datLoadLeadRepository = datLoadLeadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDatLoadLeadQuery>> GetListAsync(string custCD)
        {
            var result = await _datLoadLeadRepository.GetListAsync(custCD);
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

            return _mapper.Map<IEnumerable<GetDatLoadLeadQuery>>(resultList);
        }

        public async Task<IEnumerable<GetDatLoadLeadQuery>> GetByPostingAsync(string custCD, int postingId)
        {
            var result = await _datLoadLeadRepository.GetByPostingAsync(custCD, postingId);
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

            return _mapper.Map<IEnumerable<GetDatLoadLeadQuery>>(resultList);
        }
    }
}
