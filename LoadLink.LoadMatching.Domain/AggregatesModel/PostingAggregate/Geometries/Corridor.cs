using System;
using System.Collections.Generic;
using LoadLink.LoadMatching.Domain.Entities;


namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Geometries
{
    public class Corridor
    {
        private Point source;
        private Point destination;
        private double uABx;
        private double uABy;
        private double Nx;
        private double Ny;
        private double Mx;
        private double My;
        private double Ox;
        private double Oy;
        private double Px;
        private double Py;

        public Corridor(Route route)
        {
            source = route.Source;
            destination = route.Destination;
            var fTheta =Geometry.Degrees(((float)(source.Radius / 3959.0)));
            var fPhi = Geometry.Degrees((float)(destination.Radius / 3959.0));
            var lAB = Math.Sqrt(
                Math.Pow((destination.Long - source.Long), 2) +
                Math.Pow((destination.Lat - source.Lat), 2)
                );
            uABx = (destination.Long - source.Long) / lAB;
            uABy = (destination.Lat - source.Lat) / lAB;

            var Cx = fTheta * -uABy;
            var Cy = fTheta * uABx;
            var Ex = fPhi * -uABy;
            var Ey = fPhi * uABx;

            Px = source.Long - Cx;
            Py = source.Lat - Cy;
            Ox = source.Long + Cx;
            Oy = source.Lat + Cy;
            Mx = destination.Long - Ex;
            My = destination.Lat - Ey;
            Nx = destination.Long + Ex;
            Ny = destination.Lat + Ey;
        }
        public bool IsInCorridor(Point point)
        {
            return
                ((Nx - Mx) * (point.Lat - My) - (point.Long - Mx) * (Ny - My)) >= 0 &
               ((Ox - Nx) * (point.Lat - Ny) - (point.Long - Nx) * (Oy - Ny)) >= 0 &
               ((Px - Ox) * (point.Lat - Oy) - (point.Long - Ox) * (Py - Oy)) >= 0 &
               ((Mx - Px) * (point.Lat - Py) - (point.Long - Px) * (My - Py)) >= 0;
        }
       
        public bool IsInSameGeneralDirection(Route route)
        {
            return (uABx * (route.Destination.Long - route.Source.Long) + uABy * (route.Destination.Lat - route.Source.Lat)) > 0 ? true : false;
        }
        public bool IsMatchedCorridor(Route route)
        {
            if (!IsInSameGeneralDirection(route))
                return false;

            if (Geometry.IsInRadius(source, route.Source) & IsInCorridor(route.Destination))
                return true;
            
            if (IsInCorridor(route.Source) & (Geometry.IsInRadius(destination, route.Destination) || IsInCorridor(route.Destination)))
                return true;
            
            return false;
        }
    }
}
