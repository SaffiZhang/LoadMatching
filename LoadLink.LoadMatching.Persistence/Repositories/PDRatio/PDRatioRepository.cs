using Dapper;
using LoadLink.LoadMatching.Application.PDRatio.Repository;
using LoadLink.LoadMatching.Application.PDRatio.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.PDRatio
{
    public class PDRatioRepository : IPDRatioRepository
    {
        private readonly IDbConnection _dbConnection;

        public PDRatioRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<UspGetPDRatioResult> GetAsync(GetPDRatioCommand requestLane)
        {
            var proc = "usp_GetPDRatio";
            var param = new DynamicParameters();

            param.Add("@VType", requestLane.VehicleTypeConverted);
            param.Add("@SrceSt", requestLane.SrceSt);
            param.Add("@SrceCity", requestLane.SrceCity);
            param.Add("@DestSt", requestLane.DestSt);
            param.Add("@DestCity", requestLane.DestCity);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetPDRatioResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
