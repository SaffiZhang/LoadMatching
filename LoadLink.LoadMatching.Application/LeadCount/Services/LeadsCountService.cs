
using AutoMapper;
using LoadLink.LoadMatching.Application.LeadCount.Models.Queries;
using LoadLink.LoadMatching.Application.LeadCount.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LeadCount.Services
{
    public class LeadsCountService : ILeadsCountService
    {
        private readonly ILeadsCountRepository _leadsCountRepository;
        private readonly IMapper _mapper;

        public LeadsCountService(ILeadsCountRepository leadsCountRepository, IMapper mapper)
        {
            _leadsCountRepository = leadsCountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetLeadsCountQuery>> GetLeadsCountAsync(int userId, int token, bool getDAT, string type)
        {
            IEnumerable<GetLeadsCountQuery> resultList = null; 

            if (type.ToUpper() == "L")
            {
                var result = await _leadsCountRepository.GetLoadLeadsCountAsync(userId, token, getDAT);
                if (!result.Any())
                    return null;

                resultList = _mapper.Map<IEnumerable<GetLeadsCountQuery>>(result);
            }
            else if (type.ToUpper() == "E")
            {
                var result = await _leadsCountRepository.GetEquipLeadsCountAsync(userId, token, getDAT);
                if (!result.Any())
                    return null;

                resultList = _mapper.Map<IEnumerable<GetLeadsCountQuery>>(result);
            }

            return resultList;
        }
    }
}
