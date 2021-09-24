using System.Threading.Tasks;
using Xunit;
using LoadLink.LoadMatching.Persistence.Repositories.LeadRepositories;
namespace LoadLink.LoadMatching.Appliation.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var test = new EquipmentLeadRepository();
            var result = await test.GetListAsync("es", "s");
            Assert.NotNull(result );

        }
    }
}
