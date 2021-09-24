using System;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings.EquipmentMatchings;
using LoadLink.LoadMatching.Domain.Entities;


namespace LoadLink.LoadMatching.Application.Test
    {
        public static class FakePosting
        {
            public static PostingBase PlatformPosting()
            {


                var posting = NotPlatformPosting();
                posting.UpdateDistanceAndPointId(PostingDistanceAndPointId());
                return posting;
            }

            public static PostingBase NotPlatformPosting()
            {
            var posting = new PostingBase("a", DateTime.Now,
                                       "a", "a", 20,
                                       "a", "a", 20,
                                       1, 1, "a", "a",
                                       1, "a",
                                       "a", "a", "a", "a",
                                       1, "", false,
                                       1);

            return posting;
            }
        public static PostingBase NotPlatformPostingOfCorridor(string corridor)
        {

            var posting = new PostingBase("a", DateTime.Now, 
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
                    1, 1, 1);
            }
            public static LeadBase LeadBase()
            {
            return new PlatformEquipmentLead(FakePosting.PlatformPosting(), PlatformPosting(), "N",true);
            }
        }
    }



