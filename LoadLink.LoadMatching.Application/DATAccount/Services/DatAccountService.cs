
using AutoMapper;
using LoadLink.LoadMatching.Application.DATAccount.Models.Queries;
using LoadLink.LoadMatching.Application.DATAccount.Repository;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATAccount.Services
{
    public class DatAccountService : IDatAccountService
    {
        private readonly IDatAccountRepository _datAccountRepository;
        private readonly IMapper _mapper;

        public DatAccountService(IDatAccountRepository datAccountRepository, IMapper mapper)
        {
            _datAccountRepository = datAccountRepository;
            _mapper = mapper;
        }

        public async Task<GetDatAccountQuery> GetAsync(string custCd)
        {
            var result = await _datAccountRepository.GetAsync(custCd);
            if (result == null)
                return null;

            return _mapper.Map<GetDatAccountQuery>(result);
        }
    }
}
