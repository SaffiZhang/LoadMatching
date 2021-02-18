using AutoMapper;
using LoadLink.LoadMatching.Application.Flag.Models.Commands;
using LoadLink.LoadMatching.Application.Flag.Models.Queries;
using LoadLink.LoadMatching.Application.Flag.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Flag.Services
{
    public class FlagService : IFlagService
    {
        private readonly IFlagRepository _flagRepository;
        private readonly IMapper _mapper;

        public FlagService(IFlagRepository flagRepository, IMapper mapper)
        {
            _flagRepository = flagRepository;
            _mapper = mapper;
        }

        public async Task<GetFlagQuery> GetAsync(string custCd, int Id)
        {
            var result = await _flagRepository.GetAsync(custCd, Id);

            if (result == null)
                return null;

            return _mapper.Map<GetFlagQuery>(result);
        }

        public async Task<IEnumerable<GetFlagQuery>> GetListAsync(string custCd)
        {
            var result = await _flagRepository.GetListAsync(custCd);

            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetFlagQuery>>(result);
        }

        public async Task<CreateFlagCommand> CreateAsync(CreateFlagCommand createFlagCommand)
        {
            var result = await _flagRepository.CreateAsync(createFlagCommand);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _flagRepository.DeleteAsync(id);
        }

    }
}
