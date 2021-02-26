
using System.ComponentModel.DataAnnotations;

namespace LoadLink.LoadMatching.Application.Networks.Models.Commands
{
    public class PatchNetworksCommand
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
    }
}
