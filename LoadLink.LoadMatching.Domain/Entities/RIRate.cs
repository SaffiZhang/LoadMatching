


namespace LoadLink.LoadMatching.Domain.Entities
{
    public class RIRate
    {
        public int SrceMarketAreaID { get; set; }
        public int DestMarketAreaID { get; set; }
        public int RIVType { get; set; }
        public decimal Rate { get; set; }
        public string Extra { get; set; }
    }
}