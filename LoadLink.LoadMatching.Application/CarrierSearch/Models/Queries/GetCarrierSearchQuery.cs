using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries
{
    public class GetCarrierSearchQuery
    {
        public string SrceSt { get; set; }
        public string SrceCity { get; set; }
        public int SrceRadius { get; set; }
        public string DestSt { get; set; }
        public string DestCity { get; set; }
        public int DestRadius { get; set; }
        public string VehicleSize { get; set; }
        public string VehicleType { get; set; }
        public string PostingAttrib { get; set; }
        public string CompanyName { get; set; }
        public string GetDat { get; set; }
        public string GetMexico { get; set; }
    }
}
