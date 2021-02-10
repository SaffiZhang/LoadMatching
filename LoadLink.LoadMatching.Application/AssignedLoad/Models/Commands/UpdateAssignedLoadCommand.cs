
namespace LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands
{
    public class UpdateAssignedLoadCommand
    {
        public string PIN { get; set; }
        public int UserId { get; set; }
        public int UpdatedBy { get; set; }
        public int? DriverID { get; set; }
    }
}
