using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.AssignedLoad.Repository
{
    public interface IAssignedLoadRepository
    {
        Task<UspGetAssignedLoadResult> GetAsync(int token, int userId);
        Task<IEnumerable<UspGetAssignedLoadResult>> GetListAsync(int userId);
        Task<string> CreateAsync(CreateAssignedLoadCommand createAssignedLoadCommand);
        Task<int> UpdateAsync(UpdateAssignedLoadCommand updateAssignedLoadCommand);
        Task<int> UpdateCustomerTrackingAsync(UpdateCustomerTrackingCommand updateCustomerTrackingCommand);
        Task<UspDeleteAssignedLoadResult> DeleteAsync(int token, int userId);
    }
}
