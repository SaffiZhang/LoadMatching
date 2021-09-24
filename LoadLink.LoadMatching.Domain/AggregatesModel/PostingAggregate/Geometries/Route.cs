using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.Entities;
using LoadLink.LoadMatching.Domain.Seedwork;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Geometries
{
    public class Route:ValueObject
    {
        public Route(Point source, Point destination)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

        public Point Source { get; set; }
        public Point Destination { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Source.ID;
            yield return Destination.ID;
        
        }
    }
}
