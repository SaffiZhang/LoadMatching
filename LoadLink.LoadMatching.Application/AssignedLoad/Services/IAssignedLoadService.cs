using LoadLink.LoadMatching.Application.AssignedLoad.Models.Queries;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.AssignedLoad.Services
{
    public interface IAssignedLoadService
    {
        Task<GetAssignedLoadQuery> GetAsync(int token, int userId);
        Task<IEnumerable<GetAssignedLoadQuery>> GetListAsync(int userId);
        Task<string> CreateAsync(CreateAssignedLoadCommand createAssignedLoadCommand);
        Task<int> UpdateAsync(UpdateAssignedLoadCommand updateAssignedLoadCommand);
        Task<int> UpdateCustomerTrackingAsync(UpdateCustomerTrackingCommand updateCustomerTrackingCommand);
        Task<DeleteAssignedLoadQuery> DeleteAsync(int token, int userId);
    }
}
