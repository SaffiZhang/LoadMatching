
using AutoMapper;
using LoadLink.LoadMatching.Application.EquipmentPosition.Models.Queries;
using LoadLink.LoadMatching.Application.EquipmentPosition.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentPosition.Services
{
    public class EquipmentPositionService : IEquipmentPositionService
    {
        private readonly IEquipmentPositionRepository _equipmentPositionRepository;
        private readonly IMapper _mapper;

        public EquipmentPositionService(IEquipmentPositionRepository equipmentPositionRepository, IMapper mapper)
        {
            _equipmentPositionRepository = equipmentPositionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetEquipmentPositionQuery>> GetListAsync(int token)
        {
            var result = await _equipmentPositionRepository.GetListAsync(token);

            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetEquipmentPositionQuery>>(result);
        }

        public async Task CreateAsync(int token)
        {
            await _equipmentPositionRepository.CreateAsync(token);
        }

    }
}
