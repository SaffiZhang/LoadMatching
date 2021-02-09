using AutoMapper;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries;
using LoadLink.LoadMatching.Application.EquipmentPosting.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Services
{
    public class EquipmentPostingService : IEquipmentPostingService
    {
        private readonly IEquipmentPostingRepository _equipmentPostingRepository;
        private readonly IMapper _mapper;

        public EquipmentPostingService(IEquipmentPostingRepository equipmentPostingRepository, IMapper mapper)
        {
            _equipmentPostingRepository = equipmentPostingRepository;
            _mapper = mapper;
        }


        public async Task<CreateEquipmentPostingCommand> CreateAsync(CreateEquipmentPostingCommand createCommand)
        {
            var result = createCommand;
            var createResult = await _equipmentPostingRepository.CreateAsync(createCommand);

            if (createResult == -1)
                return null;

            //SP return a token on sucessful creation of the posting.
            result.Token = createResult;
            return result;
        }

        public async Task DeleteAsync(int token, string custCd, int userId)
        {
            await _equipmentPostingRepository.DeleteAsync(token, custCd, userId);
        }

        public async Task<GetEquipmentPostingQuery> GetAsync(int token, string custCd, string mileageProvider)
        {
            var result = await _equipmentPostingRepository.GetAsync(token ,custCd, mileageProvider);
            if (result == null)
                return null;

            return _mapper.Map<GetEquipmentPostingQuery>(result);
        }

        public async Task<IEnumerable<GetEquipmentPostingQuery>> GetListAsync(string custCd, string mileageProvider, bool? getDAT = false)
        {
            var result = await _equipmentPostingRepository.GetListAsync( custCd, mileageProvider, getDAT);
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetEquipmentPostingQuery>>(result);
        }

        public async Task UpdateAsync(int token, string pstatus)
        {
            await _equipmentPostingRepository.UpdateAsync(token, pstatus);
        }

        public async Task UpdateLeadCount(int token, int initialCount)
        {
            await _equipmentPostingRepository.UpdateLeadCount(token, initialCount);
        }
    
    }
}
