using System;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries
{
    public class CreateEquipmentPostingQuery
    {
        public int Token { get; set; }

        public string CustCd { get; set; }

        public DateTime DateAvail { get; set; }

        public int SrceCountry { get; set; }

        public double SrceLong { get; set; }

        public double SrceLat { get; set; }

        public int SrceRadius { get; set; }

        public int DestCountry { get; set; }

        public double DestLong { get; set; }

        public double DestLat { get; set; }

        public int DestRadius { get; set; }

        public int VSize { get; set; }

        public int VType { get; set; }

        public string PostMode { get; set; }

        public int CreatedBy { get; set; }

        public int NetworkId { get; set; }

        public string Corridor { get; set; }

        public int OriginalToken { get; set; }

        public bool GlobalExcluded { get; set; }
    }
}
