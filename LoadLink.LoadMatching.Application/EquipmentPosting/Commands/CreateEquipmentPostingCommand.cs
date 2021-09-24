using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using MediatR;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Commands
{
    public class CreateEquipmentPostingCommand:IRequest<IEnumerable<LeadBase>>
    {
        public int Token { get; set; }

        public string CustCD { get; set; }

        [Required(ErrorMessage = "DateAvail is required"), DataType(DataType.DateTime, ErrorMessage = "Invalid Date")]
        public DateTime DateAvail { get; set; }

        [Required(ErrorMessage = "SrceCity is required")]
        public string SrceCity { get; set; }

        [Required(ErrorMessage = "SrceSt is required")]
        public string SrceSt { get; set; }

        [RegularExpression("(?:5[0-0]{2}|1[0-0]{3}|50|100|150|200|300|400)", ErrorMessage = "Invalid SrceRadius")]
        public int SrceRadius { get; set; }

        [Required(ErrorMessage = "DestCity is required")]
        public string DestCity { get; set; }

        [Required(ErrorMessage = "DestSt is required")]
        public string DestSt { get; set; }

        [RegularExpression("(?:5[0-0]{2}|1[0-0]{3}|50|100|150|200|300|400)", ErrorMessage = "Invalid DestRadius")]
        public int DestRadius { get; set; }

        [Required(ErrorMessage = "VehicleSize is required"),
            RegularExpression("R|A|I|U|r|a|i|u", ErrorMessage = "Invalid VehicleSize")]
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


        [RegularExpression("C|R", ErrorMessage = "Invalid Corridor")]
        public string Corridor { get; set; }

        public bool? GlobalExcluded { get; set; }

        public bool CustomerTracking { get; set; }


    }
}
