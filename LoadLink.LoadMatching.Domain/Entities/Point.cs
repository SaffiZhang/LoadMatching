


namespace LoadLink.LoadMatching.Domain.Entities
{
    public class Point
    {
        public Point(double @long, double lat, int? radius=null, 
            int? id=null, string city=null, string stateCode=null, short? country=null,   int? marketAreaID=null)
        {
            Long = @long;
            Lat = lat;

            if (id != null)
                ID = id.Value;
            if (radius != null)
                this.Radius = radius.Value;
            City = city;
            StateCode = stateCode;
           
            if (country != null)
                Country = country.Value;
            MarketAreaID = marketAreaID;

        }

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