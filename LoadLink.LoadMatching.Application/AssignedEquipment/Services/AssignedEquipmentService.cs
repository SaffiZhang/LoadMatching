using AutoMapper;
using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Queries;
using LoadLink.LoadMatching.Application.AssignedEquipment.Repository;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.AssignedEquipment.Services
{
    public class AssignedEquipmentService : IAssignedEquipmentService
    {
        private readonly IAssignedEquipmentRepository _assignedEquipmentRepository;
        private readonly IMapper _mapper;

        public AssignedEquipmentService(IAssignedEquipmentRepository assignedEquipmentRepository, IMapper mapper)
        {
            _assignedEquipmentRepository = assignedEquipmentRepository;
            _mapper = mapper;
        }

        public async Task<GetAssignedLoadQuery> GetAsync(int token)
        {
            var result = await _assignedEquipmentRepository.GetAsync(token);
            if (result == null)
                return null;

            return _mapper.Map<GetAssignedLoadQuery>(result);
        }

        public async Task<int> UpdateAsync(UpdateAssignedEquipmentCommand updateAssignedEquipmentCommand)
        {
            var result = await _assignedEquipmentRepository.UpdateAsync(updateAssignedEquipmentCommand);

            return result;
        }
    }
}
