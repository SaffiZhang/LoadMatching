
namespace LoadLink.LoadMatching.Application.AssignedEquipment.Models.Commands
{
    public class UpdateAssignedEquipmentCommand
    {
        public string PIN { get; set; }
        public int EToken { get; set; }
        public int UpdatedBy { get; set; }
        public int? DriverID { get; set; }
        public int? EquipmentID { get; set; }
        public int? UserId { get; set; }
    }
}
