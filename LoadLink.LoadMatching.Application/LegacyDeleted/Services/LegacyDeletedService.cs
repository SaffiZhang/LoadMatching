using AutoMapper;
using LoadLink.LoadMatching.Application.LegacyDeleted.Models.Queries;
using LoadLink.LoadMatching.Application.LegacyDeleted.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LegacyDeleted.Services
{
    public class LegacyDeletedService : ILegacyDeletedService
    {
        private readonly ILegacyDeletedRepository _legacyDeletedRepository;
        private readonly IMapper _mapper;

        public LegacyDeletedService(ILegacyDeletedRepository legacyDeletedRepository, IMapper mapper)
        {
            _legacyDeletedRepository = legacyDeletedRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUserLegacyDeletedQuery>> GetListAsync(char leadType, string custCd)
        {
            var result = await _legacyDeletedRepository.GetListAsync(leadType, custCd);

            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetUserLegacyDeletedQuery>>(result);
        }

        public async Task UpdateAsync(char leadType, string custCd)
        {
            await _legacyDeletedRepository.UpdateAsync(leadType, custCd);
        }

    }
}
