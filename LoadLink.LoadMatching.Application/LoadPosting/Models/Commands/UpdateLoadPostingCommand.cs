using System.ComponentModel.DataAnnotations;

namespace LoadLink.LoadMatching.Application.LoadPosting.Models.Commands
{
    public class UpdateLoadPostingCommand
    {
        [Required(ErrorMessage = "Status required")]
        public string PStatus { get; set; }
    }
}
