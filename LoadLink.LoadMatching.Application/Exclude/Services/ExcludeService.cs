using AutoMapper;
using LoadLink.LoadMatching.Application.Exclude.Models.Commands;
using LoadLink.LoadMatching.Application.Exclude.Models.Queries;
using LoadLink.LoadMatching.Application.Exclude.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Exclude.Services
{
    public class ExcludeService : IExcludeService
    {
        private readonly IExcludeRepository _excludeRepository;
        private readonly IMapper _mapper;

        public ExcludeService(IExcludeRepository excludeRepository, IMapper mapper)
        {
            _excludeRepository = excludeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetExcludeQuery>> GetListAsync(string custCd)
        {
            var result = await _excludeRepository.GetListAsync(custCd);

            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetExcludeQuery>>(result);
        }

        public async Task<CreateExcludeCommand> CreateAsync(CreateExcludeCommand createExcludeCommand)
        {
            var result = await _excludeRepository.CreateAsync(createExcludeCommand);

            return result;
        }

        public async Task DeleteAsync(string custCd, string excludeCustCd)
        {
            await _excludeRepository.DeleteAsync(custCd, excludeCustCd);
        }

    }
}
