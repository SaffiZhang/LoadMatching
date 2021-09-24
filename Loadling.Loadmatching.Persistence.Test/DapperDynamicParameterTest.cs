using System;
using System.Threading.Tasks;
using Xunit;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories;

namespace Loadling.Loadmatching.Persistence.Test
{
    public class DapperDynamicParameterTest
    {
        [Fact]
        public async Task Test1()
        {
            var dbConStr = "Server=udvlpdb01.linklogi.com;Database=LoadMatching_USIT_Dev;Trusted_Connection=yes;";
            var equipmentPostingRep = new EquipmentPostingRepository(dbConStr);
            var result =await equipmentPostingRep.GetDistanceAndPointId("Ottawa", "On", "Mississauga", "On");
            Assert.Equal(2, result.SrceCountry);
        }
    }
}
