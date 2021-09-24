using Xunit;
using Moq;
using System;

using LoadLink.LoadMatching.Domain.Entities;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Geometries;


namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public class GeometryShould
    {
        private Point ottawa = new Point(-75.68861, 45.41972, 20);
        private Point cambridge = new Point(-80.29972, 43.36583, 20);
        private Point kanata = new Point(-75.91139, 45.34306, 20);// in ottawa radius
        private Point peterborough= new Point(-78.32083, 44.28278, 20);// in corridor
        private Point mississauga = new Point(-79.61167, 43.58167);//in corridor
        private Point waterloo = new Point(-80.51083, 43.46389, 20);//in cambridge radius
        private Point sudbury = new Point(-80.99667, 46.46222);
       
      
     
        // Distance between Ottawa and Brampton
        // select dbo.udf_GetDistance(45.41972,-75.68861, 43.58167,-79.61167 ) , return 231.282126977812

        //select [dbo].[udf_DestDirection] (-75.68861, 45.41972,-79.61167, 43.58167), return SW
        [Fact]
        public void SpatialShould()
        {
           
           
            var d1 = Geometry.Distance(ottawa, mississauga);
            Assert.Equal(Math.Round(231.282126977812, 4), Math.Round(d1, 4));

            var direction = Geometry.DestDirection(ottawa, mississauga);
            Assert.Equal("SW",direction );
           
            Assert.True(Geometry.IsInRadius(ottawa, kanata));

            

           
            var cRoute = new Route(peterborough, mississauga);
            
            var wRoute = new Route(cambridge, ottawa);   
          
            Assert.True(Geometry.IsMatchedRadius(new Route(ottawa, cambridge), new Route(kanata, waterloo)));
            Assert.False(Geometry.IsMatchedRadius(new Route(ottawa, cambridge), new Route(peterborough, mississauga)));

            Assert.False(Geometry.IsInRadius(ottawa, peterborough));
            Assert.False(Geometry.IsInRadius(cambridge, peterborough));

            Assert.False(Geometry.IsInRadius(ottawa, mississauga));
            Assert.False(Geometry.IsInRadius(cambridge, mississauga));

            var corridor = new Corridor(new Route(ottawa, cambridge));
            Assert.True (corridor.IsInCorridor(peterborough));
            Assert.True(corridor.IsInCorridor(mississauga));

            Assert.True(corridor.IsMatchedCorridor(new Route(peterborough, mississauga)));

            Assert.False(corridor.IsMatchedCorridor(new Route(mississauga, peterborough)));// wrong direction
            
            Assert.False(corridor.IsMatchedCorridor(new Route(ottawa, sudbury)));// outside corridor
            Assert.True(corridor.IsMatchedCorridor(new Route(ottawa, peterborough)));

        }
       
       

    }
}
