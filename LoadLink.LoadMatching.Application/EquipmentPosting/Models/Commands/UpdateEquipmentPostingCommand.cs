using System.ComponentModel.DataAnnotations;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands
{
    public class UpdateEquipmentPostingCommand
    {
        [Required(ErrorMessage = "Status required")]
        public string PStatus { get; set; }
    }
}
