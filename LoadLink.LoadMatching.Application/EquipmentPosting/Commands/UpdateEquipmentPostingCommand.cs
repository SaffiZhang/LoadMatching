using System.ComponentModel.DataAnnotations;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands
{
    public class UpdatEquipmentPostingCommand
    {
        [Required(ErrorMessage = "Status required")]
        public string PStatus { get; set; }
    }
}
