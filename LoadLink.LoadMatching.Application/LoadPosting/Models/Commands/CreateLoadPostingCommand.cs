using System;
using System.ComponentModel.DataAnnotations;

namespace LoadLink.LoadMatching.Application.LoadPosting.Models.Commands
{
    public class CreateLoadPostingCommand
    {
        public int Token { get; set; }

        public string CustCD { get; set; }

        [Required(ErrorMessage = "DateAvail is required"), DataType(DataType.DateTime, ErrorMessage = "Invalid Date")]
        public DateTime DateAvail { get; set; }

        [Required(ErrorMessage = "SrceCity is required")]
        public string SrceCity { get; set; }

        [Required(ErrorMessage = "SrceSt is required")]
        public string SrceSt { get; set; }

        [RegularExpression("50|100|150|200", ErrorMessage = "Invalid SrceRadius")]
        public int SrceRadius { get; set; }

        [Required(ErrorMessage = "DestCity is required")]
        public string DestCity { get; set; }

        [Required(ErrorMessage = "DestSt is required")]
        public string DestSt { get; set; }

        [RegularExpression("50|100|150|200", ErrorMessage = "Invalid DestRadius")]
        public int DestRadius { get; set; }

        [Required(ErrorMessage = "VehicleSize is required"),
           RegularExpression("Q|H|L|T|q|h|l|t", ErrorMessage = "Invalid VehicleSize")]
        public string VehicleSize { get; set; }

        [Required(ErrorMessage = "VehicleType is required")]
        public string VehicleType { get; set; }

        public string Comment { get; set; }

        public string PostMode { get; set; }

        public string ClientRefNum { get; set; }

        [Required(ErrorMessage = "ProductName is required")]
        public string ProductName { get; set; }

        public string PostingAttrib { get; set; }

        public int CreatedBy { get; set; }

        public int NetworkId { get; set; }

        public bool? GlobalExcluded { get; set; }
    }
}
