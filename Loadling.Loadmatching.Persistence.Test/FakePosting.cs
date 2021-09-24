using System;
using LoadLink.LoadMatching.Domain.AggregatesModel.BasePostingAggregate;
using LoadLink.LoadMatching.Domain.Entities;


namespace Loadling.Loadmatching.Persistence.Test
{
        public static class FakePosting
        {
            public static BasePosting PlatformPosting()
            {


                var posting = NotPlatformPosting();
                posting.UpdateDistanceAndPointId(PostingDistanceAndPointId());
                return posting;
            }

            public static BasePosting NotPlatformPosting()
            {
            var posting = new BasePosting("a", DateTime.Now,
                                       "a", "a", 20,
                                       "a", "a", 20,
                                       1, 1, "a", "a",
                                       1, "a",
                                       "a", "a", "a", "a",
                                       1, "", false,
                                       1);

            return posting;
            }
        
        public static BasePosting NotPlatformPostingOfCorridor(string corridor)
        {

            var posting = new BasePosting("a", DateTime.Now, 
                    "a", "a", 20,
                    "a", "a", 20,
                    1, 1, "a", "a",
                    1, "a", 
                    "a", "a", "a", "a",
                    1, corridor, false,
                    1);

            return posting;
        }
        public static PostingDistanceAndPointId PostingDistanceAndPointId()
            {
                return new PostingDistanceAndPointId(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            }
            public static PostingDistanceAndPointId PostingDistanceAndPointId(Point srce, Point dest)
            {
                return new PostingDistanceAndPointId(1, 1, 
                    srce.Long, srce.Lat,
                    1, 1, 1,
                    dest.Long, dest.Lat,
                    1, 1,1);
            }
            public static BaseLead BaseLead()
            {
                return new BaseLead(PlatformPosting(), PlatformPosting(), false, 1, "1", "1", "1");
            }
        }
    }



