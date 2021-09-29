using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public class PostingDistanceAndPointId
    {
        public PostingDistanceAndPointId()
        {
        }

        public PostingDistanceAndPointId( int token,int srceID, 
            int srceCountry, double srceLong, double srceLat, 
            int srceMarketAreaID, int destID, int destCountry, 
            double destLong, double destLat, 
            int destMarketAreaID, int distance, int googleMileage)
        {
            Token = token;

            SrceID = srceID;
            SrceCountry = srceCountry;
            SrceLong = srceLong;
            SrceLat = srceLat;
            SrceMarketAreaID = srceMarketAreaID;
            
            DestID = destID;
            DestCountry = destCountry;
            DestLong = destLong;
            DestLat = destLat;
            DestMarketAreaID = destMarketAreaID;
            
            Distance = distance;
            GoogleMileage = googleMileage;
        }

        public int Token { get; }

        public int SrceID { get;}
       public int SrceCountry { get; }
       public double SrceLong { get; }
       public double SrceLat { get; }
       public int SrceMarketAreaID { get; set; }
       
        public int DestID { get; }
       public int DestCountry { get; }
       public double DestLong { get; }
       public double DestLat { get; }
        public int DestMarketAreaID { get; set; }

       public int Distance { get; }
       public int GoogleMileage { get; }
       public string PAttribStr { get; set; }
    }
}
