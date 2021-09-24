using System;

namespace LoadLink.Library
{
    class Geometry
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.Write(Radians((float)-79.84861));
            Console.ReadLine();
            Console.WriteLine("degree");
            Console.WriteLine(Degrees(100));
            Console.ReadLine();
            Console.Write(Distance((float)-79.84861, (float)43.24639, (float)-79.39306, (float)43.6725));
            Console.ReadLine();
        }
       
        static double Distance(double lat1 , double long1, double lat2 , double long2)
        {
            if (lat1==0 || long1==0 || lat2==0 || long2==0)
            return 0;

            var tempResult = (Math.Cos(Radians(90 - lat1)) * Math.Cos(Radians(90 - lat2))) +
                (Math.Sin(Radians(90 - lat1)) * Math.Sin(Radians(90 - lat2)) * Math.Cos(Radians(long2 - long1)));
            return (tempResult >= 1 || tempResult <= -1) ? 0 : Math.Abs((float)(3959* Math.Acos(tempResult)));
            
        }
        static double Radians(double degree)
        {
            return (degree * Math.PI / 180);
        }
        static bool IsInCorridor(Point source, Point destination, Point point)
        {
            var fTheta = Degrees(((float)(source.Radius / 3959.0)));
            var fPhi = Degrees((float)(destination.Radius / 3959.0));
            var lAB = Math.Sqrt(
                Math.Pow((destination.Longitude - source.Longitude) ,2) +
                Math.Pow((destination.Latitude - source.Latitude),2)
                );
            var uABx = (destination.Longitude - source.Longitude) / lAB;
            var uABy = (destination.Latitude - source.Latitude) / lAB;

            var Cx = fTheta * -uABy;
            var Cy = fTheta * uABx;
            var Ex = fPhi * -uABy;
            var Ey = fPhi * uABx;

            var Px = source.Longitude - Cx;
            var Py = source.Latitude - Cy;
            var Ox = source.Longitude + Cx;
            var Oy = source.Latitude + Cy;
            var Mx = destination.Longitude - Ex;
            var My = destination.Latitude - Ey;
            var Nx = destination.Longitude + Ex;
            var Ny = destination.Latitude + Ey;


            return ((Nx - Mx) * (point.Latitude - My) - (point.Longitude - Mx) * (Ny - My)) >= 0 &
                ((Ox - Nx) * (point.Latitude - Ny) - (point.Longitude - Nx) * (Oy - Ny)) >= 0 &
                ((Px - Ox) * (point.Latitude - Oy) - (point.Longitude - Ox) * (Py - Oy)) >= 0 &
                ((Mx - Px) * (point.Latitude - Py) - (point.Longitude - Px) * (My - Py)) >= 0;
        }
        static double Degrees(double radians)
        {
            return ((180 / Math.PI) * radians);
        }
        static string DestDirection(double srceX, double srceY, double destX, double destY)
        {
            if ((srceX == destX) && (srceY == destY))
                return "-";
            if ((srceY == destY))
                return srceX < destX ? "E" : "W";
            var angle= Degrees(Math.Atan((destY-srceY)/(destX-srceX)));
            angle = angle < 0 ? destX < srceX ? 180 + angle 
                                              : 360 + angle
                              : destX < srceX ? 180 + angle 
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
       
    }
}
