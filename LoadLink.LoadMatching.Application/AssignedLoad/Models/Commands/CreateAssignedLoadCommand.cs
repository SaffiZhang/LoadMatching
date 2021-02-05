
namespace LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands
{
    public class CreateAssignedLoadCommand
    {
        public int? UserId { get; set; }
        public int Token { get; set; }
        public string Instruction { get; set; }
        public int CreatedBy { get; set; }
        public int? EToken { get; set; }
    }
}
