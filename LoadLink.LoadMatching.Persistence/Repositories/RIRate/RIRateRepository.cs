using Dapper;
using LoadLink.LoadMatching.Application.RIRate.Repository;
using LoadLink.LoadMatching.Application.RIRate.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.RIRate
{
    public class RIRateRepository : IRIRateRepository
    {
        private readonly IDbConnection _dbConnection;

        public RIRateRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<UspGetRIRateResult> GetAsync(GetRIRateCommand requestLane)
        {
            var proc = "usp_GetRIRate";
            var param = new DynamicParameters();

            param.Add("@VType", requestLane.VehicleTypeConverted);
            param.Add("@SrceSt", requestLane.SrceSt);
            param.Add("@SrceCity", requestLane.SrceCity);
            param.Add("@DestSt", requestLane.DestSt);
            param.Add("@DestCity", requestLane.DestCity);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetRIRateResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
