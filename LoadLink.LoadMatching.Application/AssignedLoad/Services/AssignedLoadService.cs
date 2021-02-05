using AutoMapper;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Queries;
using LoadLink.LoadMatching.Application.AssignedLoad.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.AssignedLoad.Services
{
    public class AssignedLoadService : IAssignedLoadService
    {
        private readonly IAssignedLoadRepository _assignedLoadRepository;
        private readonly IMapper _mapper;

        public AssignedLoadService(IAssignedLoadRepository assignedLoadRepository, IMapper mapper)
        {
            _assignedLoadRepository = assignedLoadRepository;
            _mapper = mapper;
        }

        public async Task<GetAssignedLoadQuery> GetAsync(int token, int userId)
        {
            var result = await _assignedLoadRepository.GetAsync(token, userId);
            
            if (result == null)
                return null;

            return _mapper.Map<GetAssignedLoadQuery>(result);
        }

        public async Task<IEnumerable<GetAssignedLoadQuery>> GetListAsync(int userId)
        {
            var result = await _assignedLoadRepository.GetListAsync(userId);
            
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetAssignedLoadQuery>>(result);
        }

        public async Task<string> CreateAsync(CreateAssignedLoadCommand createAssignedLoadCommand)
        {
            var result = await _assignedLoadRepository.CreateAsync(createAssignedLoadCommand);

            return result;
        }

        public async Task<int> UpdateAsync(UpdateAssignedLoadCommand updateAssignedLoadCommand)
        {
            var result = await _assignedLoadRepository.UpdateAsync(updateAssignedLoadCommand);

            return result;
        }

        public async Task<int> UpdateCustomerTrackingAsync(UpdateCustomerTrackingCommand updateCustomerTrackingCommand)
        {
            var result = await _assignedLoadRepository.UpdateCustomerTrackingAsync(updateCustomerTrackingCommand);

            return result;
        }

        public async Task<DeleteAssignedLoadQuery> DeleteAsync(int token, int userId)
        {
            var result = await _assignedLoadRepository.DeleteAsync(token, userId);

            if (result == null)
                return null;

            return _mapper.Map<DeleteAssignedLoadQuery>(result);
        }

    }
}
