using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.Entities;


namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Geometries
{
    public static class Geometry 
    {
        public static double Degrees(double radians)
        {
            return ((180 / Math.PI) * radians);
        }

        public static string DestDirection(Point srce, Point dest)
        {
            if ((srce.Long == dest.Long) && (dest.Lat == dest.Lat))
                return "-";
            if ((srce.Lat == dest.Lat))
                return srce.Long < dest.Long ? "E" : "W";
            var angle = Degrees(Math.Atan((dest.Lat - srce.Lat) / (dest.Long - srce.Long)));
            angle = angle < 0 ? dest.Long < srce.Long ? 180 + angle
                                              : 360 + angle
                              : dest.Long < srce.Long ? 180 + angle
                                              : angle;
          
         return angle < 22.6 ? "E"
                           : angle < 67.6 ? "NE"
                           : angle < 112.6 ? "N"
                           : angle < 157.6 ? "NW"
                           : angle < 202.6 ? "W"
                           : angle < 247.6 ? "SW"
                           : angle < 292.6 ? "S"
                           : angle < 337.5 ? "SE"
                           : "E";
           
          
        }

        public static double Distance(Point point1, Point point2)
        {
            if (point1.Long== 0 || point1.Lat == 0 || point2.Long == 0 || point2.Lat == 0)
                return 0;

            var tempResult = (Math.Cos(Radians(90 - point1.Lat)) * Math.Cos(Radians(90 - point2.Lat))) +
                (Math.Sin(Radians(90 - point1.Lat)) * Math.Sin(Radians(90 - point2.Lat)) * Math.Cos(Radians(point2.Long - point1.Long)));
            return (tempResult >= 1 || tempResult <= -1) ? 0 : Math.Abs((float)(3959 * Math.Acos(tempResult)));
        }

     
       

        public static bool IsMatchedRadius(Route radiusRoute, Route route)
        {
            return (!IsInRadius(radiusRoute.Source, route.Source)) ? false : IsInRadius(radiusRoute.Destination, route.Destination);
        }
        public static bool IsInRadius(Point radiusPoint, Point point)
        {
            return (Distance(radiusPoint, point) <= radiusPoint.Radius);
        }
        public static double Radians(double degree)
        {
            return (degree * Math.PI) / 180;
        }
    }
}
