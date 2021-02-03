using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Queries;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.AssignedEquipment.Services
{
    public interface IAssignedEquipmentService
    {
        Task<GetAssignedLoadQuery> GetAsync(int token);
        Task<int> UpdateAsync(UpdateAssignedEquipmentCommand updateAssignedEquipmentCommand);
    }
}
