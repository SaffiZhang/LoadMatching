namespace LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries
{
    public class GetCarrierSearchQuery
    {
        public int UserID { get; set; }
        public string SrceSt { get; set; }
        public string SrceCity { get; set; }
        public int SrceRadius { get; set; }
        public string DestSt { get; set; }
        public string DestCity { get; set; }
        public int DestRadius { get; set; }
        public int VSize { get; set; }
        public int VType { get; set; }
        public int PAttrib { get; set; }
        public string CompanyName { get; set; }
        public int GetDat { get; set; }
        public int GetMexico { get; set; }
        public string ServerName { get; set; }
    }
}
