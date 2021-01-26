


namespace LoadLink.LoadMatching.Domain.Entities
{
    public class Point
    {
        public int ID { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public short Country { get; set; }
        public short Region { get; set; }
        public int Radius { get; set; }
        public string ZIP { get; set; }
        public int Soundwait { get; set; }
        public int? MarketAreaID { get; set; }
        public bool isVisible { get; set; }
        public short? CityRank { get; set; }
    }
}