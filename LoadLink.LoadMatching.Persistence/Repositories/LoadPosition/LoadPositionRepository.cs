using Dapper;
using LoadLink.LoadMatching.Application.LoadPosition.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.LoadPosition
{
    public class LoadPositionRepository : ILoadPositionRepository
    {
        private readonly IDbConnection _dbConnection;

        public LoadPositionRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetLoadPositionResult>> GetListAsync(int token)
        {
            var proc = "dbo.usp_GetLoadPosition";

            var param = new DynamicParameters();
            param.Add("@Token", token);

            var result = await SqlMapper.QueryAsync<UspGetLoadPositionResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task CreateAsync(int token)
        {
            var proc = "dbo.usp_CreateLoadPosition";

            var param = new DynamicParameters();
            param.Add("@Token", token);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }
    }
}
