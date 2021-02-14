using AutoMapper;
using LoadLink.LoadMatching.Application.Member.Models.Commands;
using LoadLink.LoadMatching.Application.Member.Models.Queries;
using LoadLink.LoadMatching.Application.Member.Repository;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Member.Services
{
    public class MemberService :IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<GetMemberQuery> GetAsync(string custCd, string memberCustCd)
        {
            var result = await _memberRepository.GetAsync(custCd, memberCustCd);

            if (result == null)
                return null;

            return _mapper.Map<GetMemberQuery>(result);
        }

        public async Task<CreateMemberCommand> CreateAsync(CreateMemberCommand createMemberCommand)
        {
            var result = await _memberRepository.CreateAsync(createMemberCommand);

            return result;
        }

        public async Task UpdateAsync(UpdateMemberCommand updateMemberCommand)
        {
            await _memberRepository.UpdateAsync(updateMemberCommand);
        }

        public async Task DeleteAsync(int memberId)
        {
            await _memberRepository.DeleteAsync(memberId);
        }

    }
}
