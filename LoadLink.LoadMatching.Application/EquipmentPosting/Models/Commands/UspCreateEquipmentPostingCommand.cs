using System;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands
{
    public class UspCreateEquipmentPostingCommand
    {

        public string CustCD { get; set; }

        public DateTime DateAvail { get; set; }

        public string SrceCity { get; set; }

        public string SrceSt { get; set; }

        public int SrceRadius { get; set; }

        public string DestCity { get; set; }
        public string DestSt { get; set; }

        public int DestRadius { get; set; }

        public int VSize { get; set; }

        public int VType { get; set; }

        public string Comment { get; set; }

        public string PostMode { get; set; }

        public string ClientRefNum { get; set; }

        public string ProductName { get; set; }

        public int PAttrib { get; set; }

        public int CreatedBy { get; set; }

        public int NetworkId { get; set; }

        public string Corridor { get; set; }

        public int GlobalExcluded { get; set; }

        public int CustomerTracking { get; set; }


    }
}
