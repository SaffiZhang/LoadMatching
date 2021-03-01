
namespace LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands
{
    public class GetUSCarrierSearchCommand
    {
        public int UserId { get; set; }
        public string SrceSt { get; set; }
        public string SrceCity { get; set; }
        public int SrceRadius { get; set; }
        public string DestSt { get; set; }
        public string DestCity { get; set; }
        public int DestRadius { get; set; }
        public string VehicleSize { get; set; }
        public string VehicleType { get; set; }
        public string CompanyName { get; set; }
        public string GetMexico { get; set; }

    }
}
