
namespace LoadLink.LoadMatching.Application.PDRatio.Models.Commands
{
    public class GetPDRatioCommand
    {
        public string VehicleType { get; set; }
        public int VehicleTypeConverted { get; set; }
        public string SrceSt { get; set; }
        public string SrceCity { get; set; }
        public string DestSt { get; set; }
        public string DestCity { get; set; }
    }
}
