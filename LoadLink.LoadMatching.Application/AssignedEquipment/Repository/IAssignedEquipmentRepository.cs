using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.AssignedEquipment.Repository
{
    public interface IAssignedEquipmentRepository
    {
        Task<UspGetAssignedLoadResult> GetAsync(int token);
        Task<int> UpdateAsync(UpdateAssignedEquipmentCommand updateAssignedEquipmentCommand);
    }
}
